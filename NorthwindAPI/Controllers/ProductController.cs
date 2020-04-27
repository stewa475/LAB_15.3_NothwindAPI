using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NorthwindAPI.Models;

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private string connectionString;

        public ProductController(IConfiguration config)
        {
            connectionString = config.GetConnectionString("default");
        }

        [HttpGet]
        public IEnumerable<Products> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string command = "EXEC AllProducts";

            return conn.Query<Products>(command);
        }

        [HttpGet("ProductDetail/{id}")]
        public IEnumerable<Products> GetProductDetails(int id)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            SqlConnection conn = new SqlConnection(connectionString);
            string command = "EXEC ProductDetail2 @OrderId";

            return conn.Query<Products>(command, new { OrderId = id });
        }

        [HttpGet("ProductByName/{id}")]
        public IEnumerable<Products> GetProductByName(string id)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            SqlConnection conn = new SqlConnection(connectionString);
            string command = "EXEC ProductbyName2 @ProductName";

            return conn.Query<Products>(command, new { ProductName = id });
        }

    }
}