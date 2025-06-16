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
            ProductNameLabel = new Label();
            ProcessNameLabel = new Label();
            EquipmentNameLabel = new Label();
            label3 = new Label();
            GQtyLabel = new Label();
            panel1 = new Panel();
            EndButton = new Button();
            ReportStoreButton = new Button();
            BadReasonTextBox = new TextBox();
            BQtyTextBox = new TextBox();
            GQtyTextBox = new TextBox();
            BQtyReasonLabel = new Label();
            BQtyLabel = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // ProductNameLabel
            // 
            ProductNameLabel.AutoSize = true;
            ProductNameLabel.Location = new Point(6, 12);
            ProductNameLabel.Name = "ProductNameLabel";
            ProductNameLabel.Size = new Size(90, 15);
            ProductNameLabel.TabIndex = 0;
            ProductNameLabel.Text = "제품명 : 소나타";
            // 
            // ProcessNameLabel
            // 
            ProcessNameLabel.AutoSize = true;
            ProcessNameLabel.Location = new Point(6, 61);
            ProcessNameLabel.Name = "ProcessNameLabel";
            ProcessNameLabel.Size = new Size(90, 15);
            ProcessNameLabel.TabIndex = 1;
            ProcessNameLabel.Text = "공정명 : 프레스";
            // 
            // EquipmentNameLabel
            // 
            EquipmentNameLabel.AutoSize = true;
            EquipmentNameLabel.Location = new Point(133, 12);
            EquipmentNameLabel.Name = "EquipmentNameLabel";
            EquipmentNameLabel.Size = new Size(98, 15);
            EquipmentNameLabel.TabIndex = 2;
            EquipmentNameLabel.Text = "설비명 : 프레스A";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(133, 61);
            label3.Name = "label3";
            label3.Size = new Size(167, 15);
            label3.TabIndex = 3;
            label3.Text = "시작시간 : 2025-06-16 14:00";
            // 
            // GQtyLabel
            // 
            GQtyLabel.AutoSize = true;
            GQtyLabel.Location = new Point(6, 107);
            GQtyLabel.Name = "GQtyLabel";
            GQtyLabel.Size = new Size(59, 15);
            GQtyLabel.TabIndex = 4;
            GQtyLabel.Text = "양품 수량";
            // 
            // panel1
            // 
            panel1.Controls.Add(EndButton);
            panel1.Controls.Add(ReportStoreButton);
            panel1.Controls.Add(BadReasonTextBox);
            panel1.Controls.Add(BQtyTextBox);
            panel1.Controls.Add(GQtyTextBox);
            panel1.Controls.Add(BQtyReasonLabel);
            panel1.Controls.Add(BQtyLabel);
            panel1.Controls.Add(GQtyLabel);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(EquipmentNameLabel);
            panel1.Controls.Add(ProcessNameLabel);
            panel1.Controls.Add(ProductNameLabel);
            panel1.Location = new Point(25, 17);
            panel1.Name = "panel1";
            panel1.Size = new Size(388, 465);
            panel1.TabIndex = 5;
            // 
            // EndButton
            // 
            EndButton.Location = new Point(259, 376);
            EndButton.Name = "EndButton";
            EndButton.Size = new Size(75, 23);
            EndButton.TabIndex = 11;
            EndButton.Text = "작업 종료";
            EndButton.UseVisualStyleBackColor = true;
            // 
            // ReportStoreButton
            // 
            ReportStoreButton.Location = new Point(156, 376);
            ReportStoreButton.Name = "ReportStoreButton";
            ReportStoreButton.Size = new Size(75, 23);
            ReportStoreButton.TabIndex = 10;
            ReportStoreButton.Text = "실적 저장";
            ReportStoreButton.UseVisualStyleBackColor = true;
            ReportStoreButton.Click += ReportStoreButton_Click;
            // 
            // BadReasonTextBox
            // 
            BadReasonTextBox.Location = new Point(102, 179);
            BadReasonTextBox.Multiline = true;
            BadReasonTextBox.Name = "BadReasonTextBox";
            BadReasonTextBox.Size = new Size(232, 158);
            BadReasonTextBox.TabIndex = 9;
            // 
            // BQtyTextBox
            // 
            BQtyTextBox.Location = new Point(102, 144);
            BQtyTextBox.Name = "BQtyTextBox";
            BQtyTextBox.Size = new Size(100, 23);
            BQtyTextBox.TabIndex = 8;
            // 
            // GQtyTextBox
            // 
            GQtyTextBox.Location = new Point(102, 104);
            GQtyTextBox.Name = "GQtyTextBox";
            GQtyTextBox.Size = new Size(100, 23);
            GQtyTextBox.TabIndex = 7;
            // 
            // BQtyReasonLabel
            // 
            BQtyReasonLabel.AutoSize = true;
            BQtyReasonLabel.Location = new Point(6, 179);
            BQtyReasonLabel.Name = "BQtyReasonLabel";
            BQtyReasonLabel.Size = new Size(59, 15);
            BQtyReasonLabel.TabIndex = 6;
            BQtyReasonLabel.Text = "불량 사유";
            // 
            // BQtyLabel
            // 
            BQtyLabel.AutoSize = true;
            BQtyLabel.Location = new Point(6, 147);
            BQtyLabel.Name = "BQtyLabel";
            BQtyLabel.Size = new Size(59, 15);
            BQtyLabel.TabIndex = 5;
            BQtyLabel.Text = "불량 수량";
            // 
            // WorkPerformanceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(471, 545);
            Controls.Add(panel1);
            Name = "WorkPerformanceForm";
            Text = "WorkPerformanceForm";
            FormClosing += WorkReportForm_FormClosing;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label ProductNameLabel;
        private Label ProcessNameLabel;
        private Label EquipmentNameLabel;
        private Label label3;
        private Label GQtyLabel;
        private Panel panel1;
        private Label BQtyReasonLabel;
        private Label BQtyLabel;
        private Button EndButton;
        private Button ReportStoreButton;
        private TextBox BadReasonTextBox;
        private TextBox BQtyTextBox;
        private TextBox GQtyTextBox;
    }
}