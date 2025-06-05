namespace MES_SW.Admin
{
    partial class UserControl_UserManager
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            dataGridView1 = new DataGridView();
            AddUserButton = new Button();
            UpdateUserButton = new Button();
            DeleteUserButton = new Button();
            EmployeeIdLabel = new Label();
            EmployeeIdTextBox = new TextBox();
            UserNameTextBox = new TextBox();
            UserNameLabel = new Label();
            label4 = new Label();
            AdminRadioButton = new RadioButton();
            WorkerRadioButton = new RadioButton();
            checkBox1 = new CheckBox();
            groupBox1 = new GroupBox();
            DepartmentTextBox = new TextBox();
            DepartmentLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(148, 45);
            label1.TabIndex = 0;
            label1.Text = "사용자 관리";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.GridColor = Color.White;
            dataGridView1.Location = new Point(36, 58);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(718, 294);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // AddUserButton
            // 
            AddUserButton.Location = new Point(36, 399);
            AddUserButton.Name = "AddUserButton";
            AddUserButton.Size = new Size(157, 36);
            AddUserButton.TabIndex = 2;
            AddUserButton.Text = "추가";
            AddUserButton.UseVisualStyleBackColor = true;
            AddUserButton.Click += AddUserButton_Click;
            // 
            // UpdateUserButton
            // 
            UpdateUserButton.Location = new Point(312, 399);
            UpdateUserButton.Name = "UpdateUserButton";
            UpdateUserButton.Size = new Size(157, 36);
            UpdateUserButton.TabIndex = 3;
            UpdateUserButton.Text = "수정";
            UpdateUserButton.UseVisualStyleBackColor = true;
            UpdateUserButton.Click += UpdateUserButton_Click;
            // 
            // DeleteUserButton
            // 
            DeleteUserButton.Location = new Point(597, 399);
            DeleteUserButton.Name = "DeleteUserButton";
            DeleteUserButton.Size = new Size(157, 36);
            DeleteUserButton.TabIndex = 4;
            DeleteUserButton.Text = "삭제";
            DeleteUserButton.UseVisualStyleBackColor = true;
            DeleteUserButton.Click += DeleteUserButton_Click;
            // 
            // EmployeeIdLabel
            // 
            EmployeeIdLabel.AutoSize = true;
            EmployeeIdLabel.Location = new Point(795, 74);
            EmployeeIdLabel.Name = "EmployeeIdLabel";
            EmployeeIdLabel.Size = new Size(80, 15);
            EmployeeIdLabel.TabIndex = 5;
            EmployeeIdLabel.Text = "EmployeeId : ";
            EmployeeIdLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // EmployeeIdTextBox
            // 
            EmployeeIdTextBox.Location = new Point(883, 71);
            EmployeeIdTextBox.Name = "EmployeeIdTextBox";
            EmployeeIdTextBox.Size = new Size(100, 23);
            EmployeeIdTextBox.TabIndex = 6;
            EmployeeIdTextBox.KeyPress += EmployeeIdTextBox_KeyPress;
            // 
            // UserNameTextBox
            // 
            UserNameTextBox.Location = new Point(883, 125);
            UserNameTextBox.Name = "UserNameTextBox";
            UserNameTextBox.Size = new Size(100, 23);
            UserNameTextBox.TabIndex = 8;
            // 
            // UserNameLabel
            // 
            UserNameLabel.AutoSize = true;
            UserNameLabel.Location = new Point(795, 133);
            UserNameLabel.Name = "UserNameLabel";
            UserNameLabel.Size = new Size(50, 15);
            UserNameLabel.TabIndex = 7;
            UserNameLabel.Text = "Name : ";
            UserNameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(795, 310);
            label4.Name = "label4";
            label4.Size = new Size(51, 15);
            label4.TabIndex = 11;
            label4.Text = "Status : ";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AdminRadioButton
            // 
            AdminRadioButton.AutoSize = true;
            AdminRadioButton.Location = new Point(125, 22);
            AdminRadioButton.Name = "AdminRadioButton";
            AdminRadioButton.Size = new Size(61, 19);
            AdminRadioButton.TabIndex = 13;
            AdminRadioButton.Text = "Admin";
            AdminRadioButton.UseVisualStyleBackColor = true;
            // 
            // WorkerRadioButton
            // 
            WorkerRadioButton.AutoSize = true;
            WorkerRadioButton.Checked = true;
            WorkerRadioButton.Location = new Point(17, 22);
            WorkerRadioButton.Name = "WorkerRadioButton";
            WorkerRadioButton.Size = new Size(63, 19);
            WorkerRadioButton.TabIndex = 14;
            WorkerRadioButton.TabStop = true;
            WorkerRadioButton.Text = "Worker";
            WorkerRadioButton.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(881, 310);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(69, 19);
            checkBox1.TabIndex = 15;
            checkBox1.Text = "Activate";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(AdminRadioButton);
            groupBox1.Controls.Add(WorkerRadioButton);
            groupBox1.Location = new Point(795, 238);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(220, 50);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            groupBox1.Text = "Role";
            // 
            // DepartmentTextBox
            // 
            DepartmentTextBox.Location = new Point(883, 179);
            DepartmentTextBox.Name = "DepartmentTextBox";
            DepartmentTextBox.Size = new Size(100, 23);
            DepartmentTextBox.TabIndex = 18;
            // 
            // DepartmentLabel
            // 
            DepartmentLabel.AutoSize = true;
            DepartmentLabel.Location = new Point(795, 182);
            DepartmentLabel.Name = "DepartmentLabel";
            DepartmentLabel.Size = new Size(82, 15);
            DepartmentLabel.TabIndex = 17;
            DepartmentLabel.Text = "Department : ";
            DepartmentLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UserControl_UserManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            Controls.Add(DepartmentTextBox);
            Controls.Add(DepartmentLabel);
            Controls.Add(groupBox1);
            Controls.Add(checkBox1);
            Controls.Add(label4);
            Controls.Add(UserNameTextBox);
            Controls.Add(UserNameLabel);
            Controls.Add(EmployeeIdTextBox);
            Controls.Add(EmployeeIdLabel);
            Controls.Add(DeleteUserButton);
            Controls.Add(UpdateUserButton);
            Controls.Add(AddUserButton);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Name = "UserControl_UserManager";
            Size = new Size(1098, 600);
            Load += UserControl_UserManager_Load;
            Click += UserControl_UserManager_Click;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dataGridView1;
        private Button AddUserButton;
        private Button UpdateUserButton;
        private Button DeleteUserButton;
        private Label EmployeeIdLabel;
        private TextBox EmployeeIdTextBox;
        private TextBox UserNameTextBox;
        private Label UserNameLabel;
        private Label label4;
        private RadioButton AdminRadioButton;
        private RadioButton WorkerRadioButton;
        private CheckBox checkBox1;
        private GroupBox groupBox1;
        private TextBox DepartmentTextBox;
        private Label DepartmentLabel;
    }
}
