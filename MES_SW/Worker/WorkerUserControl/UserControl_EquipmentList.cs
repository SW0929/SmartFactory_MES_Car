using MES_SW.Data;
using MES_SW.Services.Common;
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
        private EquipmentService _equipmentService;
        private int _UserID;
        public UserControl_EquipmentList(int UserID)
        {
            InitializeComponent();
            _UserID = UserID; // 작업자 ID 설정
            _equipmentService = new EquipmentService();
            LoadEquipment();
        }

        private void LoadEquipment()
        {
            dataGridView1.DataSource = _equipmentService.GetUsingEquipment(_UserID);
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

        // 설비 결함 등록 및 설비 상태 업데이트
        private void AddButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(EquipmentIDTextBox.Text, out int equipmentID))
            {
                try
                {
                    bool success = _equipmentService.ReportDefectWithTransaction(
                        equipmentID,
                        DefectTypeTextBox.Text,
                        _UserID,
                        DescriptionTextBox.Text
                    );

                    if (success)
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
                    // Service 계층에서 발생한 예외 메시지를 사용자에게 표시
                    MessageBox.Show("오류 발생: " + ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("유효한 장비 ID를 입력하세요.");
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
