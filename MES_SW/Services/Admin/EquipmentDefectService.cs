using MES_SW.Data;
using MES_SW.Data.Admin;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Services.Admin
{
    public class EquipmentDefectService
    {
        private readonly EquipmentDefectRepository _equipmentDefectRepository;

        public EquipmentDefectService()
        {
            _equipmentDefectRepository = new EquipmentDefectRepository();
        }

        public DataTable GetEquipmentDefect()
        {
            return _equipmentDefectRepository.GetEquipmentDefectFromDB();
        }

        public int UpdateEquipmentInspect(EquipmentDefect equipmentDefect)
        {
            return _equipmentDefectRepository.UpdateEquipmentForInspect(equipmentDefect);
        }

        public bool TranDefectSolved(EquipmentDefect equipmentDefect)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int first = _equipmentDefectRepository.UpdateEquipmentDefectForSolved(equipmentDefect, conn, tran);

                        int second = _equipmentDefectRepository.UpdateEquipmentStatusSolved(equipmentDefect, conn, tran);

                        if (first > 0 && second > 0)
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
                        throw new Exception($"설비 결함 해결 실패: {ex.Message}", ex);
                    }
                }
            }
        }
    }
}
