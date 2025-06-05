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
                string userRole = loginForm.LoggedInUserRole; //�α����� ����� ����
                string userId = loginForm.LoggedInUserID; // �α����� ����� ID
                string userName = loginForm.LoggedInUserName; // �α����� ����� �̸�
                Form nextForm;

                if (userRole == "Admin")
                {
                    
                    nextForm = new AdminForm(userId, userName);
                }
                else
                {
                    nextForm = new WorkerForm(userId, userName);
                }

                Application.Run(nextForm); // ���⼭�� Run ���� (�� �ϳ��� Run ����)
            }
            else
            {
                // �α��� ���аų� ����� ���: �ƹ��͵� �������� ���� (���α׷� ����)
            }


        }
    }
}