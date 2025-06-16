namespace MES_SW.Worker.WorkerUserControl
{
    partial class UserControl_WorkOrderList
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
            StartButton = new Button();
            EndButton = new Button();
            WorkOrderID = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(50, 58);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(993, 124);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            // 
            // StartButton
            // 
            StartButton.Location = new Point(254, 492);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(75, 23);
            StartButton.TabIndex = 1;
            StartButton.Text = "시작";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // EndButton
            // 
            EndButton.Location = new Point(750, 492);
            EndButton.Name = "EndButton";
            EndButton.Size = new Size(75, 23);
            EndButton.TabIndex = 2;
            EndButton.Text = "종료";
            EndButton.UseVisualStyleBackColor = true;
            EndButton.Click += EndButton_Click;
            // 
            // WorkOrderID
            // 
            WorkOrderID.AutoSize = true;
            WorkOrderID.Location = new Point(50, 27);
            WorkOrderID.Name = "WorkOrderID";
            WorkOrderID.Size = new Size(83, 15);
            WorkOrderID.TabIndex = 3;
            WorkOrderID.Text = "생산지시 번호";
            // 
            // UserControl_WorkOrderList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            Controls.Add(WorkOrderID);
            Controls.Add(EndButton);
            Controls.Add(StartButton);
            Controls.Add(dataGridView1);
            Name = "UserControl_WorkOrderList";
            Size = new Size(1113, 580);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button StartButton;
        private Button EndButton;
        private Label WorkOrderID;
    }
}
