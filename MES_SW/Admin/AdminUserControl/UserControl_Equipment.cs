using MES_SW.Data;
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
// 수정 필요***************************************************
namespace MES_SW.Admin
{
    // TODO : ProcessID DataGridView에 숫자 말고 (프레스, 차체, 도장, 의장, 검사)로 표시되게 하기
    // 지금은 Label 을 통해서 VIsible = false로 처리했지만, 추후 수정 필요함
    public partial class UserControl_Equipment : UserControl
    {
        public UserControl_Equipment()
        {

            InitializeComponent();
        }

        #region Load_Methods

        private void UserControl_Equipment_Load(object sender, EventArgs e)
        {
            LoadEquipment();
            LoadProcess();
        }

        private void LoadEquipment()
        {
            // LastUsedTime 컬럼이 null인 경우 '미사용'으로 표기
            // TODO : 조인 통해서 프로세스 이름 표시
            string query = @"
                            SELECT EquipmentID, Name, Type, Status, ProcessID, ISNULL(CONVERT(NVARCHAR, LastUsedTime, 120), '미사용') AS LastUsedTime 
                            FROM Equipment
                            ORDER BY ProcessID
                            ";
            //Join Process ON Equipment.ProcessID = Process.ProcessID
            dataGridView1.DataSource = DBHelper.ExecuteDataTable(query);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.Columns["LastUsedTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


        }

        // ProcessIDComboBox에 프로세스 목록을 로드하는 메서드
        private void LoadProcess()
        {
            ProcessIDComboBox.Items.Clear(); // 기존 아이템 초기화

            string query = "SELECT ProcessID, Name FROM Process ORDER BY Sequence";

            using (SqlConnection conn = DBHelper.GetConnection()) // DBHelper로 커넥션 획득
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProcessItem item = new ProcessItem()
                        {
                            ProcessValue = reader.GetInt32(0),
                            ProcessName = reader.GetString(1)
                        };

                        ProcessIDComboBox.Items.Add(item);
                    }
                }
            }

            if (ProcessIDComboBox.Items.Count > 0)
                ProcessIDComboBox.SelectedIndex = 0; // 기본 선택값 설정
        }
        #endregion


        #region Event_Handlers
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                EquipmentIDLabel.Text = row.Cells["EquipmentID"].Value.ToString();
                EquipmentNameTextBox.Text = row.Cells["Name"].Value.ToString();
                EquipmentTypeTextBox.Text = row.Cells["Type"].Value.ToString();
                EquipmentStatusTextBox.Text = row.Cells["Status"].Value.ToString();
                //var selectedItem = (ProcessItem)ProcessIDComboBox.SelectedItem;
                //int selectedProcessID = selectedItem.ProcessValue;
                switch (row.Cells["ProcessID"].Value.ToString())
                {
                    case "1":
                        ProcessIDComboBox.SelectedIndex = 0; // 프레스
                        break;
                    case "2":
                        ProcessIDComboBox.SelectedIndex = 1; // 차체
                        break;
                    case "3":
                        ProcessIDComboBox.SelectedIndex = 2; // 도장
                        break;
                    case "4":
                        ProcessIDComboBox.SelectedIndex = 3; // 의장
                        break;
                    case "5":
                        ProcessIDComboBox.SelectedIndex = 4; // 검사
                        break;
                    default:
                        ProcessIDComboBox.SelectedIndex = -1; // 선택 해제
                        break;
                }
                //ProcessIDComboBox.SelectedItem = row.Cells["ProcessID"].Value; // ProcessID를 ComboBox에 설정
                /*
                object lastUsedTimeValue = row.Cells["LastUsedTime"].Value;
                // LastUsedTime이 null이거나 비어있으면 "미사용"으로 표시
                LastUsedTime.Text = lastUsedTimeValue == null || string.IsNullOrWhiteSpace(lastUsedTimeValue.ToString()) ? "미사용" : lastUsedTimeValue.ToString();
                */
            }
            else
            {
                // 클릭한 셀이 유효하지 않으면 종료
                return;
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

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (EquipmentNameTextBox.Text == "" || EquipmentTypeTextBox.Text == "" || EquipmentStatusTextBox.Text == "")
            {
                // ProcessIDComboBox 는 기본 값을 주었기 때문에 처리하지 않음.
                MessageBox.Show("모든 필드를 입력해주세요.");
                return;
            }
            string query = "INSERT INTO Equipment (Name, Type, Status, ProcessID) Values(@Name, @Type, @Status, @ProcessID)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter ("@Name", EquipmentNameTextBox.Text),
                new SqlParameter ("@Type", EquipmentTypeTextBox.Text),
                new SqlParameter ("@Status", EquipmentStatusTextBox.Text),
                new SqlParameter ("@ProcessID", ((ProcessItem)ProcessIDComboBox.SelectedItem).ProcessValue)
            };
            try
            {
                int rowsAffected = DBHelper.ExecuteNonQuery(query, parameters);
                if (rowsAffected > 0)
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
            string query = "UPDATE Equipment Set Name = @Name, Type = @Type, Status = @Status, ProcessID = @ProcessId WHERE EquipmentID = @EquipmentID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EquipmentID", EquipmentIDLabel.Text),
                new SqlParameter("@Name", EquipmentNameTextBox.Text),
                new SqlParameter("@Type", EquipmentTypeTextBox.Text),
                new SqlParameter("@Status", EquipmentStatusTextBox.Text),
                new SqlParameter("@ProcessId", ((ProcessItem)ProcessIDComboBox.SelectedItem).ProcessValue)
            };

            try
            {
                int rowsAffected = DBHelper.ExecuteNonQuery(query, parameters);
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
            // TODO : EquipmentID를 사용하여 삭제 쿼리 작성
            string query = "DELETE FROM Equipment WHERE EquipmentID = @EquipmentID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EquipmentID", int.Parse(EquipmentIDLabel.Text))
            };
            try
            {
                int rowsAffected = DBHelper.ExecuteNonQuery(query, parameters);
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
        #endregion
        
    }
}
