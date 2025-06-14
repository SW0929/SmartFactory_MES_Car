
using MES_SW.DB;
using Microsoft.Data.SqlClient;
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
        public int LoggedInUserID { get; private set; } // int �� Ÿ�� ��ȯ �ؾ���.***************************************
        public string LoggedInUserName { get; private set; } // �α����� ����� �̸�
        public string LoggedInUserRole { get; private set; }

        public LoginForm()
        {
            InitializeComponent();

        }
        #region Event_Handlers
        private void LoginButton_Click(object sender, EventArgs e)
        {
            LoggedInUserID = int.Parse(EmployeeNumText.Text); // EmployeeId �ٸ� ���� �����ϱ� ���� ���� �ʿ���**************************************
            // ���ε� ����
            string query = "SELECT * FROM Users WHERE EmployeeId = @id";
            // @id �� �Ķ����
            SqlParameter[] parameters = new SqlParameter[]
            {
                // SqlParameter ��ü�� ���� int.Parse(EmployeeNumText.Text) ����
                // @id �� "���ε�"�ϴ� ��
                new SqlParameter("@id", int.Parse(EmployeeNumText.Text))
            };
            try
            {
                SqlDataReader reader = DBHelper.ExecuteReader(query, parameters);
                // ���� �����Ͱ� ������ true
                if (reader.Read())
                {

                    // ��ȸ�� ���̺��� ���ڵ� ���� ���� ������ �� ����.
                    LoggedInUserName = reader.GetString(1); // ����� �̸� ��������
                    string role = reader.GetString(2);
                    bool status = reader.GetBoolean(3);
                    
                    if (status) //Ȱ�� ������ ���� �α��� ����
                    {
                        LoggedInUserRole = role;
                        
                        // ShowDialog()�� ȣ���� ��(��, Program.cs)������ 
                        // DialogResult.OK�� Ȯ���ؼ� ���� ���� ���� ��.
                        this.DialogResult = DialogResult.OK;

                        // �޸� ������ DB ���� ���� ���� ������ ���� �ݾ��ִ� �� �߿�
                        // �� �ݾ���� ��.
                        reader.Close();
                        // ���� Form ����
                        this.Close();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("��Ȱ��ȭ�� �����Դϴ�.");
                    }

                }
                else
                {
                    MessageBox.Show("��ϵ��� ���� ����Դϴ�.");
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"���� �޽���{ex.Message}");

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
