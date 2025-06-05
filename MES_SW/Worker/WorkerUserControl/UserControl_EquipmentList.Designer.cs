namespace MES_SW.Worker.WorkerUserControl
{
    partial class UserControl_EquipmentList
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
            EquipmentIDLabel = new Label();
            EquipmentIDTextBox = new TextBox();
            DefectTypeTextBox = new TextBox();
            DefectTypeLabel = new Label();
            DescriptionTextBox = new TextBox();
            DescriptionLabel = new Label();
            AddButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(16, 15);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(737, 414);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // EquipmentIDLabel
            // 
            EquipmentIDLabel.AutoSize = true;
            EquipmentIDLabel.Location = new Point(832, 60);
            EquipmentIDLabel.Name = "EquipmentIDLabel";
            EquipmentIDLabel.Size = new Size(47, 15);
            EquipmentIDLabel.TabIndex = 1;
            EquipmentIDLabel.Text = "설비 ID";
            // 
            // EquipmentIDTextBox
            // 
            EquipmentIDTextBox.Location = new Point(921, 52);
            EquipmentIDTextBox.Name = "EquipmentIDTextBox";
            EquipmentIDTextBox.Size = new Size(100, 23);
            EquipmentIDTextBox.TabIndex = 2;
            // 
            // DefectTypeTextBox
            // 
            DefectTypeTextBox.Location = new Point(921, 140);
            DefectTypeTextBox.Name = "DefectTypeTextBox";
            DefectTypeTextBox.Size = new Size(100, 23);
            DefectTypeTextBox.TabIndex = 4;
            // 
            // DefectTypeLabel
            // 
            DefectTypeLabel.AutoSize = true;
            DefectTypeLabel.Location = new Point(832, 148);
            DefectTypeLabel.Name = "DefectTypeLabel";
            DefectTypeLabel.Size = new Size(59, 15);
            DefectTypeLabel.TabIndex = 3;
            DefectTypeLabel.Text = "결함 유형";
            // 
            // DescriptionTextBox
            // 
            DescriptionTextBox.Location = new Point(832, 275);
            DescriptionTextBox.Multiline = true;
            DescriptionTextBox.Name = "DescriptionTextBox";
            DescriptionTextBox.Size = new Size(234, 154);
            DescriptionTextBox.TabIndex = 6;
            // 
            // DescriptionLabel
            // 
            DescriptionLabel.AutoSize = true;
            DescriptionLabel.Location = new Point(832, 242);
            DescriptionLabel.Name = "DescriptionLabel";
            DescriptionLabel.Size = new Size(31, 15);
            DescriptionLabel.TabIndex = 5;
            DescriptionLabel.Text = "설명";
            // 
            // AddButton
            // 
            AddButton.Location = new Point(832, 460);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(234, 28);
            AddButton.TabIndex = 7;
            AddButton.Text = "등록";
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += AddButton_Click;
            // 
            // UserControl_EquipmentList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            Controls.Add(AddButton);
            Controls.Add(DescriptionTextBox);
            Controls.Add(DescriptionLabel);
            Controls.Add(DefectTypeTextBox);
            Controls.Add(DefectTypeLabel);
            Controls.Add(EquipmentIDTextBox);
            Controls.Add(EquipmentIDLabel);
            Controls.Add(dataGridView1);
            Name = "UserControl_EquipmentList";
            Size = new Size(1113, 580);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label EquipmentIDLabel;
        private TextBox EquipmentIDTextBox;
        private TextBox DefectTypeTextBox;
        private Label DefectTypeLabel;
        private TextBox DescriptionTextBox;
        private Label DescriptionLabel;
        private Button AddButton;
    }
}
