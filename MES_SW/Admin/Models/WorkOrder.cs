using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Admin.Models
{
    public class WorkOrder
    {
        public int WorkOrderID { get; set; }
        public int ProductID { get; set; }
        public int OrderQty { get; set; }

        public DateTime StartDate { get; set; }
        public int IssueBy { get; set; }
        public string Status { get; set; }

        public WorkOrder()
        {
            Status = string.Empty;
        }

    }
}
