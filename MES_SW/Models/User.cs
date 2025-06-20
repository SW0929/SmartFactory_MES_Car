using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Models
{
    public class User
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    
    public User()
        {
            Name = string.Empty;  // 기본값 할당
            Role = string.Empty;  // 기본값 할당
        }
    }
}
