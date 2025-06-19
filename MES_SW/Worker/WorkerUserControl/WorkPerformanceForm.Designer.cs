namespace MES_SW.Worker.WorkerUserControl
{
    partial class WorkPerformanceForm
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
            WorkOrderNumLabel = new Label();
            ProcessNameLabel = new Label();
            EquipmentNameLabel = new Label();
            GQtyLabel = new Label();
            panel1 = new Panel();
            TotalOrderLabel = new Label();
            ProductNameLabel = new Label();
            EndButton = new Button();
            ReportStoreButton = new Button();
            BadReasonTextBox = new TextBox();
            BQtyTextBox = new TextBox();
            GQtyTextBox = new TextBox();
            BQtyReasonLabel = new Label();
            BQtyLabel = new Label();
            backButton = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // WorkOrderNumLabel
            // 
            WorkOrderNumLabel.AutoSize = true;
            WorkOrderNumLabel.Location = new Point(6, 12);
            WorkOrderNumLabel.Name = "WorkOrderNumLabel";
            WorkOrderNumLabel.Size = new Size(101, 15);
            WorkOrderNumLabel.TabIndex = 0;
            WorkOrderNumLabel.Text = "생산지시 번호 : 1";
            // 
            // ProcessNameLabel
            // 
            ProcessNameLabel.AutoSize = true;
            ProcessNameLabel.Location = new Point(6, 46);
            ProcessNameLabel.Name = "ProcessNameLabel";
            ProcessNameLabel.Size = new Size(49, 15);
            ProcessNameLabel.TabIndex = 1;
            ProcessNameLabel.Text = "공정 : 1";
            // 
            // EquipmentNameLabel
            // 
            EquipmentNameLabel.AutoSize = true;
            EquipmentNameLabel.Location = new Point(6, 83);
            EquipmentNameLabel.Name = "EquipmentNameLabel";
            EquipmentNameLabel.Size = new Size(98, 15);
            EquipmentNameLabel.TabIndex = 2;
            EquipmentNameLabel.Text = "설비명 : 프레스A";
            // 
            // GQtyLabel
            // 
            GQtyLabel.AutoSize = true;
            GQtyLabel.Location = new Point(12, 172);
            GQtyLabel.Name = "GQtyLabel";
            GQtyLabel.Size = new Size(59, 15);
            GQtyLabel.TabIndex = 4;
            GQtyLabel.Text = "양품 수량";
            // 
            // panel1
            // 
            panel1.Controls.Add(backButton);
            panel1.Controls.Add(TotalOrderLabel);
            panel1.Controls.Add(ProductNameLabel);
            panel1.Controls.Add(EndButton);
            panel1.Controls.Add(ReportStoreButton);
            panel1.Controls.Add(BadReasonTextBox);
            panel1.Controls.Add(BQtyTextBox);
            panel1.Controls.Add(GQtyTextBox);
            panel1.Controls.Add(BQtyReasonLabel);
            panel1.Controls.Add(BQtyLabel);
            panel1.Controls.Add(GQtyLabel);
            panel1.Controls.Add(EquipmentNameLabel);
            panel1.Controls.Add(ProcessNameLabel);
            panel1.Controls.Add(WorkOrderNumLabel);
            panel1.Location = new Point(25, 17);
            panel1.Name = "panel1";
            panel1.Size = new Size(388, 516);
            panel1.TabIndex = 5;
            // 
            // TotalOrderLabel
            // 
            TotalOrderLabel.AutoSize = true;
            TotalOrderLabel.Location = new Point(12, 147);
            TotalOrderLabel.Name = "TotalOrderLabel";
            TotalOrderLabel.Size = new Size(100, 15);
            TotalOrderLabel.TabIndex = 13;
            TotalOrderLabel.Text = "총 주문 수량 : 10";
            // 
            // ProductNameLabel
            // 
            ProductNameLabel.AutoSize = true;
            ProductNameLabel.Location = new Point(6, 120);
            ProductNameLabel.Name = "ProductNameLabel";
            ProductNameLabel.Size = new Size(78, 15);
            ProductNameLabel.TabIndex = 12;
            ProductNameLabel.Text = "제품명 : 투싼";
            // 
            // EndButton
            // 
            EndButton.Location = new Point(268, 471);
            EndButton.Name = "EndButton";
            EndButton.Size = new Size(75, 23);
            EndButton.TabIndex = 11;
            EndButton.Text = "작업 종료";
            EndButton.UseVisualStyleBackColor = true;
            EndButton.Click += EndButton_Click;
            // 
            // ReportStoreButton
            // 
            ReportStoreButton.Location = new Point(29, 471);
            ReportStoreButton.Name = "ReportStoreButton";
            ReportStoreButton.Size = new Size(75, 23);
            ReportStoreButton.TabIndex = 10;
            ReportStoreButton.Text = "실적 저장";
            ReportStoreButton.UseVisualStyleBackColor = true;
            ReportStoreButton.Click += ReportStoreButton_Click;
            // 
            // BadReasonTextBox
            // 
            BadReasonTextBox.Location = new Point(12, 275);
            BadReasonTextBox.Multiline = true;
            BadReasonTextBox.Name = "BadReasonTextBox";
            BadReasonTextBox.Size = new Size(361, 158);
            BadReasonTextBox.TabIndex = 9;
            // 
            // BQtyTextBox
            // 
            BQtyTextBox.Location = new Point(108, 211);
            BQtyTextBox.Name = "BQtyTextBox";
            BQtyTextBox.ReadOnly = true;
            BQtyTextBox.Size = new Size(100, 23);
            BQtyTextBox.TabIndex = 8;
            BQtyTextBox.Text = "0";
            BQtyTextBox.TextAlign = HorizontalAlignment.Right;
            BQtyTextBox.KeyPress += BQtyTextBox_KeyPress;
            // 
            // GQtyTextBox
            // 
            GQtyTextBox.Location = new Point(108, 169);
            GQtyTextBox.Name = "GQtyTextBox";
            GQtyTextBox.Size = new Size(100, 23);
            GQtyTextBox.TabIndex = 7;
            GQtyTextBox.Text = "0";
            GQtyTextBox.TextAlign = HorizontalAlignment.Right;
            GQtyTextBox.TextChanged += GQtyTextBox_TextChanged;
            GQtyTextBox.KeyPress += GQtyTextBox_KeyPress;
            // 
            // BQtyReasonLabel
            // 
            BQtyReasonLabel.AutoSize = true;
            BQtyReasonLabel.Location = new Point(12, 248);
            BQtyReasonLabel.Name = "BQtyReasonLabel";
            BQtyReasonLabel.Size = new Size(59, 15);
            BQtyReasonLabel.TabIndex = 6;
            BQtyReasonLabel.Text = "불량 사유";
            // 
            // BQtyLabel
            // 
            BQtyLabel.AutoSize = true;
            BQtyLabel.Location = new Point(12, 214);
            BQtyLabel.Name = "BQtyLabel";
            BQtyLabel.Size = new Size(59, 15);
            BQtyLabel.TabIndex = 5;
            BQtyLabel.Text = "불량 수량";
            // 
            // backButton
            // 
            backButton.Location = new Point(151, 471);
            backButton.Name = "backButton";
            backButton.Size = new Size(75, 23);
            backButton.TabIndex = 14;
            backButton.Text = "취소";
            backButton.UseVisualStyleBackColor = true;
            backButton.Click += backButton_Click;
            // 
            // WorkPerformanceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(471, 545);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "WorkPerformanceForm";
            Text = "WorkPerformanceForm";
            FormClosing += WorkReportForm_FormClosing;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label WorkOrderNumLabel;
        private Label ProcessNameLabel;
        private Label EquipmentNameLabel;
        private Label GQtyLabel;
        private Panel panel1;
        private Label BQtyReasonLabel;
        private Label BQtyLabel;
        private Button EndButton;
        private Button ReportStoreButton;
        private TextBox BadReasonTextBox;
        private TextBox BQtyTextBox;
        private TextBox GQtyTextBox;
        private Label ProductNameLabel;
        private Label TotalOrderLabel;
        private Button backButton;
    }
}