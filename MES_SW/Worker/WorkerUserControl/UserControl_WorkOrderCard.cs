using MES_SW.Services.Worker;
using MES_SW.Worker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MES_SW.Worker.WorkerUserControl
{
    public partial class UserControl_WorkOrderCard : UserControl
    {
        public Action? OnWorkStartedCallback; // 콜백 선언
        private int workerID;
        private WorkOrder _workOrder;
        private WorkOrderServices _workOrderServices;
        public UserControl_WorkOrderCard(int workerID, WorkOrder workOrder)
        {
            InitializeComponent();
            _workOrderServices = new WorkOrderServices();
            this.workerID = workerID;
            _workOrder = workOrder;
            
           
        }

        public void SetData(WorkOrder workOrder, string processName, string productName)
        {
            this.Tag = workOrder;

            WorkOrderIDLabel.Text = $"작업번호 : {workOrder.WorkOrderID}";
            ProcessName.Text = $"🔧 [공정명] {processName}";
            ProductName.Text = $"📦 제품 : {productName}";
            QtyLabel.Text = $"📋 수량 : {workOrder.OrderQty}";
            OrderDate.Text = $"📅 지시일자 : {workOrder.StartDate:yyyy-MM-dd}";
            IssuByLabel.Text = $"👤 지시자 : {workOrder.IssuedByName}";
            StatusLabel.Text = $"🔄 상태 : {workOrder.Status}";

            int progress = CalculateProgress(workOrder.OrderQty);
            progressBar1.Value = Math.Clamp(progress, 0, 100);

            AddDoubleClickToAllControls(this); // 내부 컨트롤에도 이벤트 적용
        }

        private int CalculateProgress(int OrderQty)
        {
            Random random = new Random();
            int finishQty = random.Next(1,5);
            int result = 0;
            result += finishQty;

            if (result >= OrderQty) return 100;

            double percent = (double)result / OrderQty * 100;
            return (int)Math.Clamp(percent, 0, 100);
        }

        private void UserControl_WorkOrderCard_DoubleClick(object? sender, EventArgs e)
        {
            if (this.Tag is WorkOrder workOrder)
            {
                var form = new WorkPerformanceForm(workerID, workOrder);
                form.ShowDialog();
            }
        }

        private void AddDoubleClickToAllControls(Control control)
        {
            foreach (Control child in control.Controls)
            {
                child.DoubleClick += UserControl_WorkOrderCard_DoubleClick;

                // 자식의 자식도 있는 경우 재귀적으로 적용
                if (child.HasChildren)
                    AddDoubleClickToAllControls(child);
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("작업을 시작하시겠습니까?", "확인", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {

                // 작업 지시 시작
                if (_workOrderServices.StartWorkOrderProcess(workerID, _workOrder.WorkOrderID, _workOrder.WorkOrderProcessID))
                {
                    MessageBox.Show("작업 지시가 시작되었습니다.");
                    OnWorkStartedCallback?.Invoke(); // 작업 지시 목록 새로 고침
                }
                else
                {
                    MessageBox.Show("작업 지시 시작에 실패했습니다. 다시 시도해주세요.");
                }
                
            }
        }

    }
}
