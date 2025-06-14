using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing.Text;

namespace MES_SW.DB
{
    public static class DBHelper
    {
        private static string connectionString = "DBCONNECTION";
        
        // DB 연결 열기
        public static  SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // INSERT, UPDATE, DELETE 쿼리 실행 (NonQuery)
        // 이 메서드는 SELECT가 아닌 쿼리(예: INSERT, UPDATE, DELETE)에서 사용
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            // GetConnection()을 통해 DB 연결 객체(SqlConnection) 생성
            // using을 사용하면 connection이 작업 끝나면 자동으로 닫힘
            using (SqlConnection connection = GetConnection())
            {
                // SQL 명령을 수행할 SqlCommand 객체 생성
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //파라미터가 있다면 AddRange()로 명령에 SQL 파라미터 바인딩
                    // (바인딩은 무언가 서로 연결해 주는 것, LoginForm.cs에서 보여드림)
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);
                    connection.Open();
                    return command.ExecuteNonQuery();
                    // 영향받은 행의 개수를 반환
                }
            }
        }
        // SELECT 쿼리 실행 (DataReader 반환)
        public static SqlDataReader ExecuteReader(string query, SqlParameter[] parameters = null)
        {
            SqlConnection connection = GetConnection();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                connection.Open();
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }
        // SELECT 쿼리 실행 (DataTable 반환)
        public static DataTable ExecuteDataTable(string query, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            return dataTable;
        }

        // 생산지시와 생산공정 흐름 등록하기 위해 트랜잭션으로 처리 (관리자 쪽 작업지시에 있음)
        public static int InsertWorkOrderWithProcess(string workOrderQuery, SqlParameter[] workOrderParams,
                                             string processQuery, SqlParameter[] processParams, string equipmentQuery, SqlParameter[] equipmentParams)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int newId;

                        // 1. INSERT INTO WorkOrders + OUTPUT INSERTED.WorkOrderID
                        using (SqlCommand cmd1 = new SqlCommand(workOrderQuery, conn, tran))
                        {
                            cmd1.Parameters.AddRange(workOrderParams);
                            object result = cmd1.ExecuteScalar();

                            if (result == null || result == DBNull.Value)
                                throw new Exception("WorkOrderID를 가져올 수 없습니다.");

                            newId = Convert.ToInt32(result);
                        }

                        // 2. WorkOrderID 값을 processParams에 주입
                        SqlParameter idParam = processParams.FirstOrDefault(p => p.ParameterName == "@WorkOrderID");
                        if (idParam != null)
                        {
                            idParam.Value = newId;
                        }
                        else
                        {
                            throw new Exception("@WorkOrderID 파라미터가 processParams에 없습니다.");
                        }

                        // 3. INSERT INTO WorkOrderProcess
                        using (SqlCommand cmd2 = new SqlCommand(processQuery, conn, tran))
                        {
                            cmd2.Parameters.AddRange(processParams);
                            cmd2.ExecuteNonQuery();
                        }

                        //  4. 설비 자동 할당 (공정 흐름에 맞는 설비 ID 가져오기)
                        using (SqlCommand cmd3 = new SqlCommand(equipmentQuery, conn, tran))
                        {
                            cmd3.Parameters.AddRange(equipmentParams);
                            cmd3.ExecuteNonQuery();
                        }

                        tran.Commit();
                        return newId;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw new Exception("작업지시 등록 중 오류 발생: " + ex.Message, ex);
                    }
                }
            }
        }

        // 공정 흐름에 맞는 설비 자동 부여하기 위한 메서드
        public static int GetAvailableEquipmentId(int processId)
        {
            int equipmentId = 0;
            string query = @"
                            SELECT TOP 1 EquipmentID
                            FROM Equipment
                            WHERE ProcessID = @ProcessID AND Status = '대기'
                            "; // 혹은 다른 기준

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ProcessID", processId);
                conn.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                    equipmentId = Convert.ToInt32(result);
            }

            return equipmentId; // 없으면 0
        }

        public static void CompleteWorkOrderProcess(int workOrderProcessId, int workOrderID, int processID)
        {
            int nextProcessID = processID + 1;
            int nextEquipmentID = GetAvailableEquipmentId(nextProcessID); // 다음 공정에 할당할 설비 ID 가져오기
            const int MAX_PROCESS_ID = 5; // ⚠️ 마지막 공정 번호. 필요시 DB에서 가져올 수도 있음.
            

            string finishWOPQuery = @"UPDATE WorkOrderProcess
                                     SET EndTime = @EndTime, Status = '완료'
                                     WHERE WorkOrderProcessID = @WorkOrderProcessID";


            string insertLogQuery = @"
                                    INSERT INTO WorkOrderProcessLog
                                    (WorkOrderProcessID, WorkOrderID, ProcessID, EquipmentID, AssignedUserID, StartTime, EndTime)
                                    SELECT WorkOrderProcessID, WorkOrderID, ProcessID, EquipmentID, AssignedUserID, StartTime, EndTime
                                    FROM WorkOrderProcess
                                    WHERE WorkOrderProcessID = @WorkOrderProcessID";

            string equipmentQuery = @"UPDATE e
                                    SET e.Status = '대기', e.LastUsedTime = @LastUsedTime
                                    FROM Equipment e
                                    JOIN WorkOrderProcess wop ON e.EquipmentID = wop.EquipmentID
                                    JOIN WorkOrders wds ON wop.WorkOrderID = wds.WorkOrderID
                                    WHERE wop.WorkOrderProcessID = @WorkOrderProcessID
                                    ";

            string updateQuery = @"INSERT INTO WorkOrderProcess (WorkOrderID, EquipmentID, ProcessID, Status, AssignedUserId, StartTime, EndTime)
                                    SELECT WorkOrderID, @EquipmentID, @ProcessID,N'대기', NULL, NULL, NULL
                                    FROM WorkOrderProcess
                                    WHERE WorkOrderProcessID = @workOrderProcessID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                

                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. 작업 완료
                        using (SqlCommand FCmd = new SqlCommand(finishWOPQuery, conn, tran))
                        {
                            FCmd.Parameters.AddWithValue("@EndTime", DateTime.Now);
                            FCmd.Parameters.AddWithValue("@WorkOrderProcessID", workOrderProcessId);
                            FCmd.ExecuteNonQuery();
                        }


                        // 2. 로그 기록 -> Ex) 프레스 공정이 끝난 후 해당 공정의 로그를 기록
                        using (SqlCommand insertCmd = new SqlCommand(insertLogQuery, conn, tran))
                        {
                            insertCmd.Parameters.AddWithValue("@WorkOrderProcessID", workOrderProcessId);
                            insertCmd.ExecuteNonQuery();
                        }

                        // 3. 설비 상태 변경 -> 대기 상태로 변경
                        using (SqlCommand equipmentCmd = new SqlCommand(equipmentQuery, conn, tran))
                        {
                            equipmentCmd.Parameters.AddWithValue("@WorkOrderProcessID", workOrderProcessId);
                            equipmentCmd.Parameters.AddWithValue("@LastUsedTime", DateTime.Now);
                            equipmentCmd.ExecuteNonQuery();
                        }

                        // 4. 상태 변경 -> 다음 공정으로 이동
                        // 4. 다음 공정이 남아 있는 경우에만 새 공정 추가
                        if (processID <= MAX_PROCESS_ID - 1)
                        {

                            // 1. 다음 공정 INSERT, 다음 공정을 자동으로 할당 (프레스 -> 차체 -> 도장 -> 조립 -> 검사) 순서
                            using (SqlCommand insertNextCmd = new SqlCommand(updateQuery, conn, tran))
                            {
                                insertNextCmd.Parameters.AddWithValue("@WorkOrderProcessID", workOrderProcessId);
                                insertNextCmd.Parameters.AddWithValue("@ProcessID", nextProcessID);
                                insertNextCmd.Parameters.AddWithValue("@EquipmentID", nextEquipmentID);
                                insertNextCmd.ExecuteNonQuery();
                            }

                            // 2. 다음 공정에 할당된 설비 상태를 '할당 대기'로 업데이트
                            string updateEquipmentStatusQuery = @"
                                                                UPDATE Equipment
                                                                SET Status = N'할당 대기'
                                                                WHERE EquipmentID = @EquipmentID
                                                                ";
                            if (nextEquipmentID == 0)
                            {
                                throw new Exception("다음 공정에 할당할 설비가 없습니다.");
                            }
                            else
                            {
                                using (SqlCommand updateEquipCmd = new SqlCommand(updateEquipmentStatusQuery, conn, tran))
                                {
                                    updateEquipCmd.Parameters.AddWithValue("@EquipmentID", nextEquipmentID);
                                    updateEquipCmd.ExecuteNonQuery();
                                }
                            }

                        }
                        else
                        {
                            string LastQuery = @"UPDATE WorkOrders
                                             SET Status = '완료'
                                             WHERE WorkOrderID = @WorkOrderID";
                            using (SqlCommand LastCommand = new SqlCommand(LastQuery, conn, tran))
                            {
                                LastCommand.Parameters.AddWithValue("@WorkOrderID", workOrderID);
                                LastCommand.ExecuteNonQuery();
                            }
                        }

                            tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        // TODO : 현재 설비가 모두 사용 중인 경우에 예외 처리 필요
                        tran.Rollback();
                        throw new Exception("작업 종료 시 이상 있음: " + ex.Message, ex); ;
                    }
                }
            }
        }

    }
}
