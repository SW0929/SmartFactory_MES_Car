using MES_SW.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
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


        }

        private void UserControl_Dashboard_Load(object sender, EventArgs e)
        {
            LoadDashBoardData(); // 대시보드 데이터 로드
            LoadPerformanceChart();
            //CreatePieChart();
        }

        private void LoadDashBoardData()
        {
            // TODO : ProcessID 를 ProcessName로 변경
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

        private void LoadPerformanceChart()
        {

            

            
            string query = @"
                            SELECT 
                                p.ProcessID,
                                p.Name AS ProcessName,
                                wop.Status,
                                COUNT(*) AS Count
                            FROM Process p
                            LEFT JOIN WorkOrderProcess wop ON wop.ProcessID = p.ProcessID
                            GROUP BY p.ProcessID, p.Name, wop.Status
                            ORDER BY p.ProcessID, wop.Status;";

            Dictionary<string, Dictionary<string, int>> chartData = new();

            using (SqlConnection conn = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string processName = reader["ProcessName"].ToString();
                        string status = reader["Status"]?.ToString() ?? "없음";
                        int count = Convert.ToInt32(reader["Count"]);

                        if (!chartData.ContainsKey(processName))
                            chartData[processName] = new Dictionary<string, int>();

                        chartData[processName][status] = count;
                    }
                }
            }

            // panelCharts는 FlowLayoutPanel이라고 가정
            flowLayoutPanel1.Controls.Clear();

            foreach (var processEntry in chartData)
            {
                string processName = processEntry.Key;
                var statusDict = processEntry.Value;

                Chart chart = new Chart();
                chart.Size = new Size(400, 200);
                chart.ChartAreas.Add(new ChartArea("Area"));
                chart.Titles.Add(processName);

                Series series = new Series("상태별 수량")
                {
                    ChartType = SeriesChartType.Column
                };

                foreach (var statusEntry in statusDict)
                {
                    series.Points.AddXY(statusEntry.Key, statusEntry.Value);
                }

                chart.Series.Add(series);
                chart.Legends.Add(new Legend("Legend"));
                flowLayoutPanel1.Controls.Add(chart);
            }
            
            /*
            string query = @"
                            SELECT 
                                p.ProcessID,
                                p.Name AS ProcessName,
                                ISNULL(SUM(wp.GoodQty), 0) AS TotalGood,
                                ISNULL(SUM(wp.DefectQty), 0) AS TotalDefect
                            FROM Process p
                            LEFT JOIN WorkPerformance wp ON wp.ProcessID = p.ProcessID
                            GROUP BY p.ProcessID, p.Name
                            ORDER BY p.ProcessID;";

            using (SqlConnection conn = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string processName = reader["ProcessName"].ToString();
                        int goodQty = Convert.ToInt32(reader["TotalGood"]);
                        int defectQty = Convert.ToInt32(reader["TotalDefect"]);

                        Chart chart = new Chart();
                        chart.Size = new Size(400, 200); // 차트 크기 조정
                        chart.ChartAreas.Add(new ChartArea("MainArea"));

                        Series goodSeries = new Series("양품 수량")
                        {
                            ChartType = SeriesChartType.Column,
                            Color = Color.Green
                        };

                        Series defectSeries = new Series("불량 수량")
                        {
                            ChartType = SeriesChartType.Column,
                            Color = Color.Red
                        };

                        goodSeries.Points.AddXY("양품", goodQty);
                        defectSeries.Points.AddXY("불량", defectQty);

                        chart.Series.Add(goodSeries);
                        chart.Series.Add(defectSeries);
                        chart.Titles.Add(processName);
                        chart.Legends.Add(new Legend("Legend"));

                        flowLayoutPanel1.Controls.Add(chart); // FlowLayoutPanel에 추가
                    }
                }
            }*/

        }
    }
}
