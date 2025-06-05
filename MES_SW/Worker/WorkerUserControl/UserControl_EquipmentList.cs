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

namespace MES_SW.Worker.WorkerUserControl
{
    public partial class UserControl_EquipmentList : UserControl
    {
        private string _UserID;
        public UserControl_EquipmentList(string UserID)
        {
            InitializeComponent();
            _UserID = UserID; // 작업자 ID 설정
            LoadEquipment();
        }

        private void LoadEquipment()
        {
            // LastUsedTime 컬럼이 null인 경우 '미사용'으로 표기
            // TODO : 조인 통해서 프로세스 이름 표시
            string query = @"
                            SELECT EquipmentID, Name, Type, Status, ProcessID, ISNULL(CONVERT(VARCHAR, LastUsedTime, 23), '미사용') AS LastUsedTime 
                            FROM Equipment
                            WHERE Status = '가동'
                            ORDER BY ProcessID
                            ";
            //Join Process ON Equipment.ProcessID = Process.ProcessID
            dataGridView1.DataSource = DBHelper.ExecuteDataTable(query);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                EquipmentIDTextBox.Text = row.Cells["EquipmentID"].Value.ToString();
            }
            else
            {
                // 클릭한 셀이 유효하지 않으면 종료
                return;
            }

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string query = @"INSERT INTO EquipmentDefect(EquipmentID, DefectType, DefectTime, ReportedBy, Description)
                             VALUES (@EquipmentID, @DefectType, @DefectTime, @ReportedBy, @Description) ";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EquipmentID", EquipmentIDTextBox.Text),
                new SqlParameter("@DefectType", DefectTypeTextBox.Text),
                new SqlParameter("@DefectTime", DateTime.Now),
                new SqlParameter("@ReportedBy", _UserID),
                new SqlParameter("@Description", DescriptionTextBox.Text)
            };
            try
            {
                int rowsAffected = DBHelper.ExecuteNonQuery(query, parameters);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("장비 결함이 성공적으로 등록되었습니다.");
                    LoadEquipment(); // 장비 목록 새로 고침
                }
                else
                {
                    MessageBox.Show("장비 결함 등록에 실패했습니다.");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("장비 결함 등록 중 오류가 발생했습니다: " + ex.Message);
            }
            string query2 = @"UPDATE Equipment SET Status = '고장' WHERE EquipmentID = @EquipmentID";
            SqlParameter[] parameters2 = new SqlParameter[]
            {
                new SqlParameter("@EquipmentID", EquipmentIDTextBox.Text)
            };
            try
            {
                int rowsAffected2 = DBHelper.ExecuteNonQuery(query2, parameters2);
                if (rowsAffected2 > 0)
                {
                    MessageBox.Show("장비 상태가 '고장'으로 업데이트되었습니다.");
                    LoadEquipment(); // 장비 목록 새로 고침
                }
                else
                {
                    MessageBox.Show("장비 상태 업데이트에 실패했습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("장비 상태 업데이트 중 오류가 발생했습니다: " + ex.Message);
            }

        }

        
        /* 작업자는 설비 결함을 등록만 하고 삭제는 관리자만 할 수 있도록 변경
        private void DeleteButton_Click(object sender, EventArgs e)
        {
           string query = "DELETE FROM EquipmentDefect WHERE EquipmentID = @EquipmentID";
           SqlParameter[] parameters = new SqlParameter[]
           {
               new SqlParameter("@EquipmentID", EquipmentIDTextBox.Text)
           };
           try
           {
               int rowsAffected = DBHelper.ExecuteNonQuery(query, parameters);
               if (rowsAffected > 0)
               {
                   MessageBox.Show("장비 결함이 성공적으로 삭제되었습니다.");
                   LoadEquipment(); // 장비 목록 새로 고침
               }
               else
               {
                   MessageBox.Show("장비 결함 삭제에 실패했습니다.");
               }
           }
           catch (Exception ex)
           {
               MessageBox.Show("장비 결함 삭제 중 오류가 발생했습니다: " + ex.Message);
           }
        }
        */
    }
}
