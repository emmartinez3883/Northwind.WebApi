using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Northwind.WebApi.Models;

namespace Northwind.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {

        [HttpGet]
        [Route("Product/GetProductCount")]
        public int GetProductCount() {
            var productCount = 0;

            using (var northwindContext = new Northwind())
            {
                productCount = northwindContext.Products.Count();
            }

                return productCount;
            }

        [HttpGet]
        [Route("Product/GetProducts")]
        public List<ProductDTO> GetProducts()
        {
            var products = new List<ProductDTO>();

            using (var northwindContext = new Northwind())
            {
                products = (from table in northwindContext.Products
                            select new ProductDTO
                            {
                                ProductID = table.ProductID,
                                ProductName = table.ProductName,
                                UnitPrice = (decimal)table.UnitPrice,
                                CategoryID = (int)table.CategoryID
                            }).ToList();
            }

            return products;
        }

        [HttpGet]
        [Route("Product/GetProductsByCategoryID/{categoryID}")]
        public List<ProductDTO> GetProductsByCategoryID(int categoryID)
        {
            var products = new List<ProductDTO>();

            using (var northwindContext = new Northwind())
            {
                products = (from table in northwindContext.Products
                            where table.CategoryID == categoryID
                            select new ProductDTO
                            {
                                ProductID = table.ProductID,
                                ProductName = table.ProductName,
                                UnitPrice = (decimal)table.UnitPrice,
                                CategoryID = (int)table.CategoryID
                            }).ToList();
            }

            return products;
        }

        [HttpGet]
        [Route("Product/GetProductByProductID/{productID}")]
        public ProductDTO GetProductByProductID(int productID)
        {
            var product = new ProductDTO();

            using (var northwindContext = new Northwind())
            {
                product = (from table in northwindContext.Products
                            where table.ProductID == productID
                            select new ProductDTO
                            {
                                ProductID = table.ProductID,
                                ProductName = table.ProductName,
                                UnitPrice = (decimal)table.UnitPrice,
                                CategoryID = (int)table.CategoryID
                            }).FirstOrDefault();
            }

            return product;
        }

        [HttpPost]
        [Route("Product/CreateProduct")]
        public int CreateProduct(ProductDTO product)
        {
            var rowsAffected = 0;

            using (var northwindContext = new Northwind())
            {
                Product entity = new Product();

                entity.ProductName = product.ProductName;
                entity.UnitPrice = product.UnitPrice;
                entity.CategoryID = product.CategoryID;

                northwindContext.Products.Add(entity);
                rowsAffected = northwindContext.SaveChanges();
            }

            return rowsAffected;
        }

        [HttpPost]
        [Route("Product/UpdateProduct")]
        public int UpdateProduct(ProductDTO product)
        {
            var rowsAffected = 0;

            using (var northwindContext = new Northwind())
            {
                Product entity = northwindContext.Products
                                 .Where(p => p.ProductID == product.ProductID)
                                 .FirstOrDefault();

                entity.ProductName = product.ProductName;
                entity.UnitPrice = product.UnitPrice;
                entity.CategoryID = product.CategoryID;
                rowsAffected = northwindContext.SaveChanges();
            }

            return rowsAffected;
        }
    }
}
