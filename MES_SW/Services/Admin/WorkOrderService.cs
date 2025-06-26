using MES_SW.Admin.Models;
using MES_SW.Data;
using MES_SW.Data.Admin;
using MES_SW.Services.Common;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Services.Admin
{
    public class WorkOrderService
    {
        private WorkOrderRepository _workOrderRepository;
        private EquipmentService _equipmentService;
        public WorkOrderService()
        {
            _workOrderRepository = new WorkOrderRepository();
            _equipmentService = new EquipmentService();
        }

        public DataTable GetWorkOrder()
        {
            return _workOrderRepository.GetWorkOrderFromDB();
        }

        public bool InsertWorkOrder(WorkOrder workOrder)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int workOrderID = _workOrderRepository.InsertWorkOrderToDB(workOrder, conn, tran);
                        workOrder.WorkOrderID = workOrderID;

                        int selectedEquipmentId = _equipmentService.GetAvailableEquipmentID(1); // 프레스는 공정 번호 1

                        if (selectedEquipmentId == 0)
                        {
                            throw new Exception("사용 가능한 설비가 없습니다.");
                        }
                        int first = _workOrderRepository.InsertWorkOrderPorcessToDB(workOrder.WorkOrderID, selectedEquipmentId, conn, tran);
                        int second = _workOrderRepository.UpdateEquipmentStatusTODB(selectedEquipmentId, conn, tran);
                        if (first > 0 && second > 0)
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
                        throw new Exception($"작업 지시 등록 실패: {ex.Message}", ex);
                    }
                }
            }
        }

        public int UpdateWorkOrder(WorkOrder workOrder)
        {
            return _workOrderRepository.UpdateWorkOrderToDB(workOrder);
        }

        public bool DeleteWorkOrder(WorkOrder workOrder)
        {
            using(SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                using(SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int changeEquipmentStatus = _workOrderRepository.UpdateEquipmentStatusFromDB(workOrder, conn, tran);
                        // WorkProcess 먼저 삭제 해야 함.
                        int first = _workOrderRepository.DeleteWorkProcessFromDB(workOrder, conn, tran);

                        int second = _workOrderRepository.DeleteWorkOrderFromDB(workOrder, conn, tran);

                        if (changeEquipmentStatus > 0 && first > 0 && second > 0)
                        {
                            tran.Commit(); 
                            return true;
                        }
                        else
                        {
                            tran.Rollback() ;
                            return false;
                        }
                    }
                    catch(Exception ex)
                    {
                        tran.Rollback();
                        throw new Exception($"작업 삭제 실패 : {ex.Message}");
                    }
                }
            }
        }
    }
}
