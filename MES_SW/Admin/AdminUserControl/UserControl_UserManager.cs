using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MES_SW.Admin.Models;
using MES_SW.Data;
using MES_SW.Services.Admin;
using Microsoft.Data.SqlClient;

namespace MES_SW.Admin
{
    // 현재 예외처리는 끝났다고 생각함.
    //  TODO : 작업자 1조, 2조 이런 팀 추가 AND 같은 부서의 작업자들만 조회할 수 있도록 수정 필요. (관리자가 같은 부서 처리할 수 있도록)
    public partial class UserControl_UserManager : UserControl
    {
        private Employee _employee;
        private UserManageService _userManageService;

        public UserControl_UserManager()
        {

            InitializeComponent();
            _userManageService = new UserManageService();
            _employee = new Employee();
        }

        #region methods

        private void UserControl_UserManager_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }

        // 작업자 조회
        private void LoadUserData()
        {
            dataGridView1.DataSource = _userManageService.GetUserData();
        }

        // 작업자 클릭 시 데이터 값들이 옆에 채워짐.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[rowIndex];

                EmployeeIdTextBox.Text = row.Cells["EmployeeID"].Value?.ToString() ?? "";
                UserNameTextBox.Text = row.Cells["UserName"].Value?.ToString() ?? "";
                DepartmentTextBox.Text = row.Cells["Department"].Value?.ToString() ?? "";

                string userRole = row.Cells["UserRole"].Value?.ToString() ?? "";
                AdminRadioButton.Checked = userRole == "Admin";
                WorkerRadioButton.Checked = userRole == "Worker";

                object? statusValue = row.Cells["UserStatus"].Value;
                checkBox1.Checked = statusValue is bool b && b;

            }
            else
            {
                // 클릭한 셀이 유효하지 않으면 종료
                return;
            }

        }
        // 빈 곳을 클릭하면 데이터 값 초기화
        private void UserControl_UserManager_Click(object sender, EventArgs e)
        {
            EmployeeIdTextBox.Text = "";
            UserNameTextBox.Text = "";
            DepartmentTextBox.Text = "";
            WorkerRadioButton.Checked = true;
            checkBox1.Checked = false;
        }

        // EmployeeIdTextBox에 숫자와 백스페이스만 입력 가능하도록 설정
        private void EmployeeIdTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // 숫자가 아닌 경우 입력을 막음
            }
        }
        #endregion

        // 작업자 추가
        private void AddUserButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            _employee = GetEmployeeFromInput();

            try
            {
                int result = _userManageService.InsertNewUser(_employee);
                if (result > 0)
                {
                    MessageBox.Show("등록이 완료되었습니다.");
                    LoadUserData();
                }
                else
                {
                    MessageBox.Show("등록 실패. 다시 시도해주세요.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("이미 존재하는 ID입니다.", "Error");
            }

        }

        // 작업자 정보 수정
        private void UpdateUserButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            try
            {
                _employee = GetEmployeeFromInput();
                int rowsAffected = _userManageService.UpdateUser(_employee);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("수정이 완료되었습니다.");
                    LoadUserData();
                }
                else
                {
                    MessageBox.Show("Don't change EmployeeID");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 작업자 삭제
        private void DeleteUserButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmployeeIdTextBox.Text))
            {
                MessageBox.Show("삭제할 ID를 입력하세요.");
                return;
            }

            int employeeID = int.Parse(EmployeeIdTextBox.Text);

            try
            {
                int rowsAffected = _userManageService.DeleteUser(employeeID);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("삭제가 완료되었습니다.");
                    LoadUserData();
                }
                else
                {
                    MessageBox.Show("삭제 실패! 다시 시도해주세요.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // 입력된 사용자 정보 Get
        private Employee GetEmployeeFromInput()
        {
            return new Employee
            {
                EmployeeID = int.Parse(EmployeeIdTextBox.Text),
                Name = UserNameTextBox.Text,
                Department = DepartmentTextBox.Text,
                Role = AdminRadioButton.Checked ? "Admin" : "Worker",
                Status = checkBox1.Checked
            };
        }

        // 사용자 추가/수정 시 사번과 이름 빈 칸 인지 확인
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(EmployeeIdTextBox.Text) || string.IsNullOrWhiteSpace(UserNameTextBox.Text))
            {
                MessageBox.Show("사번과 이름을 입력해주세요.");
                return false;
            }
            return true;
        }

    }
}
