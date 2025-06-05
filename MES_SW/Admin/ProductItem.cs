using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Admin
{
    public class ProductItem
    {
        public string ProductName { get; set; }
        public int ProductID { get; set; }

        public override string ToString()
        {
            return ProductName;
        }
    }
}
