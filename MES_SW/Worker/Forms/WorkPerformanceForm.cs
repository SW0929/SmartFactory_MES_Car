using MES_SW.Data;
using MES_SW.Data.Worker;
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

namespace MES_SW.Worker.WorkerUserControl
{
    // TODO : 실적 입력 할 때 맨앞에 0이 들어가는 것 예외처리 해야함. ex) 0001, 0112 
    public partial class WorkPerformanceForm : Form
    {
        private bool isSaved = false; // 저장 여부를 추적하는 변수
        private WorkOrderPerformance _workOrderPerformance;
        private WorkPerformanceService _workPerformanceService;


        public WorkPerformanceForm(int workOrderProcessID, int workOrderID, int processID, int employeeID, int equipmentID, int productID, int orderQty)
        {
            InitializeComponent();
            // 생성자에서 매개변수로 받은 값을 WorkOrderPerformance 객체에 저장
            _workOrderPerformance = new WorkOrderPerformance
            {
                WorkOrderProcessID = workOrderProcessID,
                WorkOrderID = workOrderID,
                ProcessID = processID,
                EmployeeID = employeeID,
                EquipmentID = equipmentID,
                ProductID = productID,
                OrderQty = orderQty
            };
            _workPerformanceService = new WorkPerformanceService(new WorkOrderPerformanceRepository());
            LoadWorkPerformanceForm();
            GQtyTextBox.MaxLength = _workOrderPerformance.OrderQty.ToString().Length;
        }

        private void LoadWorkPerformanceForm()
        {
            
            var info = _workPerformanceService.GetPerformanceLabelInfo(_workOrderPerformance);

            WorkOrderNumLabel.Text = "생산지시 번호 : " + _workOrderPerformance.WorkOrderID.ToString();
            TotalOrderLabel.Text = string.Format("총 생산 수량 : {0}", _workOrderPerformance.OrderQty);
            ProcessNameLabel.Text = "공정명 : " + info["ProcessName"].ToString();
            EquipmentNameLabel.Text = "설비명 : " + info["EquipmentName"].ToString();
            ProductNameLabel.Text = "제품명 : " + info["ProductName"].ToString();
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
                _workOrderPerformance.GoodQty = int.Parse(GQtyTextBox.Text);
                _workOrderPerformance.BadQty = int.Parse(BQtyTextBox.Text);
                _workOrderPerformance.Reason = BadReasonTextBox.Text;
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
                    _workPerformanceService.CompleteWorkOrderProcess(_workOrderPerformance);
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
                if (goodQty > _workOrderPerformance.OrderQty || goodQty < 0)
                {
                    MessageBox.Show("양품 수량이 총 수량보다 많을 수 없습니다.");
                    GQtyTextBox.Text = _workOrderPerformance.OrderQty.ToString();
                    return;
                }

                int defectQty = _workOrderPerformance.OrderQty - goodQty;
                BQtyTextBox.Text = defectQty.ToString();
            }
            else
            {
                BQtyTextBox.Text = string.Empty;
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close(); // 뒤로가기 버튼 클릭 시 폼 닫기
        }
    }
}
