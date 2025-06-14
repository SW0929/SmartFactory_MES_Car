using MES_SW.Worker.WorkerUserControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mes
{
    public partial class WorkerForm : Form
    {
        private int _UserID;
        private string _UserName;
        public WorkerForm(int UserID, string UserName)
        {
            InitializeComponent();
            timer1.Start(); // 타이머 시작
            _UserName = UserName;
            _UserID = UserID; // 작업자 ID 설정
            WorkerName.Text = UserName; // 작업자 이름 설정
        }
        private void LoadControl(UserControl control)
        {

            PanelMain.Controls.Clear();
            control.Dock = DockStyle.Fill;
            PanelMain.Controls.Add(control);


        }

        private void WorkOdersButton_Click(object sender, EventArgs e)
        {
            LoadControl(new UserControl_WorkOrderList(_UserID));
        }

        private void EquipmentStatusButton_Click(object sender, EventArgs e)
        {
            LoadControl(new UserControl_EquipmentList(_UserID));
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit(); // 애플리케이션 종료
        }

        private void DashboardButton_Click(object sender, EventArgs e)
        {
            //관리자와 작업자의 생산 대시보드 현황은 동일.
            LoadControl(new MES_SW.Admin.UserControl_Dashboard());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CurrentTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss"); // 현재 시간을 "yyyy-MM-dd HH:mm:ss" 형식으로 표시
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            // 로그아웃 처리
            this.Tag = "Logout";  // Logout임을 표시
            this.Close();         // 폼 닫기 → 다시 LoginForm 띄움
        }
    }
}
