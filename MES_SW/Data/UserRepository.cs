using Microsoft.Data.SqlClient;
using MES_SW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Data
{
    public class UserRepository
    {
        public User? GetUserById(int employeeId)
        {
            string query = "SELECT * FROM Users WHERE EmployeeId = @id";
            // SqlParameter 객체를 통해 int.Parse(EmployeeNumText.Text) 값을
            // @id 에 "바인딩"하는 것
            // @id 는 파라미터
            SqlParameter[] parameters = { new SqlParameter("@id", employeeId) };

            SqlDataReader reader = DBHelper.ExecuteReader(query, parameters);
            try
            {
                if (reader.Read())
                {
                    return new User
                    {
                        // 조회한 테이블의 레코드 값의 열을 가져올 수 있음.
                        EmployeeId = employeeId,
                        Name = reader.GetString(1),
                        Role = reader.GetString(2),
                        IsActive = reader.GetBoolean(3)
                    };
                }
                return null;
            }
            finally
            {
                // 메모리 누수나 DB 연결 유지 문제 방지를 위해 닫아주는 게 중요, 꼭 닫아줘야 함.
                reader.Close(); // 연결 종료
            }
        }
    }
}
