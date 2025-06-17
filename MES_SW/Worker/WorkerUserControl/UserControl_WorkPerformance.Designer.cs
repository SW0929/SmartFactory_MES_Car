namespace MES_SW.Worker.WorkerUserControl
{
    partial class UserControl_WorkPerformance
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
            dataGridView1 = new DataGridView();
            panel1 = new Panel();
            ProductNameLabel = new Label();
            EndButton = new Button();
            ReportStoreButton = new Button();
            BadReasonTextBox = new TextBox();
            BQtyTextBox = new TextBox();
            GQtyTextBox = new TextBox();
            BQtyReasonLabel = new Label();
            BQtyLabel = new Label();
            GQtyLabel = new Label();
            EquipmentNameLabel = new Label();
            ProcessNameLabel = new Label();
            WorkOrderNumLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(70, 60);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(970, 171);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // panel1
            // 
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
            panel1.Location = new Point(105, 237);
            panel1.Name = "panel1";
            panel1.Size = new Size(871, 326);
            panel1.TabIndex = 6;
            // 
            // ProductNameLabel
            // 
            ProductNameLabel.AutoSize = true;
            ProductNameLabel.Location = new Point(435, 12);
            ProductNameLabel.Name = "ProductNameLabel";
            ProductNameLabel.Size = new Size(78, 15);
            ProductNameLabel.TabIndex = 12;
            ProductNameLabel.Text = "제품명 : 투싼";
            // 
            // EndButton
            // 
            EndButton.Location = new Point(544, 290);
            EndButton.Name = "EndButton";
            EndButton.Size = new Size(75, 23);
            EndButton.TabIndex = 11;
            EndButton.Text = "작업 종료";
            EndButton.UseVisualStyleBackColor = true;
            // 
            // ReportStoreButton
            // 
            ReportStoreButton.Location = new Point(372, 290);
            ReportStoreButton.Name = "ReportStoreButton";
            ReportStoreButton.Size = new Size(75, 23);
            ReportStoreButton.TabIndex = 10;
            ReportStoreButton.Text = "실적 저장";
            ReportStoreButton.UseVisualStyleBackColor = true;
            // 
            // BadReasonTextBox
            // 
            BadReasonTextBox.Location = new Point(12, 155);
            BadReasonTextBox.Multiline = true;
            BadReasonTextBox.Name = "BadReasonTextBox";
            BadReasonTextBox.Size = new Size(232, 158);
            BadReasonTextBox.TabIndex = 9;
            // 
            // BQtyTextBox
            // 
            BQtyTextBox.Location = new Point(372, 79);
            BQtyTextBox.Name = "BQtyTextBox";
            BQtyTextBox.Size = new Size(100, 23);
            BQtyTextBox.TabIndex = 8;
            // 
            // GQtyTextBox
            // 
            GQtyTextBox.Location = new Point(108, 76);
            GQtyTextBox.Name = "GQtyTextBox";
            GQtyTextBox.Size = new Size(100, 23);
            GQtyTextBox.TabIndex = 7;
            // 
            // BQtyReasonLabel
            // 
            BQtyReasonLabel.AutoSize = true;
            BQtyReasonLabel.Location = new Point(12, 136);
            BQtyReasonLabel.Name = "BQtyReasonLabel";
            BQtyReasonLabel.Size = new Size(59, 15);
            BQtyReasonLabel.TabIndex = 6;
            BQtyReasonLabel.Text = "불량 사유";
            // 
            // BQtyLabel
            // 
            BQtyLabel.AutoSize = true;
            BQtyLabel.Location = new Point(276, 82);
            BQtyLabel.Name = "BQtyLabel";
            BQtyLabel.Size = new Size(59, 15);
            BQtyLabel.TabIndex = 5;
            BQtyLabel.Text = "불량 수량";
            // 
            // GQtyLabel
            // 
            GQtyLabel.AutoSize = true;
            GQtyLabel.Location = new Point(12, 79);
            GQtyLabel.Name = "GQtyLabel";
            GQtyLabel.Size = new Size(59, 15);
            GQtyLabel.TabIndex = 4;
            GQtyLabel.Text = "양품 수량";
            // 
            // EquipmentNameLabel
            // 
            EquipmentNameLabel.AutoSize = true;
            EquipmentNameLabel.Location = new Point(268, 12);
            EquipmentNameLabel.Name = "EquipmentNameLabel";
            EquipmentNameLabel.Size = new Size(98, 15);
            EquipmentNameLabel.TabIndex = 2;
            EquipmentNameLabel.Text = "설비명 : 프레스A";
            // 
            // ProcessNameLabel
            // 
            ProcessNameLabel.AutoSize = true;
            ProcessNameLabel.Location = new Point(147, 12);
            ProcessNameLabel.Name = "ProcessNameLabel";
            ProcessNameLabel.Size = new Size(49, 15);
            ProcessNameLabel.TabIndex = 1;
            ProcessNameLabel.Text = "공정 : 1";
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
            // UserControl_WorkPerformance
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Name = "UserControl_WorkPerformance";
            Size = new Size(1113, 580);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Panel panel1;
        private Label ProductNameLabel;
        private Button EndButton;
        private Button ReportStoreButton;
        private TextBox BadReasonTextBox;
        private TextBox BQtyTextBox;
        private TextBox GQtyTextBox;
        private Label BQtyReasonLabel;
        private Label BQtyLabel;
        private Label GQtyLabel;
        private Label EquipmentNameLabel;
        private Label ProcessNameLabel;
        private Label WorkOrderNumLabel;
    }
}
