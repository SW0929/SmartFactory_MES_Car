using MES_SW.Admin.Models.Items;
using MES_SW.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Services.Common
{
    public class ProductService
    {
        private ProductRepository _repository;
        public ProductService()
        {
            _repository = new ProductRepository();
        }

        public List<ProductItem> GetProducts()
        {
            return _repository.GetAllProduct();
        }
    }
}
