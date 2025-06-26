using mes;
using MES_SW.Admin.Models;
using MES_SW.Admin.Models.Items;
using MES_SW.Data;
using MES_SW.Services.Admin;
using MES_SW.Services.Common;
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
    // 수량 최소 1개 이상 적어야 함 (예외 처리 필요) + 빈 칸 X
    public partial class UserControl_WorkOrder : UserControl
    {
        private ProductService _productService;
        private WorkOrderService _workOrderService;
        private WorkOrder? _workOrder;

        // 사용자 ID와 이름을 로그인한 정보를 가져오기 위해 생성자에 파라미터 추가
        private int _adminID; // 관리자 ID
        public UserControl_WorkOrder(int AdminID, string AdminName)
        {

            InitializeComponent();
            _productService = new ProductService();
            _workOrderService = new WorkOrderService();
            _workOrder = new WorkOrder();
            _adminID = AdminID; // 관리자 ID 설정

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
            dataGridView1.DataSource = _workOrderService.GetWorkOrder();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        //제품 이름을 로드하는 메서드
        public void LoadProductName()
        {
            var productList = _productService.GetProducts();

            ProductNameComboBox.DataSource = productList;
            ProductNameComboBox.DisplayMember = "ProductName";
            ProductNameComboBox.ValueMember = "ProductID";

            if (ProductNameComboBox.Items.Count > 0)
                ProductNameComboBox.SelectedIndex = 0; // 기본 선택값 설정
        }

        #endregion



        #region Event_Handlers
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                SetWorkOrderFormFieldsFromRow(row);
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터를 불러오는 중 오류 발생: " + ex.Message);
            }
            
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            _workOrder = GetWorkOrderFromInput();
            if (_workOrder == null) return; // 유효성 실패 시 중단
            try
            {
                bool solvedCheck = _workOrderService.InsertWorkOrder(_workOrder);

                if (solvedCheck)
                {
                    MessageBox.Show($"작업지시 등록 성공! ID: {_workOrder.WorkOrderID}");
                    LoadWorkOrders(); // 작업 지시 목록 새로 고침
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"처리 중 오류 발생: {ex.Message}");
                // 필요시 로그로 남기기
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            _workOrder = GetWorkOrderFromInput();
            if (_workOrder == null) return; // 유효성 실패 시 중단
            // 작업 지시는 로그인한 사용자로 수정되기 때문에 사용자ID는 고정

            try
            {
                int rowsAffected = _workOrderService.UpdateWorkOrder(_workOrder);
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
            _workOrder = GetWorkOrderFromInput();
            if (_workOrder == null) return; // 유효성 실패 시 중단
            try
            {
                bool solvedCheck = _workOrderService.DeleteWorkOrder(_workOrder);

                if (solvedCheck)
                {
                    MessageBox.Show($"생산 지시가 삭제되었습니다. ID: {_workOrder.WorkOrderID}");
                    LoadWorkOrders(); // 작업 지시 목록 새로 고침
                }
                else
                {
                    MessageBox.Show("생산 지시 삭제에 실패했습니다.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"처리 중 오류 발생: {ex.Message}");
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
        private WorkOrder? GetWorkOrderFromInput()
        {
            // 작업지시 ID
            int workOrderId = int.TryParse(WorkOrderIDLabel.Text, out int id) ? id : 0;

            // 수량 입력 검증
            if (!int.TryParse(QuantityTextBox.Text, out int qty))
            {
                MessageBox.Show("수량은 숫자만 입력해야 합니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            // 제품 ID 추출
            if (ProductNameComboBox.SelectedValue is not int productId)
            {
                MessageBox.Show("제품을 선택해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            // 최종 WorkOrder 객체 생성
            return new WorkOrder
            {
                WorkOrderID = workOrderId,
                ProductID = productId,
                OrderQty = qty,
                StartDate = StartDateTimePicker.Value,
                IssueBy = _adminID
            };
        }
        private void SetWorkOrderFormFieldsFromRow(DataGridViewRow row)
        {
            WorkOrderIDLabel.Text = row.Cells["WorkOrderID"].Value.ToString();
            ProductNameComboBox.SelectedValue = Convert.ToInt32(row.Cells["ProductID"].Value);
            QuantityTextBox.Text = row.Cells["OrderQty"].Value.ToString();
            StartDateTimePicker.Value = Convert.ToDateTime(row.Cells["StartDate"].Value);
            AdminNameLabel.Text = row.Cells["IssueBy"].Value.ToString();
            string? status = row.Cells["Status"].Value?.ToString();
            //StatusColour.Text = status;
            StatusColour.BackColor = status switch
            {
                "대기" => Color.Yellow,
                "진행 중" => Color.Green,
                "완료" => Color.Blue,
                _ => SystemColors.Control
            };
        }

    }
}
