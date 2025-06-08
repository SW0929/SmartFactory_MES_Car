using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Admin
{
    public class EquipmentItem
    {
        public string EquipmentName { get; set; }
        public int EquipmentID { get; set; }
        public override string ToString()
        {
            return EquipmentName;
        }
    }
}
