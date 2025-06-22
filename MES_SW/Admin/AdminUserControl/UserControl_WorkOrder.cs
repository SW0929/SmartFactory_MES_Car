using mes;
using MES_SW.Admin.Models;
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
// 수정 필요***************************************************
namespace MES_SW.Admin
{

    public partial class UserControl_WorkOrder : UserControl
    {
        // 사용자 ID와 이름을 로그인한 정보를 가져오기 위해 생성자에 파라미터 추가
        private int _adminID; // 관리자 ID
        public UserControl_WorkOrder(int AdminID, string AdminName)
        {

            InitializeComponent();
            _adminID = AdminID; // 관리자 ID 설정
            AdminNameLabel.Text = _adminID.ToString(); // 관리자 이름 설정 (예시로 EmployeeID로 설정, 실제로는 로그인 정보에서 가져와야 함) ******************수정 필요
            
            StartDateTimePicker.Value = DateTime.Now; // 시작 날짜를 현재 시간으로 설정
        }

        #region Load_Methods

        private void UserControl_WorkOrder_Load(object sender, EventArgs e)
        {
            LoadWorkOrders(); // 작업 지시 로드
            LoadProductName(); // 제품 이름 로드
        }

        public void LoadWorkOrders()
        {
            string query = @"
                SELECT w.WorkOrderID, w.ProductID, w.OrderQty, w.StartDate, w.IssueBy, w.Status
                FROM WorkOrders w
                --JOIN WorkOrderProcess wop ON wop.WorkOrderID = w.WorkOrderID
                --WHERE wop.Status = '대기' OR wop.Status = '진행 중'
                ORDER BY w.StartDate ASC";
            dataGridView1.DataSource = DBHelper.ExecuteDataTable(query);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        //제품 이름을 로드하는 메서드
        public void LoadProductName()
        {
            //ProductNameComboBox.Items.Clear(); // 기존 아이템 초기화

            string query = "SELECT ProductID, Name FROM Product";

            using (SqlConnection conn = DBHelper.GetConnection()) // DBHelper로 커넥션 획득
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductItem item = new ProductItem()
                        {
                            ProductID = reader.GetInt32(0),
                            ProductName = reader.GetString(1)
                        };

                        ProductNameComboBox.Items.Add(item);
                        //productList.Add(item); // 제품 목록에 추가
                    }

                }
            }

            if (ProductNameComboBox.Items.Count > 0)
                ProductNameComboBox.SelectedIndex = 0; // 기본 선택값 설정
        }

        #endregion



        #region Event_Handlers
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                // enum 으로 빼서 처리해도 될 것 같음.
                switch (row.Cells["ProductID"].Value.ToString())
                {
                    case "1":
                        ProductNameComboBox.SelectedIndex = 0; // 소나타
                        break;
                    case "2":
                        ProductNameComboBox.SelectedIndex = 1; // 아반떼
                        break;
                    case "3":
                        ProductNameComboBox.SelectedIndex = 2; // 투싼
                        break;
                    case "4":
                        ProductNameComboBox.SelectedIndex = 3; // 펠리세이드
                        break;
                    case "5":
                        ProductNameComboBox.SelectedIndex = 4; // 그랜저
                        break;
                    case "6":
                        ProductNameComboBox.SelectedIndex = 5; // 베뉴
                        break;
                    case "7":
                        ProductNameComboBox.SelectedIndex = 6; // 싼타페
                        break;
                    case "8":
                        ProductNameComboBox.SelectedIndex = 7; // 아이오닉 5
                        break;
                    default:
                        ProductNameComboBox.SelectedIndex = -1; // 선택 해제
                        break;
                }
                QuantityTextBox.Text = row.Cells["OrderQty"].Value.ToString();
                StartDateTimePicker.Value = Convert.ToDateTime(row.Cells["StartDate"].Value);
                AdminNameLabel.Text = row.Cells["IssueBy"].Value.ToString();
                WorkOrderIDLabel.Text = row.Cells["WorkOrderID"].Value.ToString();

                string status = row.Cells["Status"].Value.ToString();
                if (status.Equals("대기"))
                {
                    StatusColour.BackColor = Color.Orange;
                }
                else if (status.Equals("진행 중"))
                {
                    StatusColour.BackColor = Color.Blue;
                }
                else if (status.Equals("완료"))
                {
                    StatusColour.BackColor = Color.Green;
                }
                else
                {
                    StatusColour.BackColor = Color.Red; // 오류 상태
                }

            }
            else
            {
                return; // 유효하지 않은 행 인덱스인 경우 아무 작업도 하지 않음
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {

            /*
            OUTPUT INSERTED.ID는 SCOPE_IDENTITY()와 같은 역할을 하지만, 더 안전하고 강력한 방식으로 INSERT 시 생성된 ID 값을 바로 반환받을 수 있게 해주는 SQL 문법입니다. 
            특히 **트리거(trigger)**가 있는 테이블에서 SCOPE_IDENTITY()가 의도대로 동작하지 않는 경우에도 안전하게 사용할 수 있어요.
            */
            string workOrderQuery = @"INSERT INTO WorkOrders (ProductID, OrderQty, StartDate, IssueBy)
                             OUTPUT INSERTED.WorkOrderID
                             VALUES(@ProductID, @OrderQty, @StartDate, @IssueBy);";

            SqlParameter[] workOrderParameters = new SqlParameter[]
            {
                new SqlParameter("@ProductID", ((ProductItem)ProductNameComboBox.SelectedItem).ProductID),
                new SqlParameter("@OrderQty", QuantityTextBox.Text),
                new SqlParameter("@StartDate", StartDateTimePicker.Value),
                new SqlParameter("@IssueBy", _adminID)
            };

            //TODO : 현재 설비가 중복으로 선택되는 문제가 발생 해결 필요****************************************************
            int __processId = 1; // 실제 공정에 맞게 동적으로 결정
            int selectedEquipmentId = DBHelper.GetAvailableEquipmentId(__processId);

            if (selectedEquipmentId == 0)
            {
                MessageBox.Show("사용 가능한 설비가 없습니다.");
                return;
            }

            //WorkOrderProcess 테이블에 작업 지시 추가
            string workOrderProcessQuery = @"INSERT 
                              INTO WorkOrderProcess (WorkOrderID, ProcessID, EquipmentID, Status)
                              VALUES(@WorkOrderID, 1, @EquipmentID, '대기')";
            SqlParameter[] workOrderProcessParameters = new SqlParameter[]
            {
                new SqlParameter("@WorkOrderID", 0), // 마지막으로 추가된 작업 지시 ID
                new SqlParameter("@EquipmentID", selectedEquipmentId) // 선택된 설비 ID
                
            };

            string equipmentQuery = "UPDATE Equipment SET Status = '할당 대기' WHERE EquipmentID = @EquipmentID";
            SqlParameter[] equipmentParameters = new SqlParameter[]
            {
                new SqlParameter("@EquipmentID", selectedEquipmentId)
            };
            // 실행
            try
            {
                int workOrderId = DBHelper.InsertWorkOrderWithProcess(workOrderQuery, workOrderParameters, workOrderProcessQuery, workOrderProcessParameters, equipmentQuery, equipmentParameters);
                LoadWorkOrders(); // 작업 지시 목록 새로 고침
                MessageBox.Show($"작업지시 등록 성공! ID: {workOrderId}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("등록 실패: " + ex.Message);
            }

            

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            // 작업 지시는 로그인한 사용자로 수정되기 때문에 사용자ID는 고정
            string query = @"UPDATE WorkOrders SET ProductID = @ProductID, OrderQty = @OrderQty, StartDate = @StartDate, IssueBy = @IssueBy WHERE WorkOrderID = @WorkOrderID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ProductID", ((ProductItem)ProductNameComboBox.SelectedItem).ProductID),
                new SqlParameter("@OrderQty", QuantityTextBox.Text),
                new SqlParameter("@StartDate", StartDateTimePicker.Value),
                new SqlParameter("@WorkOrderID", WorkOrderIDLabel.Text),
                new SqlParameter("@IssueBy", AdminNameLabel.Text)
            };
            try
            {
                int rowsAffected = DBHelper.ExecuteNonQuery(query, parameters);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("생산 지시가 업데이트되었습니다.");
                    LoadWorkOrders(); // 작업 지시 목록 새로 고침
                }
                else
                {
                    MessageBox.Show("생산 지시 업데이트에 실패했습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string workOrderId = WorkOrderIDLabel.Text;

            // 1. 먼저 WorkOrderProcess에서 해당 작업지시 삭제
            string deleteProcessQuery = @"DELETE FROM WorkOrderProcess WHERE WorkOrderID = @WorkOrderID";
            SqlParameter[] processParams =
            {
                new SqlParameter("@WorkOrderID", workOrderId)
            };

            // 2. 그 다음 WorkOrders 삭제
            string deleteOrderQuery = @"DELETE FROM WorkOrders WHERE WorkOrderID = @WorkOrderID";
            SqlParameter[] orderParams =
            {
                new SqlParameter("@WorkOrderID", workOrderId)
            };

            try
            {
                DBHelper.ExecuteNonQuery(deleteProcessQuery, processParams);
                int rowsAffected = DBHelper.ExecuteNonQuery(deleteOrderQuery, orderParams);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("생산 지시가 삭제되었습니다.");
                    LoadWorkOrders(); // 목록 새로고침
                }
                else
                {
                    MessageBox.Show("생산 지시 삭제에 실패했습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }
        }
        private void UserControl_WorkOrder_Click(object sender, EventArgs e)
        {
            ProductNameComboBox.SelectedIndex = -1;
            QuantityTextBox.Clear();
            StartDateTimePicker.Value = DateTime.Now; // 시작 날짜를 현재 시간으로 설정
            AdminNameLabel.Text = string.Empty; // 관리자 이름 초기화
            WorkOrderIDLabel.Text = string.Empty; // 작업 지시 ID 초기화
            StatusColour.BackColor = SystemColors.Control; // 상태 색상 초기화

        }
        #endregion

        
    }
}
