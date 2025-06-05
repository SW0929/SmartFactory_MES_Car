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
using System.Windows.Forms.DataVisualization.Charting;
// 수정 필요************************************************
namespace MES_SW.Admin
{   //관리자와 작업자의 생산 대시보드 현황은 동일.
    public partial class UserControl_Dashboard : UserControl
    {
        public UserControl_Dashboard()
        {
            InitializeComponent();
            LoadDashBoardData(); // 대시보드 데이터 로드
            //CreatePieChart();

        }

        private void LoadDashBoardData()
        {
            string query = "SELECT * FROM WorkOrderProcess";
            dataGridView1.DataSource = DBHelper.ExecuteDataTable(query);
            dataGridView1.Columns["StartTime"].DefaultCellStyle.Format = "yyyy-MM-dd tt h:mm:ss";
            dataGridView1.Columns["EndTime"].DefaultCellStyle.Format = "yyyy-MM-dd tt h:mm:ss";
            dataGridView1.Columns["StartTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["EndTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        /* 
        .Net 8.0에서 Chart 컨트롤을 사용하려면 NuGet 패키지를 설치해야 합니다.
        프로젝트 -> NuGet 패키지 관리 -> WinForms.DataVisualization 찾아서 설치
        (작성자: Kirsan "Windows Forms charting controls for .NET. Ported from System.Windows.Forms.DataVisualization...")
        */
        private void CreatePieChart()
        {
            Chart pieChart = new Chart();
            // 위치와 크기를 조정하려면 다음 코드를 사용하세요.
            //chart.Location = new Point(50, 30); // 위치 조정
            //chart.Size = new Size(400, 300);    // 크기 조정
            pieChart.Dock = DockStyle.Left;
            pieChart.BackColor = SystemColors.Control;


            ChartArea chartArea = new ChartArea();
            pieChart.ChartAreas.Add(chartArea);
            chartArea.BackColor = SystemColors.Control; // 차트 영역 배경색 설정

            /*
            Series는 차트의 데이터 집합을 나타내는 클래스이다.
            Chart.Series에 추가되는 데이터 집합.
            각 Series는 Pie, Line, Bar 등 하나의 차트 타입으로 구성된 데이터 그룹이다.
            
            series.ChartType = SeriesChartType.Column;  // 세로 막대
            series.ChartType = SeriesChartType.Line;    // 선 그래프
            series.ChartType = SeriesChartType.Bar;     // 가로 막대
            series.ChartType = SeriesChartType.Pie;     // 원형
            */

            Series series = new Series
            {
                ChartType = SeriesChartType.Pie
            };
            series.Points.AddXY("진행중", 50);
            series.Points.AddXY("대기중", 30);
            series.Points.AddXY("완료", 20);

            pieChart.Series.Add(series);

            this.Controls.Add(pieChart);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
