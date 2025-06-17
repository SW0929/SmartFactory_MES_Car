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
    public partial class UserControl_WorkPerformance : UserControl
    {
        private int _userID;
        public UserControl_WorkPerformance(int userID)
        {
            InitializeComponent();
            LoadWorkPerformanceForm();
        }

        private void LoadWorkPerformanceForm()
        {
            string query = @"SELECT *
                            FROM WorkPerformance;";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", _userID)
            };

            dataGridView1.DataSource = DBHelper.ExecuteDataTable(query, parameters);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
