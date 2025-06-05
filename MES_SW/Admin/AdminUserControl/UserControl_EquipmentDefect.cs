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

        private void LoadEquipmentDefeectData()
        {
            string query = @"
                            SELECT e.Name AS 설비명 , ed.DefectType AS 결함유형, ed.DefectTime AS 발생일시, ed.ReportedBy AS 보고자, ed.Resolved AS '해결 여부', ed.ResolvedTime AS 해결일시, ed.Description
                            FROM EquipmentDefect ed
                            JOIN Equipment e ON e.EquipmentID = ed.EquipmentID
                            ";

            dataGridView1.DataSource = DBHelper.ExecuteDataTable(query);
            // 이거 안하면 날짜가 짤려서 나오고 초 단위 까지 안나옴
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
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
                ResolvedCheckBox.Checked = Convert.ToBoolean(row.Cells["해결 여부"].Value);
                string description = row.Cells["Description"].Value?.ToString() ?? string.Empty;
                EquipmentDefectDescriptionTextBox.Text = description;
            }
            else
            {
                // 클릭한 셀이 유효하지 않으면 종료
                return;
            }
        }
        

    }
}
