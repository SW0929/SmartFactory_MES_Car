using mes;
using MES_SW.Data;
using MES_SW.Services.Worker;
using MES_SW.Worker.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODO : 현재 진행 중인 생산 작업은 다시 재시작 못하게 하기, 각 테이블 Status 컴럼 일관화 하기 WorkOrders 업데이트 안됨
// 그리고 설비 가동 까지 연동하게 해야함.

namespace MES_SW.Worker.WorkerUserControl
{
    public partial class UserControl_WorkOrderList : UserControl
    {
        
        private WorkOrder _workOrder = new();
        private WorkOrderServices _workOrderServices;
        

        public UserControl_WorkOrderList(int UserID)
        {
            InitializeComponent();
            _workOrder.EmployeeID = UserID;
            _workOrderServices = new WorkOrderServices();
            LoadWorkOrders(_workOrder.EmployeeID);
        }
        #region Load_Methods
        private void LoadWorkOrders(int _userID)
        {
            dataGridView1.DataSource = _workOrderServices.GetWorkOrders(_userID);
            dataGridView1.Columns["WorkOrderID"].Visible = false; // WorkOrderID 열 숨기기
            //dataGridView1.Columns["WorkOrderProcessID"].Visible = false; // WorkOrderProcessID 열 숨기기
            dataGridView1.Columns["ProcessID"].Visible = false; // ProcessID 열 숨기기
            dataGridView1.Columns["ProductID"].Visible = false; // ProductID 열 숨기기
            dataGridView1.Columns["EquipmentID"].Visible = false; // EquipmentID 열 숨기기
        }

        #endregion



        #region Event_Methods
        // DataGridView 셀 클릭 이벤트 핸들러
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                
                _workOrder.WorkOrderID = (int)row.Cells["WorkOrderID"].Value; // 작업 지시 ID
                _workOrder.WorkOrderProcessID = (int)row.Cells["WorkOrderProcessID"].Value;
                _workOrder.ProcessID = (int)row.Cells["ProcessID"].Value;
                _workOrder.ProductID = (int)row.Cells["ProductID"].Value; // 제품 ID
                _workOrder.EquipmentID = (int)row.Cells["EquipmentID"].Value; // 설비 ID
                _workOrder.OrderQty = (int)row.Cells["주문수량"].Value; // 주문 수량
                //_status = row.Cells["진행상태"].Value.ToString(); // 진행 상태
                WorkOrderID.Text = _workOrder.WorkOrderID.ToString(); // TextBox에 작업 지시 ID 표시
            }
            else
            {
                // 클릭한 셀이 유효하지 않으면 종료
                return;
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                

                var statusCell = row.Cells["진행상태"].Value;
                if (statusCell != null && statusCell.ToString() == "진행 중")
                {
                    if (_workOrder == null)
                    {
                        MessageBox.Show("작업 지시 정보가 없습니다.");
                        return;
                    }

                    using (WorkPerformanceForm workReportForm = new WorkPerformanceForm(
                        _workOrder.WorkOrderProcessID,
                        _workOrder.WorkOrderID,
                        _workOrder.ProcessID,
                        _workOrder.EmployeeID,
                        _workOrder.EquipmentID,
                        _workOrder.ProductID,
                        _workOrder.OrderQty))
                    {
                        if (workReportForm.ShowDialog() == DialogResult.OK)
                        {
                            LoadWorkOrders(_workOrder.EmployeeID);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("올바른 행을 선택하세요.");
            }


        }

        // 작업 시작
        private void StartButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("작업을 시작하시겠습니까?", "확인", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {

                // 작업 지시 시작
                if (_workOrderServices.StartWorkOrderProcess(_workOrder.EmployeeID, _workOrder.WorkOrderID, _workOrder.WorkOrderProcessID))
                {
                    MessageBox.Show("작업 지시가 시작되었습니다.");
                }
                else
                {
                    MessageBox.Show("작업 지시 시작에 실패했습니다. 다시 시도해주세요.");
                }
                    LoadWorkOrders(_workOrder.EmployeeID); // 작업 지시 목록 새로 고침
            }



            // 대기 상태의 작업만 가능
            /*생산흐름 상태 업데이트
            string query = @"UPDATE WorkOrderProcess
                             SET AssignedUserID = @AssignedUserID, StartTime = @StartTime, Status = '진행 중'
                             WHERE WorkOrderProcessID = @WorkOrderProcessID AND Status = '대기'";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@AssignedUserID", SqlDbType.Int) { Value = _userID },
                new SqlParameter("@StartTime", SqlDbType.DateTime) { Value = DateTime.Now },
                new SqlParameter("@WorkOrderProcessID", SqlDbType.Int) { Value = _workOrderProcessID }
            };
            try
            {
                int affectedRows = DBHelper.ExecuteNonQuery(query, parameters);
                if (affectedRows > 0)
                {
                    MessageBox.Show("작업 지시가 시작되었습니다.");
                    LoadWorkOrders(_userID); // 작업 지시 목록 새로 고침
                }
                else
                {
                    MessageBox.Show("작업 지시 시작에 실패했습니다. 다시 시도해주세요.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }
            */
            /*생산지시 상태 업데이트
            string query2 = @"UPDATE WorkOrders
                             SET Status = '진행 중'
                             WHERE WorkOrderID = @WorkOrderID";
            SqlParameter[] parameters2 = new SqlParameter[]
            {
                new SqlParameter(@"WorkOrderID", SqlDbType.Int) { Value = int.Parse(WorkOrderID.Text) }
            };
            try
            {
                int affectedRows2 = DBHelper.ExecuteNonQuery(query2, parameters2);
                if (affectedRows2 > 0)
                {
                    MessageBox.Show("작업 지시 상태가 '진행 중'으로 업데이트되었습니다.");
                }
                else
                {
                    MessageBox.Show("작업 지시 상태 업데이트에 실패했습니다. 다시 시도해주세요.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);

            }
            */
            /* 설비상태 업데이트
            string query3 = @"UPDATE e
                            SET e.Status = '가동'
                            FROM Equipment e
                            JOIN WorkOrderProcess wop ON e.EquipmentID = wop.EquipmentID
                            JOIN WorkOrders wds ON wop.WorkOrderID = wds.WorkOrderID
                            WHERE wop.WorkOrderProcessID = @WorkOrderProcessID
                            ";
            SqlParameter[] parameters3 = new SqlParameter[]
            {
                new SqlParameter("@WorkOrderProcessID", SqlDbType.Int) { Value = _workOrderProcessID }
            };

            try
            {
                int affectedRows3 = DBHelper.ExecuteNonQuery(query3, parameters3);
                if (affectedRows3 > 0)
                {
                    MessageBox.Show("설비 가동");
                }
                else
                {
                    MessageBox.Show("설비 가동 실패");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);

            }
            */

        }
        //작업 종료
        private void EndButton_Click(object sender, EventArgs e)
        {
            
            LoadWorkOrders(_workOrder.EmployeeID); // 작업 지시 목록 새로 고침

            /* 현재 공정 작업 완료
            string query = @"UPDATE WorkOrderProcess
                             SET EndTime = @EndTime, Status = '완료'
                             WHERE WorkOrderID = @WorkOrderID";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EndTime", SqlDbType.DateTime) { Value = DateTime.Now },
                new SqlParameter("@WorkOrderID", SqlDbType.Int) { Value = int.Parse(WorkOrderID.Text) }
            };

            try
            {
                int affectedRows = DBHelper.ExecuteNonQuery(query, parameters);
                if (affectedRows > 0)
                {
                    MessageBox.Show("작업 지시가 완료되었습니다.");
                    LoadWorkOrders(); // 작업 지시 목록 새로 고침
                }
                else
                {
                    MessageBox.Show("작업 지시 완료에 실패했습니다. 다시 시도해주세요.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }
            */
            /* 생산지시 상태 업데이트 쿼리문 작성
            string query2 = @"UPDATE WorkOrders
                             SET Status = '완료'
                             WHERE WorkOrderID = @WorkOrderID";
            SqlParameter[] parameters2 = new SqlParameter[]
            {
            new SqlParameter(@"WorkOrderID", SqlDbType.Int) { Value = int.Parse(WorkOrderID.Text) }
            };
            try
            {                 int affectedRows2 = DBHelper.ExecuteNonQuery(query2, parameters2);
                if (affectedRows2 > 0)
                {
                    MessageBox.Show("작업 지시 상태가 '완료'로 업데이트되었습니다.");
                }
                else
                {
                    MessageBox.Show("작업 지시 상태 업데이트에 실패했습니다. 다시 시도해주세요.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }
            */
            /* 설비 가동 종료 쿼리문 작성
            string query3 = @"UPDATE e
                            SET e.Status = '대기', e.LastUsedTime = @LastUsedTime
                            FROM Equipment e
                            JOIN WorkOrderProcess wop ON e.EquipmentID = wop.EquipmentID
                            JOIN WorkOrders wds ON wop.WorkOrderID = wds.WorkOrderID
                            WHERE wds.WorkOrderID = @WorkOrderID
                            ";
            SqlParameter[] parameters3 = new SqlParameter[]
            {
                new SqlParameter("@WorkOrderID", SqlDbType.Int) { Value = int.Parse(WorkOrderID.Text) },
                new SqlParameter("@LastUsedTime", SqlDbType.DateTime) { Value = DateTime.Now }
            };

            try
            {
                int affectedRows3 = DBHelper.ExecuteNonQuery(query3, parameters3);
                if (affectedRows3 > 0)
                {
                    MessageBox.Show("설비 가동 종료");
                }
                else
                {
                    MessageBox.Show("설비 가동 종료 실패");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);

            }
            */

        }
        #endregion


        
    }
}
