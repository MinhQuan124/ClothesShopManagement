using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShopManagement.ModelClass
{
    public class AddProduct
    {
        public  int WarehouseProduct_Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }

        public float Price { get; set; }
        public int Quanity { get; set; }
        public decimal Total
        {
            get
            {
                return (decimal)Price * Quanity;
            }
        }
    }
}
