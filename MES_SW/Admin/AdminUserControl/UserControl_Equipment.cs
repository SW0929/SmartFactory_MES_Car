using MES_SW.Admin.Models;
using MES_SW.Admin.Models.Items;
using MES_SW.Data;
using MES_SW.Models;
using MES_SW.Services.Common;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
// 수정 필요***************************************************
namespace MES_SW.Admin
{
    // TODO : ProcessID DataGridView에 숫자 말고 (프레스, 차체, 도장, 의장, 검사)로 표시되게 하기
    // 지금은 Label 을 통해서 VIsible = false로 처리했지만, 추후 수정 필요함
    public partial class UserControl_Equipment : UserControl
    {
        private Equipment equipment;
        private EquipmentService _equipmentService;
        private ProcessService _processService;
        public UserControl_Equipment()
        {
            InitializeComponent();
            _equipmentService = new EquipmentService();
            _processService = new ProcessService();
            equipment = new Equipment();
        }

        #region Load_Methods

        private void UserControl_Equipment_Load(object sender, EventArgs e)
        {
            LoadEquipment();
            LoadProcess();
        }

        private void LoadEquipment()
        {
            dataGridView1.DataSource = _equipmentService.GetEquipmentList();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.Columns["LastUsedTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        // ProcessIDComboBox에 프로세스 목록을 로드하는 메서드
        private void LoadProcess()
        {
            var processList = _processService.GetProcessList();

            ProcessIDComboBox.DataSource = processList;
            ProcessIDComboBox.DisplayMember = "ProcessName";
            ProcessIDComboBox.ValueMember = "ProcessValue";

            if (ProcessIDComboBox.Items.Count > 0)
                ProcessIDComboBox.SelectedIndex = 0; // 기본 선택값 설정
        }
        #endregion


        #region Event_Handlers
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                SetEquipmentFieldsFromRow(row);
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터를 불러오는 중 오류 발생: " + ex.Message);
            }
            
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (CheckAllText()) return;

            equipment = GetEquipmentFromInput();
            try
            {
                int result = _equipmentService.InsertNewEquipment(equipment);
                if (result > 0)
                {
                    MessageBox.Show("설비 등록 완료");
                    LoadEquipment(); // 데이터 갱신
                }
                else
                {
                    MessageBox.Show("설비 등록 실패");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("데이터베이스 오류: " + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
                return;
            }

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (CheckAllText()) return;
            equipment = GetEquipmentFromInput();
            try
            {
                int rowsAffected = _equipmentService.UpdateEquipment(equipment);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("수정이 완료되었습니다.");
                    LoadEquipment();
                }
                else
                {
                    MessageBox.Show("");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            // EquipmentID 어떻게 처리해야 할지 해야 함.
            // TODO : EquipmentID를 사용하여 삭제 쿼리 작성
            if (CheckAllText()) return;
            var equipment = GetEquipmentFromInput();
            try
            {
                int rowsAffected = _equipmentService.DeleteEquipment(equipment);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("삭제가 완료되었습니다.");
                    LoadEquipment();
                }
                else
                {
                    MessageBox.Show("삭제 실패");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB 오류 : " + ex.Message);
            }
        }
        // 빈 칸을 클릭했을 때 입력 필드를 초기화하는 이벤트 핸들러
        private void UserControl_Equipment_Click(object sender, EventArgs e)
        {
            EquipmentNameTextBox.Clear();
            EquipmentTypeTextBox.Clear();
            EquipmentStatusTextBox.Clear();
            //LastUsedTime.Text = "미사용"; // 초기화 시 "미사용"으로 설정
        }

        #endregion
        private Equipment GetEquipmentFromInput()
        {
            return new Equipment
            {
                EquipmentID = Convert.ToInt32(EquipmentIDLabel.Text),
                Name = EquipmentNameTextBox.Text,
                Type = EquipmentTypeTextBox.Text,
                Status = EquipmentStatusTextBox.Text,
                ProcessID = Convert.ToInt32(ProcessIDComboBox.SelectedValue)
            };
        }

        private bool CheckAllText()
        {
            if (EquipmentNameTextBox.Text == "" || EquipmentTypeTextBox.Text == "" || EquipmentStatusTextBox.Text == "")
            {
                // ProcessIDComboBox 는 기본 값을 주었기 때문에 처리하지 않음.
                MessageBox.Show("모든 필드를 입력해주세요.");
                return true;
            }
            else
            {
                return false;
            }
        }
        private void SetEquipmentFieldsFromRow(DataGridViewRow row)
        {

            EquipmentIDLabel.Text = row.Cells["EquipmentID"].Value.ToString();
            EquipmentNameTextBox.Text = row.Cells["Name"].Value.ToString();
            EquipmentTypeTextBox.Text = row.Cells["Type"].Value.ToString();
            EquipmentStatusTextBox.Text = row.Cells["Status"].Value.ToString();

            if (int.TryParse(row.Cells["ProcessID"].Value?.ToString(), out int ProcessID))
            {
                for (int i = 0; i <= ProcessIDComboBox.Items.Count; i++)
                {
                    if (ProcessIDComboBox.Items[i] is ProcessItem item && item.ProcessValue == ProcessID)
                    {
                        ProcessIDComboBox.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                ProcessIDComboBox.SelectedIndex = -1;
            }
        }

        

    }

}
