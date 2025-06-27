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
    public class WorkOrderServices
    {
        private WorkOrderRepository _workOrderRepository;
        public WorkOrderServices()
        {
            _workOrderRepository = new WorkOrderRepository();
        }

        public DataTable GetWorkOrders(int EmployeeID)
        {
            return _workOrderRepository.GetWorkOrder(EmployeeID);
        }

        public List<WorkOrder> GetOrdersByDate(DateTime date)
        {
            return _workOrderRepository.GetOrdersByDate(date);
        }
        public List<WorkOrder> GetOrdersWithDetailsByDate(DateTime date, int workerID)
        {
            return _workOrderRepository.GetOrdersWithDetailsByDate(date, workerID);
        }

        public bool StartWorkOrderProcess(int UserID, int WorkOrderID, int WorkOrderProcessID)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int workOrderProcessUpdate = _workOrderRepository.UpdateWorkProcessStatus(UserID, WorkOrderProcessID, conn, tran);

                        int workOrderUpdate = _workOrderRepository.UpdateWorkOrder(WorkOrderID, conn, tran);

                        int equipmentUpdate = _workOrderRepository.UpdateEquipmentSatus(WorkOrderProcessID, conn, tran);
                        if (workOrderProcessUpdate > 0 && workOrderUpdate > 0 && equipmentUpdate > 0)
                        {
                            tran.Commit();
                            return true;
                        }
                        else
                        {
                            tran.Rollback();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw new Exception($"작업 시작 오류: {ex.Message}", ex);
                    }
                }
            }
                
        }
    }
}
