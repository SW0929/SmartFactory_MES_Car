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
            EmployeeNumText = new TextBox();
            LoginButton = new Button();
            EmployeeNumLabel = new Label();
            ExitButton = new Button();
            LoginLabel = new Label();
            SuspendLayout();
            // 
            // EmployeeNumText
            // 
            EmployeeNumText.Font = new Font("Stencil", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            EmployeeNumText.Location = new Point(176, 254);
            EmployeeNumText.Multiline = true;
            EmployeeNumText.Name = "EmployeeNumText";
            EmployeeNumText.Size = new Size(200, 32);
            EmployeeNumText.TabIndex = 0;
            EmployeeNumText.TextAlign = HorizontalAlignment.Right;
            EmployeeNumText.KeyPress += EmployeeNumText_KeyPress;
            // 
            // LoginButton
            // 
            LoginButton.BackColor = SystemColors.MenuHighlight;
            LoginButton.FlatStyle = FlatStyle.Flat;
            LoginButton.ForeColor = SystemColors.Desktop;
            LoginButton.Location = new Point(176, 301);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(200, 51);
            LoginButton.TabIndex = 1;
            LoginButton.Text = "로그인";
            LoginButton.UseVisualStyleBackColor = false;
            LoginButton.Click += LoginButton_Click;
            // 
            // EmployeeNumLabel
            // 
            EmployeeNumLabel.Font = new Font("Stencil", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            EmployeeNumLabel.Location = new Point(194, 208);
            EmployeeNumLabel.Name = "EmployeeNumLabel";
            EmployeeNumLabel.Size = new Size(160, 34);
            EmployeeNumLabel.TabIndex = 2;
            EmployeeNumLabel.Text = "사원번호";
            EmployeeNumLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ExitButton
            // 
            ExitButton.BackColor = SystemColors.Control;
            ExitButton.FlatStyle = FlatStyle.Flat;
            ExitButton.Location = new Point(176, 358);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(200, 58);
            ExitButton.TabIndex = 2;
            ExitButton.Text = "종료";
            ExitButton.UseVisualStyleBackColor = false;
            ExitButton.Click += ExitButton_Click;
            // 
            // LoginLabel
            // 
            LoginLabel.Font = new Font("Stencil", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LoginLabel.Location = new Point(194, 51);
            LoginLabel.Name = "LoginLabel";
            LoginLabel.Size = new Size(160, 55);
            LoginLabel.TabIndex = 6;
            LoginLabel.Text = "로그인";
            LoginLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.White;
            ClientSize = new Size(550, 544);
            Controls.Add(ExitButton);
            Controls.Add(LoginLabel);
            Controls.Add(LoginButton);
            Controls.Add(EmployeeNumLabel);
            Controls.Add(EmployeeNumText);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "로그인";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox EmployeeNumText;
        private Button LoginButton;
        private Label EmployeeNumLabel;
        private Button ExitButton;
        private Label LoginLabel;
    }
}
