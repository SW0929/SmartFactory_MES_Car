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
            QualityCheckButton = new Button();
            WorkOdersButton = new Button();
            ProductionResultsButton = new Button();
            DefectReportButton = new Button();
            EquipmentStatusButton = new Button();
            PanelMain = new Panel();
            WorkerName = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            CurrentTime = new Label();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Location = new Point(22, 78);
            panel1.Name = "panel1";
            panel1.Size = new Size(267, 583);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(ExitButton, 0, 6);
            tableLayoutPanel1.Controls.Add(DashboardButton, 0, 5);
            tableLayoutPanel1.Controls.Add(QualityCheckButton, 0, 4);
            tableLayoutPanel1.Controls.Add(WorkOdersButton, 0, 0);
            tableLayoutPanel1.Controls.Add(ProductionResultsButton, 0, 1);
            tableLayoutPanel1.Controls.Add(DefectReportButton, 0, 2);
            tableLayoutPanel1.Controls.Add(EquipmentStatusButton, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(267, 583);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // ExitButton
            // 
            ExitButton.Dock = DockStyle.Fill;
            ExitButton.Location = new Point(3, 501);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(261, 79);
            ExitButton.TabIndex = 6;
            ExitButton.Text = "종료";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // DashboardButton
            // 
            DashboardButton.Dock = DockStyle.Fill;
            DashboardButton.Location = new Point(3, 418);
            DashboardButton.Name = "DashboardButton";
            DashboardButton.Size = new Size(261, 77);
            DashboardButton.TabIndex = 5;
            DashboardButton.Text = "생산현황 대시보드";
            DashboardButton.UseVisualStyleBackColor = true;
            DashboardButton.Click += DashboardButton_Click;
            // 
            // QualityCheckButton
            // 
            QualityCheckButton.Dock = DockStyle.Fill;
            QualityCheckButton.Location = new Point(3, 335);
            QualityCheckButton.Name = "QualityCheckButton";
            QualityCheckButton.Size = new Size(261, 77);
            QualityCheckButton.TabIndex = 4;
            QualityCheckButton.Text = "검사 결과(추후 개발)";
            QualityCheckButton.UseVisualStyleBackColor = true;
            // 
            // WorkOdersButton
            // 
            WorkOdersButton.Dock = DockStyle.Fill;
            WorkOdersButton.Location = new Point(3, 3);
            WorkOdersButton.Name = "WorkOdersButton";
            WorkOdersButton.Size = new Size(261, 77);
            WorkOdersButton.TabIndex = 0;
            WorkOdersButton.Text = "생산지시 목록";
            WorkOdersButton.UseVisualStyleBackColor = true;
            WorkOdersButton.Click += WorkOdersButton_Click;
            // 
            // ProductionResultsButton
            // 
            ProductionResultsButton.Dock = DockStyle.Fill;
            ProductionResultsButton.Location = new Point(3, 86);
            ProductionResultsButton.Name = "ProductionResultsButton";
            ProductionResultsButton.Size = new Size(261, 77);
            ProductionResultsButton.TabIndex = 1;
            ProductionResultsButton.Text = "작업실적 등록(추후 개발)";
            ProductionResultsButton.UseVisualStyleBackColor = true;
            // 
            // DefectReportButton
            // 
            DefectReportButton.Dock = DockStyle.Fill;
            DefectReportButton.Location = new Point(3, 169);
            DefectReportButton.Name = "DefectReportButton";
            DefectReportButton.Size = new Size(261, 77);
            DefectReportButton.TabIndex = 2;
            DefectReportButton.Text = "불량 입력(추후 개발)";
            DefectReportButton.UseVisualStyleBackColor = true;
            // 
            // EquipmentStatusButton
            // 
            EquipmentStatusButton.Dock = DockStyle.Fill;
            EquipmentStatusButton.Location = new Point(3, 252);
            EquipmentStatusButton.Name = "EquipmentStatusButton";
            EquipmentStatusButton.Size = new Size(261, 77);
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
            CurrentTime.Location = new Point(1270, 20);
            CurrentTime.Name = "CurrentTime";
            CurrentTime.Size = new Size(165, 42);
            CurrentTime.TabIndex = 3;
            CurrentTime.Text = "현재 시각";
            CurrentTime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // WorkerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1447, 673);
            Controls.Add(CurrentTime);
            Controls.Add(WorkerName);
            Controls.Add(PanelMain);
            Controls.Add(panel1);
            Name = "WorkerForm";
            Text = "작업자";
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button WorkOdersButton;
        private TableLayoutPanel tableLayoutPanel1;
        private Button ExitButton;
        private Button DashboardButton;
        private Button QualityCheckButton;
        private Button ProductionResultsButton;
        private Button DefectReportButton;
        private Button EquipmentStatusButton;
        private Panel PanelMain;
        private Label WorkerName;
        private System.Windows.Forms.Timer timer1;
        private Label CurrentTime;
    }
}