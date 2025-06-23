using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Data
{
    public class EquipmentRepository
    {

        public int FindAvailableEquipmentId(int processId)
        {
            int equipmentId = 0;
            string query = @"
                            SELECT TOP 1 EquipmentID
                            FROM Equipment
                            WHERE ProcessID = @ProcessID AND Status = '대기'
                            "; // 혹은 다른 기준

            using (SqlConnection conn = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ProcessID", processId);
                conn.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                    equipmentId = Convert.ToInt32(result);
            }

            return equipmentId; // 없으면 0
        }
    }
}
