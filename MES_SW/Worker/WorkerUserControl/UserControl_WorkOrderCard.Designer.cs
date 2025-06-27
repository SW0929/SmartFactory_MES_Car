
namespace MES_SW.Worker.WorkerUserControl
{
    partial class UserControl_WorkOrderCard
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
            ProcessName = new Label();
            ProductName = new Label();
            QtyLabel = new Label();
            OrderDate = new Label();
            IssuByLabel = new Label();
            StatusLabel = new Label();
            progressBar1 = new ProgressBar();
            StartButton = new Button();
            WorkOrderIDLabel = new Label();
            SuspendLayout();
            // 
            // ProcessName
            // 
            ProcessName.AutoSize = true;
            ProcessName.Location = new Point(20, 32);
            ProcessName.Name = "ProcessName";
            ProcessName.Size = new Size(107, 15);
            ProcessName.TabIndex = 0;
            ProcessName.Text = "🔧 [공정명] 프레스";
            // 
            // ProductName
            // 
            ProductName.AutoSize = true;
            ProductName.Location = new Point(20, 61);
            ProductName.Name = "ProductName";
            ProductName.Size = new Size(82, 15);
            ProductName.TabIndex = 1;
            ProductName.Text = "📦 제품 : 투싼";
            // 
            // QtyLabel
            // 
            QtyLabel.AutoSize = true;
            QtyLabel.Location = new Point(20, 88);
            QtyLabel.Name = "QtyLabel";
            QtyLabel.Size = new Size(70, 15);
            QtyLabel.TabIndex = 2;
            QtyLabel.Text = "📋 수량 : 10";
            // 
            // OrderDate
            // 
            OrderDate.AutoSize = true;
            OrderDate.Location = new Point(20, 121);
            OrderDate.Name = "OrderDate";
            OrderDate.Size = new Size(148, 15);
            OrderDate.TabIndex = 3;
            OrderDate.Text = "📅 지시일자 : 2025-06-27";
            // 
            // IssuByLabel
            // 
            IssuByLabel.AutoSize = true;
            IssuByLabel.Location = new Point(20, 150);
            IssuByLabel.Name = "IssuByLabel";
            IssuByLabel.Size = new Size(104, 15);
            IssuByLabel.TabIndex = 4;
            IssuByLabel.Text = "👤 지시자 : 이상원";
            // 
            // StatusLabel
            // 
            StatusLabel.AutoSize = true;
            StatusLabel.Location = new Point(20, 178);
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(98, 15);
            StatusLabel.TabIndex = 5;
            StatusLabel.Text = "🔄 상태 : 진행 중";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(20, 212);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(225, 25);
            progressBar1.TabIndex = 6;
            // 
            // StartButton
            // 
            StartButton.Location = new Point(20, 243);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(225, 22);
            StartButton.TabIndex = 7;
            StartButton.Text = "시작";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // WorkOrderIDLabel
            // 
            WorkOrderIDLabel.AutoSize = true;
            WorkOrderIDLabel.Location = new Point(20, 13);
            WorkOrderIDLabel.Name = "WorkOrderIDLabel";
            WorkOrderIDLabel.Size = new Size(94, 15);
            WorkOrderIDLabel.TabIndex = 8;
            WorkOrderIDLabel.Text = "작업번호 : 1000";
            // 
            // UserControal_WorkOrderCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(WorkOrderIDLabel);
            Controls.Add(StartButton);
            Controls.Add(progressBar1);
            Controls.Add(StatusLabel);
            Controls.Add(IssuByLabel);
            Controls.Add(OrderDate);
            Controls.Add(QtyLabel);
            Controls.Add(ProductName);
            Controls.Add(ProcessName);
            Name = "UserControal_WorkOrderCard";
            Size = new Size(269, 277);
            DoubleClick += this.UserControl_WorkOrderCard_DoubleClick;
            ResumeLayout(false);
            PerformLayout();
        }

        

        #endregion

        private Label ProcessName;
        private Label ProductName;
        private Label QtyLabel;
        private Label OrderDate;
        private Label IssuByLabel;
        private Label StatusLabel;
        private ProgressBar progressBar1;
        private Button StartButton;
        private Label WorkOrderIDLabel;
    }
}
