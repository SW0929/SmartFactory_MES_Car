using mes;
using MES_SW.DB;
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
        private string _userID;
        //private string _workOrderID;
        public UserControl_WorkOrderList(string UserID)
        {
            InitializeComponent();
            _userID = UserID;
            LoadWorkOrders();
        }
        #region Load_Methods
        private void LoadWorkOrders()
        {
            // 작업 지시 불러올 때 해당 부서에 맞게 불러오게 하기, 부서 처리는 나중에 꼭 추가하기
            /*
             JOIN Process pc ON pc.Name = u.Department
             WHERE pc.Name = u.Department"
             */

            string query = @"SELECT w.WorkOrderID, p.Name AS 제품명, w.OrderQty AS 주문수량, w.StartDate AS 지시날짜, u.UserName AS 지시자, wop.Status AS 진행상태
                            FROM WorkOrders w
                            JOIN Product p ON p.ProductID = w.ProductID
                            JOIN WorkOrderProcess wop ON wop.WorkOrderID = w.WorkOrderID
                            JOIN Users u ON u.EmployeeID = w.IssueBy";

            dataGridView1.DataSource = DBHelper.ExecuteDataTable(query);
            dataGridView1.Columns["WorkOrderID"].Visible = false; // WorkOrderID 열 숨기기
        }

        #endregion

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                WorkOrderID.Text = row.Cells["WorkOrderID"].Value.ToString();
            }
            else
            {
                // 클릭한 셀이 유효하지 않으면 종료
                return;
            }
        }
        /*
        쿼리문에서 조인을 사용하면 DB에 저장된 값들을 활용하는 것이고
        parameter를 사용하면 Form이나 UserControl에서 입력한 값을 활용하는 것이다.
        */
        private void StartButton_Click(object sender, EventArgs e)
        {
            string query = @"UPDATE WorkOrderProcess
                             SET AssignedUserID = @AssignedUserID, StartTime = @StartTime, Status = '진행 중'
                             WHERE WorkOrderID = @WorkOrderID";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@AssignedUserID", SqlDbType.Int) { Value = int.Parse(_userID) },
                new SqlParameter("@StartTime", SqlDbType.DateTime) { Value = DateTime.Now },
                new SqlParameter("@WorkOrderID", SqlDbType.Int) { Value = int.Parse(WorkOrderID.Text) }
            };
            try
            {
                int affectedRows = DBHelper.ExecuteNonQuery(query, parameters);
                if (affectedRows > 0)
                {
                    MessageBox.Show("작업 지시가 시작되었습니다.");
                    LoadWorkOrders(); // 작업 지시 목록 새로 고침
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

            string query3 = @"UPDATE e
                            SET e.Status = '가동'
                            FROM Equipment e
                            JOIN WorkOrderProcess wop ON e.EquipmentID = wop.EquipmentID
                            JOIN WorkOrders wds ON wop.WorkOrderID = wds.WorkOrderID
                            WHERE wds.WorkOrderID = @WorkOrderID
                            ";
            SqlParameter[] parameters3 = new SqlParameter[]
            {
                new SqlParameter("@WorkOrderID", SqlDbType.Int) { Value = int.Parse(WorkOrderID.Text) }
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


        }

        private void EndButton_Click(object sender, EventArgs e)
        {
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

        }
        
    }
}
