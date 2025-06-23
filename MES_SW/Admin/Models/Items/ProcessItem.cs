using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Admin.Models.Items
{
    
    public class ProcessItem
    {
        public int ProcessValue { get; set; }
        public string ProcessName { get; set; }

        public override string ToString()
        {
            return ProcessName;
        }
    }
}
