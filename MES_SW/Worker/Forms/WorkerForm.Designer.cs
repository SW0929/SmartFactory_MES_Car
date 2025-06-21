namespace mes
{
    partial class WorkerForm
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
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            ExitButton = new Button();
            DashboardButton = new Button();
            WorkOdersButton = new Button();
            WorkPerformanceButton = new Button();
            EquipmentStatusButton = new Button();
            PanelMain = new Panel();
            WorkerName = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            CurrentTime = new Label();
            LogOutButton = new Button();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Location = new Point(25, 78);
            panel1.Name = "panel1";
            panel1.Size = new Size(267, 583);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(ExitButton, 0, 4);
            tableLayoutPanel1.Controls.Add(DashboardButton, 0, 3);
            tableLayoutPanel1.Controls.Add(WorkOdersButton, 0, 0);
            tableLayoutPanel1.Controls.Add(WorkPerformanceButton, 0, 1);
            tableLayoutPanel1.Controls.Add(EquipmentStatusButton, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(267, 583);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // ExitButton
            // 
            ExitButton.Dock = DockStyle.Fill;
            ExitButton.Location = new Point(3, 467);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(261, 113);
            ExitButton.TabIndex = 6;
            ExitButton.Text = "종료";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // DashboardButton
            // 
            DashboardButton.Dock = DockStyle.Fill;
            DashboardButton.Location = new Point(3, 351);
            DashboardButton.Name = "DashboardButton";
            DashboardButton.Size = new Size(261, 110);
            DashboardButton.TabIndex = 5;
            DashboardButton.Text = "생산현황 대시보드";
            DashboardButton.UseVisualStyleBackColor = true;
            DashboardButton.Click += DashboardButton_Click;
            // 
            // WorkOdersButton
            // 
            WorkOdersButton.Dock = DockStyle.Fill;
            WorkOdersButton.Location = new Point(3, 3);
            WorkOdersButton.Name = "WorkOdersButton";
            WorkOdersButton.Size = new Size(261, 110);
            WorkOdersButton.TabIndex = 0;
            WorkOdersButton.Text = "생산지시 목록";
            WorkOdersButton.UseVisualStyleBackColor = true;
            WorkOdersButton.Click += WorkOdersButton_Click;
            // 
            // WorkPerformanceButton
            // 
            WorkPerformanceButton.Dock = DockStyle.Fill;
            WorkPerformanceButton.Location = new Point(3, 119);
            WorkPerformanceButton.Name = "WorkPerformanceButton";
            WorkPerformanceButton.Size = new Size(261, 110);
            WorkPerformanceButton.TabIndex = 1;
            WorkPerformanceButton.Text = "작업실적";
            WorkPerformanceButton.UseVisualStyleBackColor = true;
            WorkPerformanceButton.Click += WorkPerformanceButton_Click;
            // 
            // EquipmentStatusButton
            // 
            EquipmentStatusButton.Dock = DockStyle.Fill;
            EquipmentStatusButton.Location = new Point(3, 235);
            EquipmentStatusButton.Name = "EquipmentStatusButton";
            EquipmentStatusButton.Size = new Size(261, 110);
            EquipmentStatusButton.TabIndex = 3;
            EquipmentStatusButton.Text = "설비상태";
            EquipmentStatusButton.UseVisualStyleBackColor = true;
            EquipmentStatusButton.Click += EquipmentStatusButton_Click;
            // 
            // PanelMain
            // 
            PanelMain.Location = new Point(322, 78);
            PanelMain.Name = "PanelMain";
            PanelMain.Size = new Size(1113, 580);
            PanelMain.TabIndex = 1;
            // 
            // WorkerName
            // 
            WorkerName.AutoSize = true;
            WorkerName.Location = new Point(25, 20);
            WorkerName.Name = "WorkerName";
            WorkerName.Size = new Size(71, 15);
            WorkerName.TabIndex = 2;
            WorkerName.Text = "작업자 이름";
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
            CurrentTime.Location = new Point(1270, 20);
            CurrentTime.Name = "CurrentTime";
            CurrentTime.Size = new Size(165, 42);
            CurrentTime.TabIndex = 3;
            CurrentTime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LogOutButton
            // 
            LogOutButton.Location = new Point(164, 17);
            LogOutButton.Name = "LogOutButton";
            LogOutButton.Size = new Size(75, 23);
            LogOutButton.TabIndex = 4;
            LogOutButton.Text = "로그아웃";
            LogOutButton.UseVisualStyleBackColor = true;
            LogOutButton.Click += LogOutButton_Click;
            // 
            // WorkerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1447, 673);
            Controls.Add(LogOutButton);
            Controls.Add(CurrentTime);
            Controls.Add(WorkerName);
            Controls.Add(PanelMain);
            Controls.Add(panel1);
            Name = "WorkerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "작업자";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Panel PanelMain;
        private Label WorkerName;
        private System.Windows.Forms.Timer timer1;
        private Label CurrentTime;
        private Button LogOutButton;
        private TableLayoutPanel tableLayoutPanel1;
        private Button ExitButton;
        private Button DashboardButton;
        private Button WorkOdersButton;
        private Button WorkPerformanceButton;
        private Button EquipmentStatusButton;
    }
}