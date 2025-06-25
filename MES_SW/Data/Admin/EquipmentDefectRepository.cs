using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Data.Admin
{
    public class EquipmentDefectRepository
    {
        public DataTable GetEquipmentDefectFromDB()
        {
            string query = @"
                            SELECT ed.DefectID, e.Name AS 설비명 , ed.DefectType AS 결함유형, ed.DefectTime AS 발생일시, u.UserName AS 보고자, ed.Resolved AS '해결 여부', ed.ResolvedTime AS 해결일시, ed.Description
                            FROM EquipmentDefect ed
                            JOIN Equipment e ON e.EquipmentID = ed.EquipmentID
                            JOIN Users u ON u.EmployeeID = ed.ReportedBy
                            ";
            return DBHelper.ExecuteDataTable(query);
        }

        // 설비 점검 중 일 때 설비 상태 업데이트
        public int UpdateEquipmentForInspect(EquipmentDefect equipmentDefect)
        {
            string query = @"
                            UPDATE e
                            SET e.Status = '점검'
                            From Equipment e
                            JOIN EquipmentDefect ed ON ed.EquipmentID = e.EquipmentID
                            WHERE ed.DefectID = @DefectID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DefectID", equipmentDefect.DefectID)
            };
            return DBHelper.ExecuteNonQuery(query, parameters);
        }


        // 아래 두 개 트랜잭션으로 처리
        public int UpdateEquipmentDefectForSolved(EquipmentDefect equipmentDefect, SqlConnection conn, SqlTransaction tran)
        {
            string query = "UPDATE EquipmentDefect SET Resolved = 1, ResolvedTime = @ResolvedTime WHERE DefectID = @DefectID";

            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
            {
                cmd.Parameters.AddWithValue("@ResolvedTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@DefectID", equipmentDefect.DefectID);
                return cmd.ExecuteNonQuery();
            }
        }
        public int UpdateEquipmentStatusSolved(EquipmentDefect equipmentDefect, SqlConnection conn, SqlTransaction tran)
        {
            string query = @"
                            UPDATE e
                            SET e.Status = '가동' 
                            FROM Equipment e
                            JOIN EquipmentDefect ed ON ed.EquipmentID = e.EquipmentID
                            WHERE ed.DefectID = @DefectID";
            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
            {
                cmd.Parameters.AddWithValue("@DefectID", equipmentDefect.DefectID);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
