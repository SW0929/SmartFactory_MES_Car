using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Data.Admin
{
    public class UserManageRepository
    {
        public DataTable UserInfo()
        {
            string query = "SELECT * FROM Users";
            return DBHelper.ExecuteDataTable(query);
        }

        public int InsertNewUserToDB(int EmployeeID, string Name, bool Status, string Role, string Department)
        {
            string query = @"INSERT INTO 
                            Users (EmployeeID, UserName, UserRole, UserStatus, Department) 
                            VALUES(@EmployeeID, @UserName, @UserRole, @UserStatus, @Department)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", EmployeeID),
                new SqlParameter("UserName", Name),
                new SqlParameter("UserStatus", Status),
                new SqlParameter("UserRole", Role),
                new SqlParameter("@Department", Department)
            };
            return DBHelper.ExecuteNonQuery(query, parameters);
            
        }

        public int UpdateUserToDB(int EmployeeID, string Name, bool Status, string Role, string Department)
        {
            string query = @"UPDATE Users 
                            SET UserName = @UserName,
                                UserRole = @UserRole, 
                                UserStatus = @UserStatus, 
                                Department = @Department 
                                WHERE EmployeeID = @EmployeeID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", EmployeeID),
                new SqlParameter("UserName", Name),
                new SqlParameter("UserStatus", Status),
                new SqlParameter("UserRole", Role),
                new SqlParameter("Department", Department)
            };
            return DBHelper.ExecuteNonQuery(query, parameters);
        }

        public int DeleteUserToDB(int EmployeeID)
        {
            string query = "DELETE FROM Users WHERE EmployeeID = @EmployeeID";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", EmployeeID)
            };
            return DBHelper.ExecuteNonQuery(query, parameters);
        }

    }
}
