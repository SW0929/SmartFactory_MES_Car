namespace mes
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            LoginButton = new Button();
            EmployeeNumText = new TextBox();
            EmployeeNumLabel = new Label();
            panel1 = new Panel();
            ExitButton = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // LoginButton
            // 
            LoginButton.Location = new Point(159, 192);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(95, 26);
            LoginButton.TabIndex = 0;
            LoginButton.Text = "로그인";
            LoginButton.UseVisualStyleBackColor = true;
            LoginButton.Click += LoginButton_Click;
            // 
            // EmployeeNumText
            // 
            EmployeeNumText.Location = new Point(160, 153);
            EmployeeNumText.Name = "EmployeeNumText";
            EmployeeNumText.Size = new Size(94, 23);
            EmployeeNumText.TabIndex = 1;
            EmployeeNumText.KeyPress += EmployeeNumText_KeyPress;
            // 
            // EmployeeNumLabel
            // 
            EmployeeNumLabel.AutoSize = true;
            EmployeeNumLabel.Location = new Point(160, 135);
            EmployeeNumLabel.Name = "EmployeeNumLabel";
            EmployeeNumLabel.Size = new Size(66, 15);
            EmployeeNumLabel.TabIndex = 2;
            EmployeeNumLabel.Text = "사원번호 : ";
            EmployeeNumLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.Controls.Add(ExitButton);
            panel1.Controls.Add(EmployeeNumLabel);
            panel1.Controls.Add(LoginButton);
            panel1.Controls.Add(EmployeeNumText);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(440, 430);
            panel1.TabIndex = 5;
            // 
            // ExitButton
            // 
            ExitButton.Location = new Point(160, 224);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(94, 26);
            ExitButton.TabIndex = 5;
            ExitButton.Text = "종료";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(440, 430);
            Controls.Add(panel1);
            Name = "LoginForm";
            Text = "로그인";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button LoginButton;
        private TextBox EmployeeNumText;
        private Label EmployeeNumLabel;
        private Panel panel1;
        private Button ExitButton;
    }
}
