using MES_SW.DB;
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

namespace MES_SW.Admin
{
    //TODO : 설비 결함 사유 연동 못함 해결 방법 생각하기. - 해결 완료
    //TODO : 조치 상태 변경 해야함.
    public partial class UserControl_EquipmentDefect : UserControl
    {
        public UserControl_EquipmentDefect()
        {
            InitializeComponent();
            LoadEquipmentDefeectData();

        }
        #region Load_Methods
        private void LoadEquipmentDefeectData()
        {
            string query = @"
                            SELECT ed.DefectID, e.Name AS 설비명 , ed.DefectType AS 결함유형, ed.DefectTime AS 발생일시, ed.ReportedBy AS 보고자, ed.Resolved AS '해결 여부', ed.ResolvedTime AS 해결일시, ed.Description
                            FROM EquipmentDefect ed
                            JOIN Equipment e ON e.EquipmentID = ed.EquipmentID
                            ";

            dataGridView1.DataSource = DBHelper.ExecuteDataTable(query);
            // 이거 안하면 날짜가 짤려서 나오고 초 단위 까지 안나옴
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            dataGridView1.Columns["DefectID"].Visible = false;
            dataGridView1.Columns["Description"].Visible = false;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                EquipmentNameTextBox.Text = row.Cells["설비명"].Value.ToString();
                EquipmentTypeTextBox.Text = row.Cells["결함유형"].Value.ToString();
                DefectDateTimePicker.Value = Convert.ToDateTime(row.Cells["발생일시"].Value);
                //ResolvedCheckBox.Checked = Convert.ToBoolean(row.Cells["해결 여부"].Value);
                EquipmentIDLabel.Text = row.Cells["DefectID"].Value.ToString();
                string description = row.Cells["Description"].Value?.ToString() ?? string.Empty;
                EquipmentDefectDescriptionTextBox.Text = description;
            }
            else
            {
                // 클릭한 셀이 유효하지 않으면 종료
                return;
            }
        }
        #endregion

        #region Event_Handlers
        private void MaintenanceButton_Click(object sender, EventArgs e)
        {
            string query = @"
                            UPDATE e
                            SET e.Status = '점검'
                            From Equipment e
                            JOIN EquipmentDefect ed ON ed.EquipmentID = e.EquipmentID
                            WHERE ed.DefectID = @DefectID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DefectID", int.Parse(EquipmentIDLabel.Text))
            };
            try
            {
                int rowsAffected = DBHelper.ExecuteNonQuery(query, parameters);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("설비 상태가 '점검'으로 변경되었습니다.");
                    LoadEquipmentDefeectData(); // 데이터 새로고침
                }
                else
                {
                    MessageBox.Show("설비 상태 업데이트에 실패했습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}");
            }
        }

        private void SolvedButton_Click(object sender, EventArgs e)
        {
            string query = "UPDATE EquipmentDefect SET Resolved = 1, ResolvedTime = @ResolvedTime WHERE DefectID = @DefectID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ResolvedTime", DateTime.Now),
                new SqlParameter("@DefectID", int.Parse(EquipmentIDLabel.Text))
            };

            try
            {
                int rowsAffected = DBHelper.ExecuteNonQuery(query, parameters);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("결함이 해결되었습니다.");
                    LoadEquipmentDefeectData(); // 데이터 새로고침
                }
                else
                {
                    MessageBox.Show("해결 상태 업데이트에 실패했습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}");
            }

            string query2 = @"
                            UPDATE e
                            SET e.Status = '대기' 
                            FROM Equipment e
                            JOIN EquipmentDefect ed ON ed.EquipmentID = e.EquipmentID
                            WHERE ed.DefectID = @DefectID";
            SqlParameter[] parameters2 = new SqlParameter[]
            {
                new SqlParameter("@DefectID", int.Parse(EquipmentIDLabel.Text))
            };

            try
            {
                int rowsAffected2 = DBHelper.ExecuteNonQuery(query2, parameters2);
                if (rowsAffected2 > 0)
                {
                    MessageBox.Show("설비 상태가 '대기'로 변경되었습니다.");
                }
                else
                {
                    MessageBox.Show("설비 상태 업데이트에 실패했습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}");
            }
        }

        #endregion

    }
}
