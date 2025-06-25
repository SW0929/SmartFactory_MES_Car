namespace MES_SW.Admin
{
    partial class UserControl_EquipmentDefect
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
            EquipmentDefectInfoLabel = new Label();
            panel1 = new Panel();
            InspectButton = new Button();
            DefectDateLabel = new Label();
            DefectDateTimePicker = new DateTimePicker();
            EquipmentIDLabel = new Label();
            EquipmentTypeTextBox = new TextBox();
            DefectTypeLabel = new Label();
            EquipmentDefectDescriptionTextBox = new TextBox();
            EquipmentDefectDescriptionLabel = new Label();
            EquipmentNameTextBox = new TextBox();
            EquipmentName = new Label();
            SolvedButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(34, 24);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 0;
            label1.Text = "설비 관리";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(34, 68);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(700, 402);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // EquipmentDefectInfoLabel
            // 
            EquipmentDefectInfoLabel.AllowDrop = true;
            EquipmentDefectInfoLabel.BorderStyle = BorderStyle.FixedSingle;
            EquipmentDefectInfoLabel.Location = new Point(758, 53);
            EquipmentDefectInfoLabel.Name = "EquipmentDefectInfoLabel";
            EquipmentDefectInfoLabel.Size = new Size(100, 23);
            EquipmentDefectInfoLabel.TabIndex = 5;
            EquipmentDefectInfoLabel.Text = "결함 관리";
            EquipmentDefectInfoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(InspectButton);
            panel1.Controls.Add(DefectDateLabel);
            panel1.Controls.Add(DefectDateTimePicker);
            panel1.Controls.Add(EquipmentIDLabel);
            panel1.Controls.Add(EquipmentTypeTextBox);
            panel1.Controls.Add(DefectTypeLabel);
            panel1.Controls.Add(EquipmentDefectDescriptionTextBox);
            panel1.Controls.Add(EquipmentDefectDescriptionLabel);
            panel1.Controls.Add(EquipmentNameTextBox);
            panel1.Controls.Add(EquipmentName);
            panel1.Controls.Add(SolvedButton);
            panel1.Location = new Point(740, 68);
            panel1.Name = "panel1";
            panel1.Size = new Size(352, 522);
            panel1.TabIndex = 4;
            // 
            // InspectButton
            // 
            InspectButton.Location = new Point(30, 378);
            InspectButton.Name = "InspectButton";
            InspectButton.Size = new Size(296, 42);
            InspectButton.TabIndex = 4;
            InspectButton.Text = "점검";
            InspectButton.UseVisualStyleBackColor = true;
            InspectButton.Click += InspectButton_Click;
            // 
            // DefectDateLabel
            // 
            DefectDateLabel.AutoSize = true;
            DefectDateLabel.Location = new Point(30, 133);
            DefectDateLabel.Name = "DefectDateLabel";
            DefectDateLabel.Size = new Size(71, 15);
            DefectDateLabel.TabIndex = 15;
            DefectDateLabel.Text = "결함 발생일";
            // 
            // DefectDateTimePicker
            // 
            DefectDateTimePicker.Enabled = false;
            DefectDateTimePicker.Location = new Point(30, 151);
            DefectDateTimePicker.Name = "DefectDateTimePicker";
            DefectDateTimePicker.Size = new Size(193, 23);
            DefectDateTimePicker.TabIndex = 2;
            // 
            // EquipmentIDLabel
            // 
            EquipmentIDLabel.AutoSize = true;
            EquipmentIDLabel.Location = new Point(268, 13);
            EquipmentIDLabel.Name = "EquipmentIDLabel";
            EquipmentIDLabel.Size = new Size(79, 15);
            EquipmentIDLabel.TabIndex = 11;
            EquipmentIDLabel.Text = "설비결함번호";
            EquipmentIDLabel.Visible = false;
            // 
            // EquipmentTypeTextBox
            // 
            EquipmentTypeTextBox.Location = new Point(107, 88);
            EquipmentTypeTextBox.Name = "EquipmentTypeTextBox";
            EquipmentTypeTextBox.Size = new Size(100, 23);
            EquipmentTypeTextBox.TabIndex = 1;
            // 
            // DefectTypeLabel
            // 
            DefectTypeLabel.AutoSize = true;
            DefectTypeLabel.Location = new Point(30, 91);
            DefectTypeLabel.Name = "DefectTypeLabel";
            DefectTypeLabel.Size = new Size(59, 15);
            DefectTypeLabel.TabIndex = 7;
            DefectTypeLabel.Text = "결함 유형";
            // 
            // EquipmentDefectDescriptionTextBox
            // 
            EquipmentDefectDescriptionTextBox.Location = new Point(30, 212);
            EquipmentDefectDescriptionTextBox.Multiline = true;
            EquipmentDefectDescriptionTextBox.Name = "EquipmentDefectDescriptionTextBox";
            EquipmentDefectDescriptionTextBox.Size = new Size(296, 145);
            EquipmentDefectDescriptionTextBox.TabIndex = 3;
            // 
            // EquipmentDefectDescriptionLabel
            // 
            EquipmentDefectDescriptionLabel.AutoSize = true;
            EquipmentDefectDescriptionLabel.Location = new Point(30, 194);
            EquipmentDefectDescriptionLabel.Name = "EquipmentDefectDescriptionLabel";
            EquipmentDefectDescriptionLabel.Size = new Size(59, 15);
            EquipmentDefectDescriptionLabel.TabIndex = 5;
            EquipmentDefectDescriptionLabel.Text = "결함 사유";
            // 
            // EquipmentNameTextBox
            // 
            EquipmentNameTextBox.Location = new Point(107, 41);
            EquipmentNameTextBox.Name = "EquipmentNameTextBox";
            EquipmentNameTextBox.Size = new Size(100, 23);
            EquipmentNameTextBox.TabIndex = 0;
            // 
            // EquipmentName
            // 
            EquipmentName.AutoSize = true;
            EquipmentName.Location = new Point(30, 44);
            EquipmentName.Name = "EquipmentName";
            EquipmentName.Size = new Size(43, 15);
            EquipmentName.TabIndex = 1;
            EquipmentName.Text = "설비명";
            // 
            // SolvedButton
            // 
            SolvedButton.Location = new Point(30, 445);
            SolvedButton.Name = "SolvedButton";
            SolvedButton.Size = new Size(296, 39);
            SolvedButton.TabIndex = 5;
            SolvedButton.Text = "해결";
            SolvedButton.UseVisualStyleBackColor = true;
            SolvedButton.Click += SolvedButton_Click;
            // 
            // UserControl_EquipmentDefect
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            Controls.Add(EquipmentDefectInfoLabel);
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Name = "UserControl_EquipmentDefect";
            Size = new Size(1098, 600);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dataGridView1;
        private Label EquipmentDefectInfoLabel;
        private Panel panel1;
        private Label EquipmentIDLabel;
        private TextBox EquipmentTypeTextBox;
        private Label DefectTypeLabel;
        private TextBox EquipmentDefectDescriptionTextBox;
        private Label EquipmentDefectDescriptionLabel;
        private TextBox EquipmentNameTextBox;
        private Label EquipmentName;
        private Button SolvedButton;
        private Label DefectDateLabel;
        private DateTimePicker DefectDateTimePicker;
        private Button InspectButton;
    }
}
