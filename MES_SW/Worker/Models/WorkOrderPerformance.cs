using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Worker.Models
{
    public class WorkOrderPerformance
    {
        public int WorkOrderProcessID { get; set; }
        public int WorkOrderID { get; set; }
        public int ProcessID { get; set; }
        public int IssuedBy { get; set; }
        public int EquipmentID { get; set; }
        public int ProductID { get; set; }
        public int GoodQty { get; set; }
        public int BadQty { get; set; }
        public string Reason { get; set; }
        public int OrderQty { get; set; }

        public WorkOrderPerformance()
        {
            GoodQty = 0;
            BadQty = 0;
            Reason = string.Empty;
            OrderQty = 0;
        }
    }
}
