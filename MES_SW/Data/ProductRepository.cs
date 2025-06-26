using MES_SW.Admin.Models.Items;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_SW.Data
{
    public class ProductRepository
    {
        public List<ProductItem> GetAllProduct()
        {
            List<ProductItem> products = new List<ProductItem>();
            string query = "SELECT ProductID, Name FROM Product;";

            using (SqlConnection conn = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new ProductItem
                        {
                            ProductID = reader.GetInt32(0),
                            ProductName = reader.GetString(1)
                        });
                    }
                }
            }

            return products;
        }
    }
}
