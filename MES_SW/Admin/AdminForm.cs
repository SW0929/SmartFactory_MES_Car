using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MES_SW.Admin;

namespace mes
{
    public partial class AdminForm : Form
    {
        private string _UserID;
        private string _UserName;
        // TODO : 로그인 한 사용자 이름, 사번 좌측 상단에 표시
        public AdminForm(string UserID, string UserName)
        {
            
            InitializeComponent();
            _UserID = UserID;
            _UserName = UserName;
            AdminName.Text = _UserName; // 관리자 이름 설정 (예시로 EmployeeID로 설정, 실제로는 로그인 정보에서 가져와야 함)
            timer1.Start();
            //string query = "SELECT EmployeeID, UserName FROM Users WHERE UserName = @UserName";
        }

        

        #region methods
        private void LoadControl(UserControl control)
        {

            panelMain.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelMain.Controls.Add(control);


        }

        private void UsersButton_Click(object sender, EventArgs e)
        {
            LoadControl(new UserControl_UserManager());
        }

        private void OrdersButton_Click(object sender, EventArgs e)
        {
            LoadControl(new UserControl_WorkOrder(_UserID, _UserName));
        }

        private void DashboardButton_Click(object sender, EventArgs e)
        {
            LoadControl(new UserControl_Dashboard());
        }

        private void EquipmentButton_Click(object sender, EventArgs e)
        {
            LoadControl(new UserControl_Equipment());
        }

        private void EquipmentDefectButton_Click(object sender, EventArgs e)
        {
            LoadControl(new UserControl_EquipmentDefect());
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();

        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            CurrentTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        
    }
}
