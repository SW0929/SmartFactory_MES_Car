using MES_SW.Admin.Models;
using MES_SW.Admin.Models.Items;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Data.Admin
{
    public class WorkOrderRepository
    {
        
        public DataTable GetWorkOrderFromDB()
        {
            string query = @"
                SELECT w.WorkOrderID, w.ProductID, w.OrderQty, w.StartDate, w.IssueBy, w.Status
                FROM WorkOrders w
                --JOIN WorkOrderProcess wop ON wop.WorkOrderID = w.WorkOrderID
                --WHERE wop.Status = '대기' OR wop.Status = '진행 중'
                ORDER BY w.StartDate ASC";

            return DBHelper.ExecuteDataTable(query);
        }


        #region 작업 추가
        public int InsertWorkOrderToDB(WorkOrder workOrder, SqlConnection conn, SqlTransaction tran)
        {
            /*
            OUTPUT INSERTED.ID는 SCOPE_IDENTITY()와 같은 역할을 하지만, 더 안전하고 강력한 방식으로 INSERT 시 생성된 ID 값을 바로 반환받을 수 있게 해주는 SQL 문법입니다. 
            특히 **트리거(trigger)**가 있는 테이블에서 SCOPE_IDENTITY()가 의도대로 동작하지 않는 경우에도 안전하게 사용할 수 있어요.
            */
            string workOrderQuery = @"INSERT INTO WorkOrders (ProductID, OrderQty, StartDate, IssueBy)
                                     OUTPUT INSERTED.WorkOrderID
                                     VALUES(@ProductID, @OrderQty, @StartDate, @IssueBy);";

            using (SqlCommand cmd = new SqlCommand(workOrderQuery, conn, tran))
            {
                cmd.Parameters.AddWithValue("@ProductID", workOrder.ProductID);
                cmd.Parameters.AddWithValue("@OrderQty", workOrder.OrderQty);
                cmd.Parameters.AddWithValue("@StartDate", workOrder.StartDate);
                cmd.Parameters.AddWithValue("@IssueBy", workOrder.IssueBy);
                return (int)cmd.ExecuteScalar();
            }
        }

        public int InsertWorkOrderPorcessToDB(int WorkOrderID, int SelectedEquipment, SqlConnection conn, SqlTransaction tran)
        {
            string workOrderProcessQuery = @"INSERT 
                                              INTO WorkOrderProcess (WorkOrderID, ProcessID, EquipmentID, Status)
                                              VALUES(@WorkOrderID, 1, @EquipmentID, '대기')";
            
            using (SqlCommand cmd = new SqlCommand(workOrderProcessQuery, conn, tran))
            {
                cmd.Parameters.AddWithValue("@WorkOrderID", WorkOrderID);
                cmd.Parameters.AddWithValue("@EquipmentID", SelectedEquipment);
                return cmd.ExecuteNonQuery();
            }
        }

        public int UpdateEquipmentStatusTODB(int SelectedEquipment, SqlConnection conn, SqlTransaction tran)
        {
            string equipmentQuery = "UPDATE Equipment SET Status = '할당 대기' WHERE EquipmentID = @EquipmentID";

            using (SqlCommand cmd = new SqlCommand(equipmentQuery, conn, tran))
            {
                cmd.Parameters.AddWithValue("@EquipmentID", SelectedEquipment);
                return cmd.ExecuteNonQuery();
            }
            
        }
        #endregion

        
        public int UpdateWorkOrderToDB(WorkOrder workOrder)
        {
            // 작업 지시는 로그인한 사용자로 수정되기 때문에 사용자ID는 고정
            string query = @"UPDATE WorkOrders 
                            SET ProductID = @ProductID, OrderQty = @OrderQty, StartDate = @StartDate, IssueBy = @IssueBy 
                            WHERE WorkOrderID = @WorkOrderID";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ProductID", workOrder.ProductID),
                new SqlParameter("@OrderQty", workOrder.OrderQty),
                new SqlParameter("@StartDate", workOrder.StartDate),
                new SqlParameter("@WorkOrderID", workOrder.WorkOrderID),
                new SqlParameter("@IssueBy", workOrder.IssueBy)
            };
            return DBHelper.ExecuteNonQuery(query, parameters);
        }


        #region 작업 삭제
        public int DeleteWorkProcessFromDB(WorkOrder workOrder, SqlConnection conn, SqlTransaction tran)
        {
            string deleteProcessQuery = @"DELETE FROM WorkOrderProcess WHERE WorkOrderID = @WorkOrderID";
            using (SqlCommand cmd = new SqlCommand(deleteProcessQuery, conn, tran))
            {
                cmd.Parameters.AddWithValue("@WorkOrderID", workOrder.WorkOrderID);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteWorkOrderFromDB(WorkOrder workOrder, SqlConnection conn, SqlTransaction tran)
        {
            string deleteOrderQuery = @"DELETE FROM WorkOrders WHERE WorkOrderID = @WorkOrderID";
            using (SqlCommand cmd = new SqlCommand(deleteOrderQuery, conn, tran))
            {
                cmd.Parameters.AddWithValue("@WorkOrderID", workOrder.WorkOrderID);
                return cmd.ExecuteNonQuery();
            }
            
        }

        public int UpdateEquipmentStatusFromDB(WorkOrder workOrder, SqlConnection conn, SqlTransaction tran)
        {
            string query = @"
                            UPDATE Equipment
                            SET Status = '대기'
                            WHERE EquipmentID IN (
                                SELECT EquipmentID FROM WorkOrderProcess WHERE WorkOrderID = @WorkOrderID)";

            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
            {
                cmd.Parameters.AddWithValue("@WorkOrderID", workOrder.WorkOrderID);
                return cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}
