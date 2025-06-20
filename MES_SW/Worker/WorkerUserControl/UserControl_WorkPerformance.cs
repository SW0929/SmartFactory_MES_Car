using MES_SW.Data;
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
        private int _userID;
        private int _performanceID;
        private int _totalQty; // 총 생산량`
        public UserControl_WorkPerformance(int userID)
        {
            InitializeComponent();
            _userID = userID;
            LoadWorkPerformanceForm();
        }

        private void LoadWorkPerformanceForm()
        {
            string query = @"SELECT wpf.PerformanceID, wpf.OrderID AS 주문번호, pr.Name AS 제품, p.Name AS 공정, e.Name AS 설비, wpf.RegisteredBy AS 작업자, wpf.GoodQty, wpf.DefectQty, wpf.Reason
                            FROM WorkPerformance wpf
                            JOIN Process p ON p.ProcessID = wpf.ProcessID
                            JOIN Product pr ON pr.ProductID = wpf.ProductID
                            JOIN Equipment e ON e.EquipmentID = wpf.EquipmentID
                            WHERE wpf.RegisteredBy = @UserID;";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", _userID)
            };

            dataGridView1.DataSource = DBHelper.ExecuteDataTable(query, parameters);
            dataGridView1.Columns["Reason"].Visible = false; // Hide the Reason column
            //dataGridView1.Columns["PerformanceID"].Visible = false; // Hide the PerformanceID column

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowAffected = e.RowIndex;
            if (rowAffected >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[rowAffected];
                GQtyTextBox.Text = row.Cells["GoodQty"].Value.ToString();
                //BQtyTextBox.Text = row.Cells["DefectQty"].Value.ToString();
                BadReasonTextBox.Text = row.Cells["Reason"].Value.ToString();
                _performanceID = Convert.ToInt32(row.Cells["PerformanceID"].Value.ToString());
                _totalQty = Convert.ToInt32(row.Cells["GoodQty"].Value) + Convert.ToInt32(row.Cells["DefectQty"].Value);
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            string query = @"
                            UPDATE WorkPerformance
                            Set GoodQty = @GoodQty, DefectQty = @DefectQty, Reason = @Reason, UpdateDate = @UpdateDate
                            Where PerformanceID = @PerformanceID";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@GoodQty", int.Parse(GQtyTextBox.Text)),
                new SqlParameter("@DefectQty", int.Parse(BQtyTextBox.Text)),
                new SqlParameter("@Reason", BadReasonTextBox.Text), //불량이 없으면 안적어도 되는데 어떻게 처리해야 할지 고민 중...
                new SqlParameter("@UpdateDate", DateTime.Now),
                new SqlParameter("@PerformanceID", _performanceID)
            };
            try
            {
                int rowsAffected = DBHelper.ExecuteNonQuery(query, parameters);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("작업 성과가 성공적으로 업데이트되었습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadWorkPerformanceForm(); // Refresh the data grid view
                }
                else
                {
                    MessageBox.Show("작업 성과 업데이트에 실패했습니다.", "실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

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

        private void GQtyTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_totalQty == 0) return;

            if (int.TryParse(GQtyTextBox.Text, out int goodQty) )
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
    }
}
