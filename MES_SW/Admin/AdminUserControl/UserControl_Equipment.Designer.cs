namespace MES_SW.Admin
{
    partial class UserControl_Equipment
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
            panel1 = new Panel();
            EquipmentIDLabel = new Label();
            ProcessIDComboBox = new ComboBox();
            ProcessIDLabel = new Label();
            EquipmentTypeTextBox = new TextBox();
            EquipmentTypeLabel = new Label();
            EquipmentStatusTextBox = new TextBox();
            EquipmentStatusLabel = new Label();
            EquipmentNameTextBox = new TextBox();
            DeleteButton = new Button();
            UpdateButton = new Button();
            EquipmentName = new Label();
            AddButton = new Button();
            EquipmentInfoLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(21, 11);
            label1.Name = "label1";
            label1.Size = new Size(111, 30);
            label1.TabIndex = 0;
            label1.Text = "설비 목록";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(21, 73);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(632, 465);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(EquipmentIDLabel);
            panel1.Controls.Add(ProcessIDComboBox);
            panel1.Controls.Add(ProcessIDLabel);
            panel1.Controls.Add(EquipmentTypeTextBox);
            panel1.Controls.Add(EquipmentTypeLabel);
            panel1.Controls.Add(EquipmentStatusTextBox);
            panel1.Controls.Add(EquipmentStatusLabel);
            panel1.Controls.Add(EquipmentNameTextBox);
            panel1.Controls.Add(DeleteButton);
            panel1.Controls.Add(UpdateButton);
            panel1.Controls.Add(EquipmentName);
            panel1.Controls.Add(AddButton);
            panel1.Location = new Point(706, 73);
            panel1.Name = "panel1";
            panel1.Size = new Size(352, 465);
            panel1.TabIndex = 2;
            // 
            // EquipmentIDLabel
            // 
            EquipmentIDLabel.AutoSize = true;
            EquipmentIDLabel.Location = new Point(41, 32);
            EquipmentIDLabel.Name = "EquipmentIDLabel";
            EquipmentIDLabel.Size = new Size(55, 15);
            EquipmentIDLabel.TabIndex = 11;
            EquipmentIDLabel.Text = "설비번호";
            EquipmentIDLabel.Visible = false;
            // 
            // ProcessIDComboBox
            // 
            ProcessIDComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ProcessIDComboBox.FormattingEnabled = true;
            ProcessIDComboBox.Location = new Point(122, 258);
            ProcessIDComboBox.Name = "ProcessIDComboBox";
            ProcessIDComboBox.Size = new Size(121, 23);
            ProcessIDComboBox.TabIndex = 3;
            // 
            // ProcessIDLabel
            // 
            ProcessIDLabel.AutoSize = true;
            ProcessIDLabel.Location = new Point(45, 261);
            ProcessIDLabel.Name = "ProcessIDLabel";
            ProcessIDLabel.Size = new Size(31, 15);
            ProcessIDLabel.TabIndex = 9;
            ProcessIDLabel.Text = "공정";
            // 
            // EquipmentTypeTextBox
            // 
            EquipmentTypeTextBox.Location = new Point(122, 147);
            EquipmentTypeTextBox.Name = "EquipmentTypeTextBox";
            EquipmentTypeTextBox.Size = new Size(100, 23);
            EquipmentTypeTextBox.TabIndex = 1;
            // 
            // EquipmentTypeLabel
            // 
            EquipmentTypeLabel.AutoSize = true;
            EquipmentTypeLabel.Location = new Point(45, 150);
            EquipmentTypeLabel.Name = "EquipmentTypeLabel";
            EquipmentTypeLabel.Size = new Size(31, 15);
            EquipmentTypeLabel.TabIndex = 7;
            EquipmentTypeLabel.Text = "종류";
            // 
            // EquipmentStatusTextBox
            // 
            EquipmentStatusTextBox.Location = new Point(122, 199);
            EquipmentStatusTextBox.Name = "EquipmentStatusTextBox";
            EquipmentStatusTextBox.Size = new Size(100, 23);
            EquipmentStatusTextBox.TabIndex = 2;
            // 
            // EquipmentStatusLabel
            // 
            EquipmentStatusLabel.AutoSize = true;
            EquipmentStatusLabel.Location = new Point(45, 207);
            EquipmentStatusLabel.Name = "EquipmentStatusLabel";
            EquipmentStatusLabel.Size = new Size(31, 15);
            EquipmentStatusLabel.TabIndex = 5;
            EquipmentStatusLabel.Text = "상태";
            // 
            // EquipmentNameTextBox
            // 
            EquipmentNameTextBox.Location = new Point(122, 100);
            EquipmentNameTextBox.Name = "EquipmentNameTextBox";
            EquipmentNameTextBox.Size = new Size(100, 23);
            EquipmentNameTextBox.TabIndex = 0;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(203, 408);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(75, 23);
            DeleteButton.TabIndex = 6;
            DeleteButton.Text = "삭제";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // UpdateButton
            // 
            UpdateButton.Location = new Point(122, 408);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(75, 23);
            UpdateButton.TabIndex = 5;
            UpdateButton.Text = "수정";
            UpdateButton.UseVisualStyleBackColor = true;
            UpdateButton.Click += UpdateButton_Click;
            // 
            // EquipmentName
            // 
            EquipmentName.AutoSize = true;
            EquipmentName.Location = new Point(45, 103);
            EquipmentName.Name = "EquipmentName";
            EquipmentName.Size = new Size(43, 15);
            EquipmentName.TabIndex = 1;
            EquipmentName.Text = "설비명";
            // 
            // AddButton
            // 
            AddButton.Location = new Point(41, 408);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(75, 23);
            AddButton.TabIndex = 4;
            AddButton.Text = "추가";
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += AddButton_Click;
            // 
            // EquipmentInfoLabel
            // 
            EquipmentInfoLabel.AllowDrop = true;
            EquipmentInfoLabel.BorderStyle = BorderStyle.FixedSingle;
            EquipmentInfoLabel.Location = new Point(723, 58);
            EquipmentInfoLabel.Name = "EquipmentInfoLabel";
            EquipmentInfoLabel.Size = new Size(100, 23);
            EquipmentInfoLabel.TabIndex = 3;
            EquipmentInfoLabel.Text = "설비 정보";
            EquipmentInfoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UserControl_Equipment
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            Controls.Add(EquipmentInfoLabel);
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Name = "UserControl_Equipment";
            Size = new Size(1098, 600);
            Load += UserControl_Equipment_Load;
            Click += UserControl_Equipment_Click;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private DataGridView dataGridView1;
        private Panel panel1;
        private Label EquipmentInfoLabel;
        private TextBox EquipmentTypeTextBox;
        private Label EquipmentTypeLabel;
        private TextBox EquipmentStatusTextBox;
        private Label EquipmentStatusLabel;
        private TextBox EquipmentNameTextBox;
        private Button DeleteButton;
        private Button UpdateButton;
        private Label EquipmentName;
        private Button AddButton;
        private ComboBox ProcessIDComboBox;
        private Label ProcessIDLabel;
        private Label EquipmentIDLabel;
    }
}
