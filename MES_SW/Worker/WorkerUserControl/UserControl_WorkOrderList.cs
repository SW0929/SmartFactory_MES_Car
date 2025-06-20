using mes;
using MES_SW.Data;
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
        private int _userID; // 작업자 ID
        private int _workOrderProcessID; // 작업 지시 공정 ID
        private int _processID; // 공정 ID
        private int _workOrderID; // 작업 지시 ID
        private int _equipmentID; // 설비 ID
        private int _productID; // 제품 ID
        private int _orderQty = 0; // 수량
        //private string _status = null; // 진행 상태

        public UserControl_WorkOrderList(int UserID)
        {
            InitializeComponent();
            _userID = UserID;
            LoadWorkOrders(_userID);
        }
        #region Load_Methods
        private void LoadWorkOrders(int _userID)
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
            // 작업자 EmployeeID
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", SqlDbType.Int) { Value = _userID }
            };

            dataGridView1.DataSource = DBHelper.ExecuteDataTable(query, parameters);
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
                // 전역 변수로 빼도 될듯
                _workOrderID = (int)row.Cells["WorkOrderID"].Value; // 작업 지시 ID
                _workOrderProcessID = (int)row.Cells["WorkOrderProcessID"].Value;
                _processID = (int)row.Cells["ProcessID"].Value;
                _productID = (int)row.Cells["ProductID"].Value; // 제품 ID
                _equipmentID = (int)row.Cells["EquipmentID"].Value; // 설비 ID
                _orderQty = (int)row.Cells["주문수량"].Value; // 주문 수량
                //_status = row.Cells["진행상태"].Value.ToString(); // 진행 상태
                WorkOrderID.Text = _workOrderID.ToString(); // TextBox에 작업 지시 ID 표시
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
                // 선택한 행의 상태가 '진행 중' 인 경우에만 작업 보고서 폼을 표시
                if (row.Cells["진행상태"].Value.Equals("진행 중"))
                {
                    WorkPerformanceForm workReportForm = new WorkPerformanceForm(_workOrderProcessID, _workOrderID, _processID, _userID, _equipmentID, _productID, _orderQty);
                    workReportForm.ShowDialog(); // 작업 보고서 폼을 모달로 표시
                }
            }
            LoadWorkOrders(_userID); // 작업 지시 목록 새로 고침


        }




        // 작업 시작
        private void StartButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("작업을 시작하시겠습니까?", "확인", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DBHelper.StartWorkOrderProcess(_userID, _workOrderID, _workOrderProcessID);
                LoadWorkOrders(_userID); // 작업 지시 목록 새로 고침
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
            
            LoadWorkOrders(_userID); // 작업 지시 목록 새로 고침

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
