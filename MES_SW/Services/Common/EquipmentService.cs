using MES_SW.Data;
using MES_SW.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Services.Common
{
    public class EquipmentService
    {
        private readonly EquipmentRepository _equipmentRepository;

        public EquipmentService()
        {
            _equipmentRepository = new EquipmentRepository();
        }

        // 추후에 파일을 나누어도 좋을 듯, 지금은 공통으로 사용 중.

        #region 작업자
        // 공정 흐름에 맞는 설비 자동 부여하기 위한 메서드
        public int GetAvailableEquipmentID(int processId)
        {
            return _equipmentRepository.FindAvailableEquipmentId(processId);
        }

        // 현재 가동 중인 설비 정보를 가져오는 메서드
        public DataTable GetUsingEquipment(int EmployeeID)
        {
            return _equipmentRepository.GetUsingEquipmentInfo(EmployeeID);
        }

        // 설비 결함 등록과 설비 상태 업데이트를 트랜잭션으로 처리하는 메서드
        public bool ReportDefectWithTransaction(int EquipmentId, string DefectType, int ReportedBy, string Description)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {

                    try
                    {
                        int insertedDefect = _equipmentRepository.InsertDefect(EquipmentId, DefectType, DateTime.Now, ReportedBy, Description, conn, tran);

                        int updateEquipment = _equipmentRepository.UpdateEquipmentStatus(EquipmentId, "고장", conn, tran);
                        
                        if (insertedDefect > 0 && updateEquipment > 0)
                        {
                            tran.Commit();
                            return true;
                        }
                        else
                        {
                            tran.Rollback();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw new Exception($"설비 결함 등록 중 오류 발생: {ex.Message}", ex);
                    }
                }
            }
        }
        #endregion


        public DataTable GetEquipmentList()
        {
            return _equipmentRepository.GetEquipmentListFromDB();
        }

        public int InsertNewEquipment(Equipment equipment)
        {
            return _equipmentRepository.InsertEquipmentToDB(equipment);
        }

        public int UpdateEquipment(Equipment equipment)
        {
            return _equipmentRepository.UpdateEquipmentToDB(equipment);
        }

        public int DeleteEquipment(Equipment equipment)
        {
            return _equipmentRepository.DeleteEquipmentFromDB(equipment);
        }


    }
}
