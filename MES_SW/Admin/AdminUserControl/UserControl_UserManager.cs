using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MES_SW.Data;
using Microsoft.Data.SqlClient;

namespace MES_SW.Admin
{
    // 현재 예외처리는 끝났다고 생각함.
    // DB 수정해서 작업자 부서까지 넣어야함 - 완료.
    //  TODO : 작업자 1조, 2조 이런 팀 추가 AND 같은 부서의 작업자들만 조회할 수 있도록 수정 필요. (관리자가 같은 부서 처리할 수 있도록)
    public partial class UserControl_UserManager : UserControl
    {

        public UserControl_UserManager()
        {

            InitializeComponent();
            
        }

        #region methods

        private void UserControl_UserManager_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }
        // 작업자 조회
        private void LoadUserData()
        {
            string query = "SELECT * FROM Users";
            dataGridView1.DataSource = DBHelper.ExecuteDataTable(query);
        }
        // 작업자 클릭 시 데이터 값들이 옆에 채워짐.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                EmployeeIdTextBox.Text = row.Cells["EmployeeID"].Value.ToString();
                UserNameTextBox.Text = row.Cells["UserName"].Value.ToString();
                DepartmentTextBox.Text = row.Cells["Department"].Value.ToString();
                string userRole = row.Cells["UserRole"].Value.ToString();
                AdminRadioButton.Checked = userRole == "Admin";
                WorkerRadioButton.Checked = userRole == "Worker";
                checkBox1.Checked = (bool)row.Cells["UserStatus"].Value;
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
            // 입력값 체크 예외처리
            if (EmployeeIdTextBox.Text == "" || UserNameTextBox.Text == "")
            {
                MessageBox.Show("Please write EmployeeID or Name.");
                return;
            }


            string query = "INSERT INTO Users (EmployeeID, UserName, Department, UserRole, UserStatus, Department) VALUES(@EmployeeID, @UserName, @Department, @UserRole, @UserStatus, @Department)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", int.Parse(EmployeeIdTextBox.Text)),
                new SqlParameter("@UserName", UserNameTextBox.Text),
                new SqlParameter("@Department", DepartmentTextBox.Text),
                new SqlParameter("@UserRole", AdminRadioButton.Checked ? "Admin" : "Worker"),
                new SqlParameter("@UserStatus", checkBox1.Checked ? 1 : 0)
            };

            try
            {
                int rowsAffected = DBHelper.ExecuteNonQuery(query, parameters);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("등록이 완료되었습니다.");
                    LoadUserData();
                }
                else
                {
                    MessageBox.Show("등록 실패! 다시 시도해주세요.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("This Id already exists.", "Error");
            }

        }

        // 작업자 정보 수정
        private void UpdateUserButton_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Users SET UserName = @UserName, UserRole = @UserRole, UserStatus = @UserStatus, Department = @Department WHERE EmployeeID = @EmployeeID";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", int.Parse(EmployeeIdTextBox.Text)),
                new SqlParameter("@UserName", UserNameTextBox.Text),
                new SqlParameter("@Department", DepartmentTextBox.Text),
                new SqlParameter("@UserRole", AdminRadioButton.Checked ? "Admin" : "Worker"),
                new SqlParameter("@UserStatus", checkBox1.Checked ? 1 : 0)
            };

            try
            {
                int rowsAffected = DBHelper.ExecuteNonQuery(query, parameters);
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
            string query = "DELETE FROM Users WHERE EmployeeID = @EmployeeID";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", int.Parse(EmployeeIdTextBox.Text))
            };
            try
            {
                int rowsAffected = DBHelper.ExecuteNonQuery(query, parameters);
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

        
    }
}
