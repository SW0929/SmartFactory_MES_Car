using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MES_SW.Worker.WorkerUserControl
{
    public partial class WorkPerformanceForm : Form
    {
        private bool isSaved = false; // 저장 여부를 추적하는 변수
        public WorkPerformanceForm()
        {
            InitializeComponent();
        }

        private void ReportStoreButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("실직 등록이 완료되었습니다.", "실적 등록");
        }

        private void WorkReportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isSaved)
            {
                DialogResult result = MessageBox.Show("저장되지 않은 데이터가 있습니다. 종료하시겠습니까?",
                                                      "종료 확인", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
