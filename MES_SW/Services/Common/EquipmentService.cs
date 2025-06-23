using MES_SW.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
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
        // 공정 흐름에 맞는 설비 자동 부여하기 위한 메서드
        public int GetAvailableEquipmentID(int processId)
        {
            return _equipmentRepository.FindAvailableEquipmentId(processId);
        }
    }
}
