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
    public class CustomerController : ControllerBase
    {
        private string connectionString;

        public CustomerController(IConfiguration config)
        {
            connectionString = config.GetConnectionString("default");
        }

        [HttpGet]
        public IEnumerable<Customers> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string command = "EXEC AllCustomers";

            return conn.Query<Customers>(command);
        }

        [HttpGet("Country/{id}")]
        public IEnumerable<Customers> GetAllByCountry(string id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string command = "EXEC CustomerByCountry @Country";

            return conn.Query<Customers>(command, new { Country = id });
        }

        [HttpGet("Customer/{id}")]
        public IEnumerable<Customers> GetAllByCustomerID(string id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string command = "EXEC CustomerByID @CustomerID";

            return conn.Query<Customers>(command, new { CustomerID = id });
        }

        [HttpPost]
        public Object Post(Customers c)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string queryString = "INSERT INTO Customers (CustomerID, CompanyName, ContactName, Country)";
            queryString += " VALUES (@CustomerID, @CompanyName, @ContactName, @Country);";
            queryString += " SELECT SCOPE_IDENTITY();";

            connection.ExecuteScalar<int>(queryString, c);

            return new { success = true };

        }
    }
}