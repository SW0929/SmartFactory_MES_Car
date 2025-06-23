using MES_SW.Data;
using MES_SW.Data.Worker;
using MES_SW.Worker.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
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

        public Dictionary<string, string> GetPerformanceLabelInfo(WorkOrderPerformance perf)
        {
            return _repository.GetDisplayInfo(perf.ProcessID, perf.EquipmentID, perf.ProductID);
        }

        public void CompleteWorkOrderProcess(WorkOrderPerformance perf)
        {
            _repository.CompleteWorkOrderTransaction(perf);
        }
    }
}
