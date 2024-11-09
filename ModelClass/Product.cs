using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShopManagement.ModelClass
{
    public class Product
    {
        public int productId {  get; set; }
        public string productName { get; set; }
        public string size {  get; set; }
        public float price {  get; set; }
        public string material { get; set; }
        public int quantity {  get; set; }
        public int brandId {  get; set; }
        public string brandName { get; set; }
    }
}
