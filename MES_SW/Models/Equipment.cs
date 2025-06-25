using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Models
{
    public class Equipment
    {
        public int EquipmentID { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }
        public DateTime? LastUserTime { get; set; }

        public int ProcessID { get; set; }

        public Equipment()
        {
            Name = string.Empty;
            Type = string.Empty;
            Status = string.Empty;
        }
    }
}
