using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Data
{
    public static class DBHelper
    {
        private static string connectionString = "DBCONNECTION";
        
        // DB 연결
        public static  SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // INSERT, UPDATE, DELETE 쿼리 실행 (NonQuery)
        // 이 메서드는 SELECT가 아닌 쿼리(예: INSERT, UPDATE, DELETE)에서 사용
        public static int ExecuteNonQuery(string query, SqlParameter[]? parameters = null)
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
        public static SqlDataReader ExecuteReader(string query, SqlParameter[]? parameters = null)
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
        public static DataTable ExecuteDataTable(string query, SqlParameter[]? parameters = null)
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
    }
}
