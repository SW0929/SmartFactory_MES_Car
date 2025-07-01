using MES_SW.Data;
using MES_SW.Services.Admin;
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
    //TODO : 조치 상태 변경 해야함.
    public partial class UserControl_EquipmentDefect : UserControl
    {
        private EquipmentDefect _equipmentDefect;
        private EquipmentDefectService _equipmentDefectService;
        public UserControl_EquipmentDefect()
        {
            InitializeComponent();
            _equipmentDefect = new EquipmentDefect();
            _equipmentDefectService = new EquipmentDefectService();
            LoadEquipmentDefeectData();

        }
        #region Load_Methods
        // 현재 등록된 결함이 발생한 설비 목록
        private void LoadEquipmentDefeectData()
        {
            dataGridView1.DataSource = _equipmentDefectService.GetEquipmentDefect();

            // 이거 안하면 날짜가 짤려서 나오고 초 단위 까지 안나옴
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            
            dataGridView1.Columns["발생일시"].DefaultCellStyle.Format = "yyyy-MM-dd tt h:mm:ss";
            dataGridView1.Columns["해결일시"].DefaultCellStyle.Format = "yyyy-MM-dd tt h:mm:ss";
            
            dataGridView1.Columns["발생일시"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["해결일시"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.Columns["DefectID"].Visible = false;
            dataGridView1.Columns["Description"].Visible = false;
        }

        /// <summary>
        /// 각 셀을 클릭하면 설비 결함 정보가 모든 textbox에 입려된다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            try
            {
                if (e.RowIndex < 0) return;
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                SetDefectFormFieldsFromRow(row);
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터를 불러오는 중 오류 발생: " + ex.Message);
            }
        }
        #endregion

        #region Event_Handlers
        /// <summary>
        /// 선택한 설비 '점검' 상태로 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InspectButton_Click(object sender, EventArgs e)
        {
            _equipmentDefect = GetEquipmentFromInput();
            try
            {
                int rowsAffected = _equipmentDefectService.UpdateEquipmentInspect(_equipmentDefect);
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


        /// <summary>
        /// 해결 완료된 설비 상태 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SolvedButton_Click(object sender, EventArgs e)
        {
            _equipmentDefect = GetEquipmentFromInput();
            try
            {
                bool solvedCheck = _equipmentDefectService.TranDefectSolved(_equipmentDefect);

                if (solvedCheck)
                {
                    MessageBox.Show("결함이 해결되었습니다.\n설비가 다시 가동합니다.");
                    LoadEquipmentDefeectData(); // 갱신까지
                }
                else
                {
                    MessageBox.Show("결함 또는 설비 상태 업데이트에 실패했습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"처리 중 오류 발생: {ex.Message}");
                // 필요시 로그로 남기기
            }
        }

        #endregion

        // 사용자가 입력한 설비결함 Model 의 값 들을 받아온다.
        private EquipmentDefect GetEquipmentFromInput()
        {

            return new EquipmentDefect
            {
                // 지금 실질적으로 사용하는 변수는 DefectID 밖에 사용하지 않음.
                DefectID = Convert.ToInt32(EquipmentIDLabel.Text),
                EquipmentName = EquipmentNameTextBox.Text,
                DefectTime = DefectDateTimePicker.Value,
                DefectType = EquipmentTypeTextBox.Text,
                Description = EquipmentDefectDescriptionTextBox.Text
            };
        }

        // 셀에서 선택된 설비 결함 정보를 각 데이터에 넣는다.
        private void SetDefectFormFieldsFromRow(DataGridViewRow row)
        {

            EquipmentIDLabel.Text = row.Cells["DefectID"].Value.ToString();
            EquipmentNameTextBox.Text = row.Cells["설비명"].Value?.ToString() ?? string.Empty;
            EquipmentTypeTextBox.Text = row.Cells["결함유형"].Value?.ToString() ?? string.Empty;
            DefectDateTimePicker.Value = Convert.ToDateTime(row.Cells["발생일시"].Value);
            EquipmentDefectDescriptionTextBox.Text = row.Cells["Description"].Value?.ToString() ?? string.Empty;
        }
    }
}
