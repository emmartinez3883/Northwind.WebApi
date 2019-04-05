using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Northwind.WebApi.Models
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int CategoryID { get; set; }
    }
}