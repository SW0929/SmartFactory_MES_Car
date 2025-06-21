
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
            자동 구현 속성
            C#이 내부적으로 필드를 자동으로 생성해주는 속성
            값을 수정하는 것은 LoginForm 클래스 내부에서만 가능하고 값을 읽는 것은 외부에서 가능하도록 설정함.
            */

            // 캡슐화
            public int LoggedInUserID { get; private set; }
            public string LoggedInUserName { get; private set; } = string.Empty; // 초기값 할당
            public string LoggedInUserRole { get; private set; } = string.Empty;

            private readonly UserRepository _userRepo = new();

            public LoginForm()
            {
                InitializeComponent();
            this.ActiveControl = EmployeeNumText; // 폼이 열리면 사번 입력란에 포커스가 가도록 설정
        }
            #region Event_Handlers
            private void LoginButton_Click(object sender, EventArgs e)
            {
                try
                {
                    if (EmployeeNumText.Text.IsNullOrEmpty())
                        throw new Exception("사번을 입력해야 합니다.");

                if (!int.TryParse(EmployeeNumText.Text, out int employeeId))
                        throw new Exception("사번은 숫자만 입력해야 합니다.");

                    // ? 는 null을 허용하는 Nullable 타입
                    User? user = _userRepo.GetUserById(employeeId);
                    if (user == null)
                        throw new Exception("등록되지 않은 사번입니다.");

                    if (!user.IsActive) //활성 계정일 때만 로그인 가능
                        throw new Exception("비활성화된 계정입니다.");

                    // 로그인 성공
                    LoggedInUserID = user.EmployeeId;
                    LoggedInUserName = user.Name;
                    LoggedInUserRole = user.Role;

                    // ShowDialog()를 호출한 곳(즉, Program.cs)에서는 
                    // DialogResult.OK를 확인해서 다음 폼을 열게 됨.
                    DialogResult = DialogResult.OK;
                    Close(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "로그인 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        // 사번 입력란에 숫자와 백스페이스만 입력 가능하도록 설정
        private void EmployeeNumText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // 숫자와 백스페이스 외의 키 입력을 무시
            }
        }
        #endregion
    }
}
