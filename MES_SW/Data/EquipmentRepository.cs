using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Data
{
    public class EquipmentRepository
    {

        /// <summary>
        /// 공정에 맞는 대기 중인 설비를 찾는 메서드
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 현재 가동 중인 설비 정보를 가져오는 메서드
        /// </summary>
        /// <returns></returns>
        public DataTable GetUsingEquipmentInfo()
        {
            // LastUsedTime 컬럼이 null인 경우 '미사용'으로 표기
            // TODO : 조인 통해서 프로세스 이름 표시
            string query = @"
                            SELECT EquipmentID, Name, Type, Status, ProcessID, ISNULL(CONVERT(VARCHAR, LastUsedTime, 23), '미사용') AS LastUsedTime 
                            FROM Equipment
                            WHERE Status = '가동'
                            ORDER BY ProcessID
                            ";
            //Join Process ON Equipment.ProcessID = Process.ProcessID
            return DBHelper.ExecuteDataTable(query);
        }

        // 설비 결함 등록 메서드
        public int InsertDefect(int EquipmentID, string DefectType, DateTime DefectTime, int ReportedBy, string Description, SqlConnection conn, SqlTransaction tran)
        {
            string query = @"INSERT INTO EquipmentDefect(EquipmentID, DefectType, DefectTime, ReportedBy, Description)
                             VALUES (@EquipmentID, @DefectType, @DefectTime, @ReportedBy, @Description) ";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EquipmentID", EquipmentID),
                new SqlParameter("@DefectType", DefectType),
                new SqlParameter("@DefectTime", DefectTime),
                new SqlParameter("@ReportedBy", ReportedBy),
                new SqlParameter("@Description", Description)
            };
            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
            {
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        // 설비 상태 업데이트 메서드
        public int UpdateEquipmentStatus(int EquipmentID, string Status, SqlConnection conn, SqlTransaction tran)
        {
            string query = @"UPDATE Equipment SET Status = @Status WHERE EquipmentID = @EquipmentID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EquipmentID", EquipmentID),
                new SqlParameter("@Status", Status)
            };
            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
            {
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
