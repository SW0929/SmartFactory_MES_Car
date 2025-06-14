
using MES_SW.DB;
using Microsoft.Data.SqlClient;
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
        public int LoggedInUserID { get; private set; } // int 로 타입 변환 해야함.***************************************
        public string LoggedInUserName { get; private set; } // 로그인한 사용자 이름
        public string LoggedInUserRole { get; private set; }

        public LoginForm()
        {
            InitializeComponent();

        }
        #region Event_Handlers
        private void LoginButton_Click(object sender, EventArgs e)
        {
            LoggedInUserID = int.Parse(EmployeeNumText.Text); // EmployeeId 다른 폼에 전달하기 위해 수정 필요함**************************************
            // 바인딩 설명
            string query = "SELECT * FROM Users WHERE EmployeeId = @id";
            // @id 는 파라미터
            SqlParameter[] parameters = new SqlParameter[]
            {
                // SqlParameter 객체를 통해 int.Parse(EmployeeNumText.Text) 값을
                // @id 에 "바인딩"하는 것
                new SqlParameter("@id", int.Parse(EmployeeNumText.Text))
            };
            try
            {
                SqlDataReader reader = DBHelper.ExecuteReader(query, parameters);
                // 읽을 데이터가 있으면 true
                if (reader.Read())
                {

                    // 조회한 테이블의 레코드 값의 열을 가져올 수 있음.
                    LoggedInUserName = reader.GetString(1); // 사용자 이름 가져오기
                    string role = reader.GetString(2);
                    bool status = reader.GetBoolean(3);
                    
                    if (status) //활성 계정일 때만 로그인 가능
                    {
                        LoggedInUserRole = role;
                        
                        // ShowDialog()를 호출한 곳(즉, Program.cs)에서는 
                        // DialogResult.OK를 확인해서 다음 폼을 열게 됨.
                        this.DialogResult = DialogResult.OK;

                        // 메모리 누수나 DB 연결 유지 문제 방지를 위해 닫아주는 게 중요
                        // 꼭 닫아줘야 함.
                        reader.Close();
                        // 현재 Form 종료
                        this.Close();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("비활성화된 계정입니다.");
                    }

                }
                else
                {
                    MessageBox.Show("등록되지 않은 사번입니다.");
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 메시지{ex.Message}");

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
