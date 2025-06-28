using MES_SW.Services.Worker;
using MES_SW.Worker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private int progress = 0;

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

            //if (!workOrder.Status.Equals("대기"))
            //{
            //    int progress = CalculateProgress(workOrder.OrderQty, workOrder.Status);
            //    progressBar1.Value = Math.Clamp(progress, 0, 100);
            //}

            // 상태가 '진행 중'일 때만 애니메이션 시작
            switch (workOrder.Status)
            {
                case "진행 중":
                    StartProgressAnimation(); // 애니메이션 진행
                    break;

                case "완료":
                    progress = 100;
                    progressBar1.Value = 100; // 꽉 찬 상태
                    break;

                default:
                    progressBar1.Value = 0; // 초기 상태
                    break;
            }

            StatusCheck(workOrder.Status);

            AddDoubleClickToAllControls(this); // 내부 컨트롤에도 이벤트 적용
        }



        private void UserControl_WorkOrderCard_DoubleClick(object? sender, EventArgs e)
        {
            if (this.Tag is WorkOrder workOrder && !workOrder.Status.Equals("대기"))
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
                    //StartProgressAnimation();
                    OnWorkStartedCallback?.Invoke(); // 작업 지시 목록 새로 고침
                }
                else
                {
                    MessageBox.Show("작업 지시 시작에 실패했습니다. 다시 시도해주세요.");
                }

            }
        }
        private void StatusCheck(string Status)
        {
            switch (Status)
            {
                case "대기":
                    StartButton.Text = "시작";
                    StartButton.Enabled = true;
                    StartButton.BackColor = Color.SteelBlue;
                    this.BackColor = Color.Gainsboro;
                    break;

                case "진행 중":
                    StartButton.Text = "진행 중...";
                    StartButton.Enabled = false;
                    StartButton.BackColor = Color.LightGoldenrodYellow;
                    this.BackColor = Color.LemonChiffon;
                    break;

                case "완료":
                    StartButton.Text = "완료됨";
                    StartButton.Enabled = false;
                    StartButton.BackColor = Color.LightGreen;
                    this.BackColor = Color.Honeydew;
                    break;

                default:
                    StartButton.Text = "시작";
                    StartButton.Enabled = false;
                    StartButton.BackColor = SystemColors.Control;
                    this.BackColor = SystemColors.Control;
                    break;
            }
        }


        #region 프로그레스바
        // 지금은 시간으로 처리 함. 실제 공정에서는 완료 될 때 마다 올라가는 구조
        // 자동 생산이 불가하기 때문에 임의로 해놓음 -- 나중에 처리 하기
        private int CalculateProgress(int OrderQty, string Status)
        {
            Random random = new Random();
            int finishQty = random.Next(1, 5);
            int result = 0;
            result += finishQty;

            if (result >= OrderQty || Status.Equals("완료")) return 100;

            double percent = (double)result / OrderQty * 100;
            return (int)Math.Clamp(percent, 0, 100);
        }
        private void StartProgressAnimation()
        {
            progress = 0;
            progressBar1.Value = 0;

            progressTimer = new System.Windows.Forms.Timer();
            progressTimer.Interval = 500; // 0.5초 간격
            progressTimer.Tick += progressTimer_Tick;
            progressTimer.Start();
        }
        

        private void progressTimer_Tick(object? sender, EventArgs e)
        {
            if (progress >= 100)
            {
                progressTimer.Stop();
                return;
            }

            progress += 10; // 10%씩 증가 (속도 조절 가능)
            progressBar1.Value = Math.Min(progress, 100);
        }
        #endregion
    }
}
