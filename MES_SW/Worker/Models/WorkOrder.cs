using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Worker.Models
{
    public class WorkOrder
    {
        public int WorkerID { get; set; } // 작업자 ID
        public int IssuedBy { get; set; } // 관리자 ID
        public string IssuedByName { get; set; } // 작업 지시자 이름
        public int WorkOrderProcessID { get; set; } // 작업 지시 공정 ID
        public int ProcessID { get; set; } // 공정 ID
        public string ProcessName { get; set; }
        public int WorkOrderID { get; set; } // 작업 지시 ID
        
        public int EquipmentID { get; set; } // 설비 ID
        public int ProductID { get; set; } // 제품 ID

        public string ProductName { get; set; }
        public int OrderQty { get; set; } // 수량

        public DateTime StartDate { get; set; }

        public string Status { get; set; } // 상태

        public WorkOrder()
        {
            ProcessName = string.Empty;
            IssuedByName = String.Empty;
            OrderQty = 0; // 기본값으로 0 설정
            Status = String.Empty;
            ProductName = String.Empty;
        }
    }
    
}
