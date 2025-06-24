using MES_SW.Data;
using MES_SW.Data.Worker;
using MES_SW.Services.Worker;
using MES_SW.Worker.Models;
using Microsoft.Data.SqlClient;
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
    public partial class UserControl_WorkPerformance : UserControl
    {

        private WorkPerformanceService _workPerformanceService;

        private int _userID;
        private int _performanceID;
        private int _totalQty; // 총 생산량`
        public UserControl_WorkPerformance(int userID)
        {
            InitializeComponent();
            _userID = userID;
            _workPerformanceService = new WorkPerformanceService();
            LoadWorkPerformanceForm();
        }

        private void LoadWorkPerformanceForm()
        {
            dataGridView1.DataSource = _workPerformanceService.GetUserPerformances(_userID);
            dataGridView1.Columns["Reason"].Visible = false; // Hide the Reason column
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[rowIndex];

            string goodQtyStr = row.Cells["GoodQty"].Value?.ToString() ?? "0";
            string defectQtyStr = row.Cells["DefectQty"].Value?.ToString() ?? "0";
            string performanceIDStr = row.Cells["PerformanceID"].Value?.ToString() ?? "";
            string reasonStr = row.Cells["Reason"].Value?.ToString() ?? "";

            bool goodQtyOk = int.TryParse(goodQtyStr, out int goodQty);
            bool defectQtyOk = int.TryParse(defectQtyStr, out int defectQty);
            bool performanceIDOk = int.TryParse(performanceIDStr, out int performanceID);

            if (goodQtyOk && defectQtyOk && performanceIDOk)
            {
                _performanceID = performanceID;
                _totalQty = goodQty + defectQty;
                
                GQtyTextBox.Text = goodQtyStr;
                BQtyTextBox.Text = defectQtyStr;
                BadReasonTextBox.Text = reasonStr;
            }
            else
            {
                MessageBox.Show("숫자 형식이 잘못되었습니다.");
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            int goodQty = int.Parse(GQtyTextBox.Text);
            int defectQty = int.Parse(BQtyTextBox.Text);
            string reason = BadReasonTextBox.Text;

            if (_workPerformanceService.UpdatePerformance(_performanceID, goodQty, defectQty, reason))
            {
                MessageBox.Show("작업 성과가 성공적으로 업데이트되었습니다.");
                LoadWorkPerformanceForm();
            }
            else
            {
                MessageBox.Show("작업 성과 업데이트에 실패했습니다.");
            }

        }
        private void GQtyTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_totalQty == 0) return;

            if (int.TryParse(GQtyTextBox.Text, out int goodQty))
            {
                if (goodQty > _totalQty || goodQty < 0)
                {
                    MessageBox.Show("수량을 제대로 입력하세요.");
                    GQtyTextBox.Text = _totalQty.ToString();
                    return;
                }
                int defectQty = _totalQty - goodQty;
                BQtyTextBox.Text = defectQty.ToString();
            }
            else
            {
                BQtyTextBox.Text = string.Empty;
            }
        }
        // 숫자만 입력 가능
        private void GQtyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // 숫자와 백스페이스 외의 키 입력을 무시
            }
        }

        /* 실적 삭제 (필요 없음)
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string query = @"DELETE FROM WorkPerformance WHERE PerformanceID = @PerformanceID";

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("@PerformanceID", _performanceID)
            };

            try
            {
                int rowAffected = DBHelper.ExecuteNonQuery(query, sqlParameter);

                if (rowAffected > 0)
                {
                    MessageBox.Show("작업 성과가 성공적으로 삭제되었습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadWorkPerformanceForm(); // Refresh the data grid view
                }
                else
                {
                    MessageBox.Show("작업 성과 삭제에 실패했습니다.", "실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        */
    }
}
