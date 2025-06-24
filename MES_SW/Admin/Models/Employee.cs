using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Admin.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        public bool Status { get; set; }

        public Employee()
        {
            Name = string.Empty;
            Role = string.Empty;
            Department = string.Empty;
        }
    }
}
