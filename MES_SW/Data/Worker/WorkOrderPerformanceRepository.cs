using MES_SW.Services.Common;
using MES_SW.Worker.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Data.Worker
{
    public class WorkOrderPerformanceRepository
    {
        //실적 입력 시 필요한 정보 가져오기
        public Dictionary<string, string> GetDisplayInfo(int EquipmentID, int ProductID, int ProcessID)
        {
            string query = @"SELECT 
                            pr.Name AS ProcessName,
                            eq.Name AS EquipmentName,
                            pd.Name AS ProductName
                            FROM Process pr
                            JOIN Equipment eq ON eq.EquipmentID = @EquipmentID
                            JOIN Product pd ON pd.ProductID = @ProductID
                            WHERE pr.ProcessID = @ProcessID";

            SqlParameter[] parameters = new SqlParameter[]
            {
                        new SqlParameter("@ProcessID", ProcessID),
                        new SqlParameter("@EquipmentID", EquipmentID),
                        new SqlParameter("@ProductID", ProductID)
            };

            DataTable dt = DBHelper.ExecuteDataTable(query, parameters);

            if (dt.Rows.Count == 0)
                return null;

            return new Dictionary<string, string>
            {
                ["ProcessName"] = dt.Rows[0]["ProcessName"].ToString(),
                ["EquipmentName"] = dt.Rows[0]["EquipmentName"].ToString(),
                ["ProductName"] = dt.Rows[0]["ProductName"].ToString()
            };
        }

        //생산 종료 후 데이터 업데이트(작업자)
        // 여기서는 쿼리만 만들고 트랜잭션은 서비스에서 처리하도록 수정해야함.***********************************
        // TODO : 메서드 타입 변경해서 오류 메시지 출력 추가하기 AND DTO 만들어서 처리해야 함 매개변수가 너무 많음
        public void CompleteWorkOrderTransaction(WorkOrderPerformance perf)
        {
            var equipmentService = new EquipmentService(); // 서비스 인스턴스 생성 

            int nextProcessID = perf.ProcessID + 1;
            int nextEquipmentID = equipmentService.GetAvailableEquipmentID(nextProcessID); // 다음 공정에 할당할 설비 ID 가져오기
            const int MAX_PROCESS_ID = 5; // ⚠️ 마지막 공정 번호. 필요시 DB에서 가져올 수도 있음.

            // TODO : 작업이 끝나면 다음 공정으로 자동으로 넘어가기 때문에 '진행 중'은 필요한지 확인 필요함.
            string finishWOPQuery = @"UPDATE WorkOrderProcess
                                     SET EndTime = @EndTime, Status = '완료'
                                     WHERE WorkOrderProcessID = @WorkOrderProcessID AND Status = '진행 중';";


            string insertLogQuery = @"
                                    INSERT INTO WorkOrderProcessLog
                                    (WorkOrderProcessID, WorkOrderID, ProcessID, EquipmentID, AssignedUserID, StartTime, EndTime)
                                    SELECT WorkOrderProcessID, WorkOrderID, ProcessID, EquipmentID, AssignedUserID, StartTime, EndTime
                                    FROM WorkOrderProcess
                                    WHERE WorkOrderProcessID = @WorkOrderProcessID;";

            string equipmentQuery = @"UPDATE e
                                    SET e.Status = '대기', e.LastUsedTime = @LastUsedTime
                                    FROM Equipment e
                                    JOIN WorkOrderProcess wop ON e.EquipmentID = wop.EquipmentID
                                    JOIN WorkOrders wds ON wop.WorkOrderID = wds.WorkOrderID
                                    WHERE wop.WorkOrderProcessID = @WorkOrderProcessID;
                                    ";

            string updateQuery = @"INSERT INTO WorkOrderProcess (WorkOrderID, EquipmentID, ProcessID, Status, AssignedUserId, StartTime, EndTime)
                                    SELECT WorkOrderID, @EquipmentID, @ProcessID,N'대기', NULL, NULL, NULL
                                    FROM WorkOrderProcess
                                    WHERE WorkOrderProcessID = @workOrderProcessID;";

            string insertPerformanceQuery = @"INSERT
                                              INTO WorkPerformance (OrderID, ProcessID, ProductID, RegisteredBy, EquipmentID, GoodQty, DefectQty, Reason, RegDate)
                                              VALUES (@WorkOrderID, @ProcessID, @ProductID, @RegiseredBy, @EquipmentID, @GoodQty, @DefectQTy, @Reason, @RegDate);";

            using (SqlConnection conn = DBHelper.GetConnection())
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
                            FCmd.Parameters.AddWithValue("@WorkOrderProcessID", perf.WorkOrderProcessID);
                            FCmd.ExecuteNonQuery();
                        }


                        // 2. 로그 기록 -> Ex) 프레스 공정이 끝난 후 해당 공정의 로그를 기록
                        using (SqlCommand insertCmd = new SqlCommand(insertLogQuery, conn, tran))
                        {
                            insertCmd.Parameters.AddWithValue("@WorkOrderProcessID", perf.WorkOrderProcessID);
                            insertCmd.ExecuteNonQuery();
                        }

                        // 3. 설비 상태 변경 -> 대기 상태로 변경
                        using (SqlCommand equipmentCmd = new SqlCommand(equipmentQuery, conn, tran))
                        {
                            equipmentCmd.Parameters.AddWithValue("@WorkOrderProcessID", perf.WorkOrderProcessID);
                            equipmentCmd.Parameters.AddWithValue("@LastUsedTime", DateTime.Now);
                            equipmentCmd.ExecuteNonQuery();
                        }

                        // 4. 상태 변경 -> 다음 공정으로 이동
                        // 4. 다음 공정이 남아 있는 경우에만 새 공정 추가
                        if (perf.ProcessID <= MAX_PROCESS_ID - 1)
                        {
                            if (nextEquipmentID == 0)
                            {
                                throw new Exception("다음 공정에 할당할 설비가 없습니다.\n잠시만 기다려주세요.");
                            }
                            else
                            {
                                // 1. 다음 공정 INSERT, 다음 공정을 자동으로 할당 (프레스 -> 차체 -> 도장 -> 조립 -> 검사) 순서
                                using (SqlCommand insertNextCmd = new SqlCommand(updateQuery, conn, tran))
                                {
                                    insertNextCmd.Parameters.AddWithValue("@WorkOrderProcessID", perf.WorkOrderProcessID);
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
                                LastCommand.Parameters.AddWithValue("@WorkOrderID", perf.WorkOrderID);
                                LastCommand.ExecuteNonQuery();
                            }
                        }
                        // 5. 작업 성과 등록
                        using (SqlCommand performanceCommand = new SqlCommand(insertPerformanceQuery, conn, tran))
                        {
                            performanceCommand.Parameters.AddWithValue("@WorkOrderID", perf.WorkOrderID);
                            performanceCommand.Parameters.AddWithValue("@ProcessID", perf.ProcessID);
                            performanceCommand.Parameters.AddWithValue("@ProductID", perf.ProductID);
                            performanceCommand.Parameters.AddWithValue("@RegiseredBy", perf.EmployeeID); // 현재 사용자 이름
                            performanceCommand.Parameters.AddWithValue("@EquipmentID", perf.EquipmentID);
                            performanceCommand.Parameters.AddWithValue("@GoodQty", perf.GoodQty);
                            performanceCommand.Parameters.AddWithValue("@DefectQTy", perf.BadQty);
                            performanceCommand.Parameters.AddWithValue("@Reason", perf.Reason);
                            performanceCommand.Parameters.AddWithValue("@RegDate", DateTime.Now);
                            performanceCommand.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        // TODO : 현재 설비가 모두 사용 중인 경우에 예외 처리 필요
                        tran.Rollback();
                        MessageBox.Show(ex.Message); ;
                    }
                }
            }
        }

        // 사용자별 실적 조회 (사용자가 입력한 실적을 조회하는 메서드)
        public DataTable GetPerformancesByUser(int EmployeeID)
        {
            string query = @"SELECT wpf.PerformanceID, wpf.OrderID AS 주문번호, pr.Name AS 제품, p.Name AS 공정, e.Name AS 설비, wpf.RegisteredBy AS 작업자, wpf.GoodQty, wpf.DefectQty, wpf.Reason
                            FROM WorkPerformance wpf
                            JOIN Process p ON p.ProcessID = wpf.ProcessID
                            JOIN Product pr ON pr.ProductID = wpf.ProductID
                            JOIN Equipment e ON e.EquipmentID = wpf.EquipmentID
                            WHERE wpf.RegisteredBy = @UserID;";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", EmployeeID)
            };

            return DBHelper.ExecuteDataTable(query, parameters);
        }

        // 실적 업데이트 메서드 (작업자가 실적을 수정하는 경우)
        public int UpdatePerformance(int performanceId, int goodQty, int defectQty, string reason)
        {
            string query = @"UPDATE WorkPerformance
                         SET GoodQty = @GoodQty, DefectQty = @DefectQty, Reason = @Reason, UpdateDate = @UpdateDate
                         WHERE PerformanceID = @PerformanceID";

            SqlParameter[] parameters =
            {
            new SqlParameter("@GoodQty", goodQty),
            new SqlParameter("@DefectQty", defectQty),
            new SqlParameter("@Reason", reason),
            new SqlParameter("@UpdateDate", DateTime.Now),
            new SqlParameter("@PerformanceID", performanceId)
            };

            return DBHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
