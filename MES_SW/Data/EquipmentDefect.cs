using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Data
{
    public class EquipmentDefect
    {
        public int DefectID { get; set; }
        public int EquipmentID { get; set; }

        public string EquipmentName { get; set; }
        public DateTime DefectTime { get; set; }
        public int ReportedBy { get; set; }
        public string DefectType { get; set; }
        public string Description { get; set; }
        public bool Resolved { get; set; }
        public DateTime? ResolvedTime { get; set; }

        public EquipmentDefect() 
        {
            EquipmentName = string.Empty;
            DefectType = string.Empty;
            Description = string.Empty;
        }
        
    }
}
