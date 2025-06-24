using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Data.Worker
{
    public class WorkOrderRepository
    {

        // 작업 지시 정보 가져오는 메서드
        public DataTable GetWorkOrder(int EmployeeID)
        {
            string query = @"SELECT pc.Name, w.productID, wop.ProcessID, wop.EquipmentID, wop.WorkOrderProcessID, w.WorkOrderID, p.Name AS 제품명, w.OrderQty AS 주문수량, w.StartDate AS 지시날짜, u.UserName AS 지시자, wop.Status AS 진행상태
                            FROM WorkOrders w
                            JOIN Product p ON p.ProductID = w.ProductID
                            JOIN WorkOrderProcess wop ON wop.WorkOrderID = w.WorkOrderID
                            JOIN Users u ON u.EmployeeID = w.IssueBy
                            JOIN Users uu ON uu.EmployeeID = @EmployeeID
                            JOIN Process pc ON pc.ProcessID = wop.ProcessID
                            WHERE uu.Department = pc.Name AND wop.Status IN ('대기', '진행 중')
                            ";

            SqlParameter[] parameter =
            [
                new SqlParameter("@EmployeeID", EmployeeID)
            ];
            return DBHelper.ExecuteDataTable(query, parameter);
        }


        /// <summary>
        /// 아래 3개의 메서드는 작업 지시 공정 시작 시 호출됩니다.
        /// 트랜잭션으로 처리
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <param name="WorkOrderProcessID"></param>
        /// <param name="conn"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public int UpdateWorkProcessStatus(int EmployeeID, int WorkOrderProcessID, SqlConnection conn, SqlTransaction tran)
        {
            // 대기 상태의 작업만 가능
            string query = @"UPDATE WorkOrderProcess
                             SET AssignedUserID = @AssignedUserID, StartTime = @StartTime, Status = '진행 중'
                             WHERE WorkOrderProcessID = @WorkOrderProcessID AND Status = '대기'";
            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
            {
                cmd.Parameters.AddWithValue("@AssignedUserID", EmployeeID);
                cmd.Parameters.AddWithValue("@StartTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@WorkOrderProcessID", WorkOrderProcessID);
                return cmd.ExecuteNonQuery(); // 성공 시 1 반환
            }
        }

        public int UpdateWorkOrder(int WorkOrderID, SqlConnection conn, SqlTransaction tran)
        {
            string query = @"UPDATE WorkOrders
                             SET Status = '진행 중'
                             WHERE WorkOrderID = @WorkOrderID";

            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
            {
                cmd.Parameters.AddWithValue("@WorkOrderID", WorkOrderID);
                return cmd.ExecuteNonQuery(); // 성공 시 1 반환
            }
        }

        public int UpdateEquipmentSatus(int WorkOrderProcessID, SqlConnection conn, SqlTransaction tran)
        {
            string query = @"UPDATE e
                                    SET e.Status = '가동'
                                    FROM Equipment e
                                    JOIN WorkOrderProcess wop ON e.EquipmentID = wop.EquipmentID
                                    JOIN WorkOrders wds ON wop.WorkOrderID = wds.WorkOrderID
                                    WHERE wop.WorkOrderProcessID = @WorkOrderProcessID
                                    ";
            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
            {
                cmd.Parameters.AddWithValue("@WorkOrderProcessID", WorkOrderProcessID);
                return cmd.ExecuteNonQuery(); // 성공 시 1 반환
            }
        }

    }
}
