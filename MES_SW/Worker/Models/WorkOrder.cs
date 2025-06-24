using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Worker.Models
{
    public class WorkOrder
    {
        public int EmployeeID { get; set; } // 작업자 ID
        public int WorkOrderProcessID { get; set; } // 작업 지시 공정 ID
        public int ProcessID { get; set; } // 공정 ID
        public int WorkOrderID { get; set; } // 작업 지시 ID
        public int EquipmentID { get; set; } // 설비 ID
        public int ProductID { get; set; } // 제품 ID
        public int OrderQty { get; set; } // 수량

        //public string Status { get; set; } // 상태

        public WorkOrder()
        {
            OrderQty = 0; // 기본값으로 0 설정
        }
    }
    
}
