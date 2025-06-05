using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MES_SW.DB
{
    public static class DBHelper
    {
        private static string connectionString = "Data Source=localhost;Initial Catalog=MES;Integrated Security=True;TrustServerCertificate=True;";
        
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

        // 생산지시와 생산공정 흐름 등록하기 위해 트랜잭션으로 처리
        public static int InsertWorkOrderWithProcess(string workOrderQuery, SqlParameter[] workOrderParams,
                                             string processQuery, SqlParameter[] processParams)
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

    }
}
