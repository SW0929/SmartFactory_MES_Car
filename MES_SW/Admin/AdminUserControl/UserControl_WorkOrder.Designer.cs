namespace MES_SW.Admin
{
    partial class UserControl_WorkOrder
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
            ProductNameLabel = new Label();
            QuantityTextBox = new TextBox();
            QuantityLabel = new Label();
            StartDateLabel = new Label();
            StartDateTimePicker = new DateTimePicker();
            AddButton = new Button();
            UpdateButton = new Button();
            DeleteButton = new Button();
            dataGridView1 = new DataGridView();
            panel1 = new Panel();
            WorkOrderIDLabel = new Label();
            ProductNameComboBox = new ComboBox();
            StatusColour = new Label();
            AdminNameLabel = new Label();
            StatusLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new Font("맑은 고딕", 15F);
            label1.Location = new Point(22, 16);
            label1.Name = "label1";
            label1.Size = new Size(164, 35);
            label1.TabIndex = 0;
            label1.Text = "생산 지시 관리";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ProductNameLabel
            // 
            ProductNameLabel.AutoSize = true;
            ProductNameLabel.Location = new Point(32, 20);
            ProductNameLabel.Name = "ProductNameLabel";
            ProductNameLabel.Size = new Size(46, 15);
            ProductNameLabel.TabIndex = 1;
            ProductNameLabel.Text = "제품명:";
            // 
            // QuantityTextBox
            // 
            QuantityTextBox.Location = new Point(100, 88);
            QuantityTextBox.Name = "QuantityTextBox";
            QuantityTextBox.Size = new Size(100, 23);
            QuantityTextBox.TabIndex = 4;
            // 
            // QuantityLabel
            // 
            QuantityLabel.AutoSize = true;
            QuantityLabel.Location = new Point(32, 91);
            QuantityLabel.Name = "QuantityLabel";
            QuantityLabel.Size = new Size(62, 15);
            QuantityLabel.TabIndex = 3;
            QuantityLabel.Text = "지시 수량:";
            // 
            // StartDateLabel
            // 
            StartDateLabel.AutoSize = true;
            StartDateLabel.Location = new Point(32, 136);
            StartDateLabel.Name = "StartDateLabel";
            StartDateLabel.Size = new Size(46, 15);
            StartDateLabel.TabIndex = 5;
            StartDateLabel.Text = "시작일:";
            // 
            // StartDateTimePicker
            // 
            StartDateTimePicker.Location = new Point(100, 136);
            StartDateTimePicker.Name = "StartDateTimePicker";
            StartDateTimePicker.Size = new Size(200, 23);
            StartDateTimePicker.TabIndex = 9;
            StartDateTimePicker.Value = new DateTime(2025, 5, 28, 17, 23, 53, 0);
            // 
            // AddButton
            // 
            AddButton.Location = new Point(217, 488);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(75, 23);
            AddButton.TabIndex = 12;
            AddButton.Text = "등록";
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += AddButton_Click;
            // 
            // UpdateButton
            // 
            UpdateButton.Location = new Point(298, 488);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(75, 23);
            UpdateButton.TabIndex = 13;
            UpdateButton.Text = "수정";
            UpdateButton.UseVisualStyleBackColor = true;
            UpdateButton.Click += UpdateButton_Click;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(379, 488);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(75, 23);
            DeleteButton.TabIndex = 14;
            DeleteButton.Text = "삭제";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(22, 67);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(667, 393);
            dataGridView1.TabIndex = 15;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // panel1
            // 
            panel1.Controls.Add(WorkOrderIDLabel);
            panel1.Controls.Add(ProductNameComboBox);
            panel1.Controls.Add(StatusColour);
            panel1.Controls.Add(AdminNameLabel);
            panel1.Controls.Add(StatusLabel);
            panel1.Controls.Add(StartDateTimePicker);
            panel1.Controls.Add(StartDateLabel);
            panel1.Controls.Add(QuantityTextBox);
            panel1.Controls.Add(QuantityLabel);
            panel1.Controls.Add(ProductNameLabel);
            panel1.Location = new Point(753, 67);
            panel1.Name = "panel1";
            panel1.Size = new Size(314, 393);
            panel1.TabIndex = 16;
            // 
            // WorkOrderIDLabel
            // 
            WorkOrderIDLabel.AutoSize = true;
            WorkOrderIDLabel.Location = new Point(32, 278);
            WorkOrderIDLabel.Name = "WorkOrderIDLabel";
            WorkOrderIDLabel.Size = new Size(83, 15);
            WorkOrderIDLabel.TabIndex = 20;
            WorkOrderIDLabel.Text = "생산지시 번호";
            // 
            // ProductNameComboBox
            // 
            ProductNameComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ProductNameComboBox.FormattingEnabled = true;
            ProductNameComboBox.Location = new Point(100, 17);
            ProductNameComboBox.Name = "ProductNameComboBox";
            ProductNameComboBox.Size = new Size(121, 23);
            ProductNameComboBox.TabIndex = 19;
            // 
            // StatusColour
            // 
            StatusColour.BackColor = SystemColors.ActiveBorder;
            StatusColour.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            StatusColour.ForeColor = SystemColors.Control;
            StatusColour.Location = new Point(100, 302);
            StatusColour.Name = "StatusColour";
            StatusColour.Size = new Size(30, 30);
            StatusColour.TabIndex = 16;
            // 
            // AdminNameLabel
            // 
            AdminNameLabel.AutoSize = true;
            AdminNameLabel.Location = new Point(32, 236);
            AdminNameLabel.Name = "AdminNameLabel";
            AdminNameLabel.Size = new Size(43, 15);
            AdminNameLabel.TabIndex = 13;
            AdminNameLabel.Text = "관리자";
            // 
            // StatusLabel
            // 
            StatusLabel.AutoSize = true;
            StatusLabel.Location = new Point(32, 302);
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(31, 15);
            StatusLabel.TabIndex = 12;
            StatusLabel.Text = "상태";
            // 
            // UserControl_WorkOrder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            Controls.Add(DeleteButton);
            Controls.Add(UpdateButton);
            Controls.Add(panel1);
            Controls.Add(AddButton);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Name = "UserControl_WorkOrder";
            Size = new Size(1098, 600);
            Load += UserControl_WorkOrder_Load;
            Click += UserControl_WorkOrder_Click;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label ProductNameLabel;
        private TextBox QuantityTextBox;
        private Label QuantityLabel;
        private Label StartDateLabel;
        private DateTimePicker StartDateTimePicker;
        private Button AddButton;
        private Button UpdateButton;
        private Button DeleteButton;
        private DataGridView dataGridView1;
        private Panel panel1;
        private Label StatusLabel;
        private Label StatusColour;
        private Label AdminNameLabel;
        private ComboBox ProductNameComboBox;
        private Label WorkOrderIDLabel;
    }
}
