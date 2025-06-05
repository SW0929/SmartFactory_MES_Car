using MES_SW.Admin;

namespace mes
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new LoginForm());

            LoginForm loginForm = new LoginForm();
            
            DialogResult result = loginForm.ShowDialog();
            
            

            if (result == DialogResult.OK)
            {
                string userRole = loginForm.LoggedInUserRole; //로그인한 사용자 역할
                string userId = loginForm.LoggedInUserID; // 로그인한 사용자 ID
                string userName = loginForm.LoggedInUserName; // 로그인한 사용자 이름
                Form nextForm;

                if (userRole == "Admin")
                {
                    
                    nextForm = new AdminForm(userId, userName);
                }
                else
                {
                    nextForm = new WorkerForm(userId, userName);
                }

                Application.Run(nextForm); // 여기서만 Run 실행 (폼 하나만 Run 가능)
            }
            else
            {
                // 로그인 실패거나 취소한 경우: 아무것도 실행하지 않음 (프로그램 종료)
            }


        }
    }
}