using MES_SW.Admin.Models.Items;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Data
{
    public class ProcessRepository
    {
        public List<ProcessItem> GetAllProcess()
        {
            List<ProcessItem> processes = new List<ProcessItem>();
            string query = "SELECT ProcessID, Name FROM Process ORDER BY Sequence";

            using (SqlConnection conn = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        processes.Add(new ProcessItem
                        {
                            ProcessValue = reader.GetInt32(0),
                            ProcessName = reader.GetString(1)
                        });
                    }
                }
            }

            return processes;
        }
    }
}
