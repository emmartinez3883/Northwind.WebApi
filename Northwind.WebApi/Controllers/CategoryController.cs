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
    public class CategoryController : ApiController
    {
        [HttpGet]
        [Route("Category/GetCategories")]
        public List<CategoryDTO> GetCategories()
        {
            var categories = new List<CategoryDTO>();

            using (var northwindContext = new Northwind())
            {
                categories = (from table in northwindContext.Categories
                            select new CategoryDTO
                            {
                                CategoryID = table.CategoryID,
                                CategoryName = table.CategoryName
                            }).ToList();
            }

            return categories;
        }
    }
}
