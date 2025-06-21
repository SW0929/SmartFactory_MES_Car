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

namespace MES_SW.Worker.WorkerUserControl
{
    // TODO : 실적 입력 할 때 맨앞에 0이 들어가는 것 예외처리 해야함. ex) 0001, 0112 
    public partial class WorkPerformanceForm : Form
    {
        private bool isSaved = false; // 저장 여부를 추적하는 변수
        private int _workOrderID;
        private int _processID;
        private int _employeeID;
        private int _equipmentID;
        private int _productID;
        private int _workOrderProcessID;
        private int _GoodQty = 0;
        private int _BadQty = 0;
        private string _Reason = string.Empty;
        private int _orderQty = 0;

        public WorkPerformanceForm(int workOrderProcessID, int workOrderID, int processID, int employeeID, int equipmentID, int productID, int orderQty)
        {
            InitializeComponent();
            _workOrderProcessID = workOrderProcessID;
            _workOrderID = workOrderID;
            _processID = processID;
            _employeeID = employeeID;
            _equipmentID = equipmentID;
            _productID = productID;
            _orderQty = orderQty;
            LoadWorkPerformanceForm();
            GQtyTextBox.MaxLength = _orderQty.ToString().Length;
        }

        private void LoadWorkPerformanceForm()
        {
            //WorkOrderNumLabel.Text = string.Format("생산지시 번호 : {0}", _workOrderID);
            //ProcessNameLabel.Text = string.Format("공정 번호 : {0}", _processID);
            //EquipmentNameLabel.Text = string.Format("설비 번호 : {0}", _equipmentID);
            //ProductNameLabel.Text = string.Format("제품 번호 : {0}", _productID); 
            string query = @"SELECT 
                            pr.Name AS ProcessName,
                            eq.Name AS EquipmentName,
                            pd.Name AS ProductName
                            FROM Process pr
                            JOIN Equipment eq ON eq.EquipmentID = @EquipmentID
                            JOIN Product pd ON pd.ProductID = @ProductID
                            WHERE pr.ProcessID = @ProcessID";

            SqlParameter[] parameters = new SqlParameter[]
            {
                        new SqlParameter("@ProcessID", _processID),
                        new SqlParameter("@EquipmentID", _equipmentID),
                        new SqlParameter("@ProductID", _productID)
            };

            DataTable dt = DBHelper.ExecuteDataTable(query, parameters);
            if (dt.Rows.Count > 0)
            {
                WorkOrderNumLabel.Text = "생산지시 번호 : " + _workOrderID.ToString();
                TotalOrderLabel.Text = string.Format("총 생산 수량 : {0}", _orderQty);
                ProcessNameLabel.Text = "공정명 : " + dt.Rows[0]["ProcessName"].ToString();
                EquipmentNameLabel.Text = "설비명 : " + dt.Rows[0]["EquipmentName"].ToString();
                ProductNameLabel.Text = "제품명 : " + dt.Rows[0]["ProductName"].ToString();
            }
        }


        private void WorkReportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 굳이 필요할까?  
            if (!isSaved)
            {
                DialogResult result = MessageBox.Show("저장되지 않은 데이터가 있습니다. 종료하시겠습니까?",
                                                      "종료 확인", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void ReportStoreButton_Click(object sender, EventArgs e)
        {

            // TODO : 실적 등록 로직 구현
            if (string.IsNullOrWhiteSpace(GQtyTextBox.Text) || string.IsNullOrWhiteSpace(BQtyTextBox.Text))
            {
                MessageBox.Show("양품 또는 불량을 채워주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (int.Parse(BQtyTextBox.Text) > 0 && string.IsNullOrWhiteSpace(BadReasonTextBox.Text))
            {
                MessageBox.Show("불량이 있는 경우 불량 사유를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _GoodQty = int.Parse(GQtyTextBox.Text);
                _BadQty = int.Parse(BQtyTextBox.Text);
                _Reason = BadReasonTextBox.Text;
                isSaved = true; // 저장이 완료되었음을 표시
                MessageBox.Show("실적 등록이 완료되었습니다.", "실적 등록");
            }

        }

        private void EndButton_Click(object sender, EventArgs e)
        {
            if (!isSaved)
            {
                MessageBox.Show("실적을 먼저 등록해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("작업을 종료하시겠습니까?", "확인", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DBHelper.CompleteWorkOrderProcess(_workOrderProcessID, _workOrderID, _processID, _employeeID, _GoodQty, _BadQty, _Reason, _productID, _equipmentID);
                    this.Close(); // 작업 종료 후 폼 닫기

                }
            }

        }

        private void GQtyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // 숫자와 백스페이스 외의 키 입력을 무시
            }
        }

        private void BQtyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // 숫자와 백스페이스 외의 키 입력을 무시
            }
        }

        private void GQtyTextBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(GQtyTextBox.Text, out int goodQty))
            {
                if (goodQty > _orderQty || goodQty < 0)
                {
                    MessageBox.Show("양품 수량이 총 수량보다 많을 수 없습니다.");
                    GQtyTextBox.Text = _orderQty.ToString();
                    return;
                }

                int defectQty = _orderQty - goodQty;
                BQtyTextBox.Text = defectQty.ToString();
            }
            else
            {
                BQtyTextBox.Text = string.Empty;
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close(); // 뒤로가기 버튼 클릭 시 폼 닫기
        }
    }
}
