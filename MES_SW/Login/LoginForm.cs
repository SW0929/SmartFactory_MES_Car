
using MES_SW.Data;
using MES_SW.Models;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;

namespace mes
{    
    public partial class LoginForm : Form
        {
            /*
            �ڵ� ���� �Ӽ�
            C#�� ���������� �ʵ带 �ڵ����� �������ִ� �Ӽ�
            ���� �����ϴ� ���� LoginForm Ŭ���� ���ο����� �����ϰ� ���� �д� ���� �ܺο��� �����ϵ��� ������.
            */

            // ĸ��ȭ
            public int LoggedInUserID { get; private set; }
            public string LoggedInUserName { get; private set; } = string.Empty; // �ʱⰪ �Ҵ�
            public string LoggedInUserRole { get; private set; } = string.Empty;

            private readonly UserRepository _userRepo = new();

            public LoginForm()
            {
                InitializeComponent();
            this.ActiveControl = EmployeeNumText; // ���� ������ ��� �Է¶��� ��Ŀ���� ������ ����
        }
            #region Event_Handlers
            private void LoginButton_Click(object sender, EventArgs e)
            {
                try
                {
                    if (EmployeeNumText.Text.IsNullOrEmpty())
                        throw new Exception("����� �Է��ؾ� �մϴ�.");

                if (!int.TryParse(EmployeeNumText.Text, out int employeeId))
                        throw new Exception("����� ���ڸ� �Է��ؾ� �մϴ�.");

                    // ? �� null�� ����ϴ� Nullable Ÿ��
                    User? user = _userRepo.GetUserById(employeeId);
                    if (user == null)
                        throw new Exception("��ϵ��� ���� ����Դϴ�.");

                    if (!user.IsActive) //Ȱ�� ������ ���� �α��� ����
                        throw new Exception("��Ȱ��ȭ�� �����Դϴ�.");

                    // �α��� ����
                    LoggedInUserID = user.EmployeeId;
                    LoggedInUserName = user.Name;
                    LoggedInUserRole = user.Role;

                    // ShowDialog()�� ȣ���� ��(��, Program.cs)������ 
                    // DialogResult.OK�� Ȯ���ؼ� ���� ���� ���� ��.
                    DialogResult = DialogResult.OK;
                    Close(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "�α��� ����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        // ��� �Է¶��� ���ڿ� �齺���̽��� �Է� �����ϵ��� ����
        private void EmployeeNumText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // ���ڿ� �齺���̽� ���� Ű �Է��� ����
            }
        }
        #endregion
    }
}
