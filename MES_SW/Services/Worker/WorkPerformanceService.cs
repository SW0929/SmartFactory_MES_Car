using MES_SW.Data;
using MES_SW.Data.Worker;
using MES_SW.Worker.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Services.Worker
{
    public class WorkPerformanceService
    {
        private readonly WorkOrderPerformanceRepository _repository;

        public WorkPerformanceService(WorkOrderPerformanceRepository repository)
        {
            _repository = repository;
        }

        // 생산지시 정보 가져오는 메서드
        public Dictionary<string, string> GetPerformanceLabelInfo(WorkOrderPerformance perf)
        {
            return _repository.GetDisplayInfo(perf.ProcessID, perf.EquipmentID, perf.ProductID);
        }

        // 실적 등록 및 작업 종료 메서드
        public void CompleteWorkOrderProcess(WorkOrderPerformance perf)
        {
            _repository.CompleteWorkOrderTransaction(perf);
        }

        public DataTable GetUserPerformances(int userId)
        {
            return _repository.GetPerformancesByUser(userId);
        }

        public bool UpdatePerformance(int performanceId, int goodQty, int defectQty, string reason)
        {
            int result = _repository.UpdatePerformance(performanceId, goodQty, defectQty, reason);
            return result > 0;
        }
    }
}
