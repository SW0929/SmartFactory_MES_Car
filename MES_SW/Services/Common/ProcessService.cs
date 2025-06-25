using MES_SW.Admin.Models.Items;
using MES_SW.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Services.Common
{
    public class ProcessService
    {
        private readonly ProcessRepository _repository;

        public ProcessService()
        {
            _repository = new ProcessRepository();
        }

        public List<ProcessItem> GetProcessList()
        {
            return _repository.GetAllProcess();
        }
    }
}
