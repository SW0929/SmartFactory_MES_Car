using MES_SW.Admin.Models;
using MES_SW.Data.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Services.Admin
{
    public class UserManageService
    {
        private readonly UserManageRepository _userManageRepository;
        public UserManageService()
        {
            _userManageRepository = new UserManageRepository();
        }

        public DataTable GetUserData()
        {
            return _userManageRepository.UserInfo();
        }

        public int InsertNewUser(Employee employee)
        {
            return _userManageRepository.InsertNewUserToDB(employee.EmployeeID, employee.Name, employee.Status, employee.Role, employee.Department);
        }

        public int UpdateUser(Employee employee)
        {
            return _userManageRepository.UpdateUserToDB(employee.EmployeeID, employee.Name, employee.Status, employee.Role, employee.Department);
        }

        public int DeleteUser(int EmployeeID)
        {
            return _userManageRepository.DeleteUserToDB(EmployeeID);
        }
    }
}
