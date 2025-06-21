namespace mes
{
    partial class AdminForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tableLayoutPanel1 = new TableLayoutPanel();
            EquipmentDefectButton = new Button();
            EquipmentButton = new Button();
            UsersButton = new Button();
            DashboardButton = new Button();
            OrdersButton = new Button();
            ExitButton = new Button();
            panelMenu = new Panel();
            panelMain = new Panel();
            timer1 = new System.Windows.Forms.Timer(components);
            CurrentTime = new Label();
            AdminName = new Label();
            LogOutButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            panelMenu.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(EquipmentDefectButton, 0, 4);
            tableLayoutPanel1.Controls.Add(EquipmentButton, 0, 3);
            tableLayoutPanel1.Controls.Add(UsersButton, 0, 0);
            tableLayoutPanel1.Controls.Add(DashboardButton, 0, 2);
            tableLayoutPanel1.Controls.Add(OrdersButton, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(274, 350);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // EquipmentDefectButton
            // 
            EquipmentDefectButton.Dock = DockStyle.Fill;
            EquipmentDefectButton.Location = new Point(3, 283);
            EquipmentDefectButton.Name = "EquipmentDefectButton";
            EquipmentDefectButton.Size = new Size(268, 64);
            EquipmentDefectButton.TabIndex = 4;
            EquipmentDefectButton.Text = "설비 관리";
            EquipmentDefectButton.UseVisualStyleBackColor = true;
            EquipmentDefectButton.Click += EquipmentDefectButton_Click;
            // 
            // EquipmentButton
            // 
            EquipmentButton.Dock = DockStyle.Fill;
            EquipmentButton.Location = new Point(3, 213);
            EquipmentButton.Name = "EquipmentButton";
            EquipmentButton.Size = new Size(268, 64);
            EquipmentButton.TabIndex = 3;
            EquipmentButton.Text = "설비 목록";
            EquipmentButton.UseVisualStyleBackColor = true;
            EquipmentButton.Click += EquipmentButton_Click;
            // 
            // UsersButton
            // 
            UsersButton.Dock = DockStyle.Fill;
            UsersButton.Location = new Point(3, 3);
            UsersButton.Name = "UsersButton";
            UsersButton.Size = new Size(268, 64);
            UsersButton.TabIndex = 0;
            UsersButton.Text = "사용자 관리";
            UsersButton.UseVisualStyleBackColor = true;
            UsersButton.Click += UsersButton_Click;
            // 
            // DashboardButton
            // 
            DashboardButton.Dock = DockStyle.Fill;
            DashboardButton.Location = new Point(3, 143);
            DashboardButton.Name = "DashboardButton";
            DashboardButton.Size = new Size(268, 64);
            DashboardButton.TabIndex = 2;
            DashboardButton.Text = "생산 현황 대시보드";
            DashboardButton.UseVisualStyleBackColor = true;
            DashboardButton.Click += DashboardButton_Click;
            // 
            // OrdersButton
            // 
            OrdersButton.Dock = DockStyle.Fill;
            OrdersButton.Location = new Point(3, 73);
            OrdersButton.Name = "OrdersButton";
            OrdersButton.Size = new Size(268, 64);
            OrdersButton.TabIndex = 1;
            OrdersButton.Text = "생산 지시 관리";
            OrdersButton.UseVisualStyleBackColor = true;
            OrdersButton.Click += OrdersButton_Click;
            // 
            // ExitButton
            // 
            ExitButton.Dock = DockStyle.Bottom;
            ExitButton.Location = new Point(0, 516);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(274, 52);
            ExitButton.TabIndex = 1;
            ExitButton.Text = "종료";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // panelMenu
            // 
            panelMenu.Controls.Add(tableLayoutPanel1);
            panelMenu.Controls.Add(ExitButton);
            panelMenu.Location = new Point(12, 93);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(274, 568);
            panelMenu.TabIndex = 5;
            // 
            // panelMain
            // 
            panelMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelMain.Location = new Point(338, 93);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1097, 595);
            panelMain.TabIndex = 6;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // CurrentTime
            // 
            CurrentTime.BackColor = Color.DimGray;
            CurrentTime.ForeColor = Color.White;
            CurrentTime.Location = new Point(1221, 19);
            CurrentTime.Name = "CurrentTime";
            CurrentTime.Size = new Size(214, 49);
            CurrentTime.TabIndex = 7;
            CurrentTime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AdminName
            // 
            AdminName.AutoSize = true;
            AdminName.Location = new Point(42, 19);
            AdminName.Name = "AdminName";
            AdminName.Size = new Size(71, 15);
            AdminName.TabIndex = 8;
            AdminName.Text = "관리자 이름";
            // 
            // LogOutButton
            // 
            LogOutButton.Location = new Point(135, 15);
            LogOutButton.Name = "LogOutButton";
            LogOutButton.Size = new Size(75, 23);
            LogOutButton.TabIndex = 9;
            LogOutButton.Text = "로그아웃";
            LogOutButton.UseVisualStyleBackColor = true;
            LogOutButton.Click += LogOutButton_Click;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1447, 700);
            Controls.Add(LogOutButton);
            Controls.Add(AdminName);
            Controls.Add(CurrentTime);
            Controls.Add(panelMain);
            Controls.Add(panelMenu);
            Name = "AdminForm";
            Text = "관리자";
            tableLayoutPanel1.ResumeLayout(false);
            panelMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private Button EquipmentButton;
        private Button UsersButton;
        private Button DashboardButton;
        private Button OrdersButton;
        private Button ExitButton;
        private Panel panelMenu;
        private Panel panelMain;
        private System.Windows.Forms.Timer timer1;
        private Label CurrentTime;
        private Button EquipmentDefectButton;
        private Label AdminName;
        private Button LogOutButton;
    }
}