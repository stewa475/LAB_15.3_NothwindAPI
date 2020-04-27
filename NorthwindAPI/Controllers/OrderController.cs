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
    public class OrderController : ControllerBase
    {
        private string connectionString;

        public OrderController(IConfiguration config)
        {
            connectionString = config.GetConnectionString("default");
        }

        [HttpGet]
        public IEnumerable<Orders> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string command = "EXEC AllOrders";

            return conn.Query<Orders>(command);
        }

        [HttpGet("CustomerID/{id}")]
        public IEnumerable<Orders> GetOrderByCustomerID(string id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string command = "EXEC OrderDetail @CustomerID";

            return conn.Query<Orders>(command, new { CustomerID = id });
        }

        [HttpGet("OrderID/{id}")]
        public IEnumerable<Orders> GetOrderByOrderrID(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string command = "EXEC OrderByID @OrderID";

            return conn.Query<Orders>(command, new { OrderID = id });
        }

        [HttpPost]
        public Object Post(Orders o)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string queryString = "INSERT INTO Orders (CustomerID, OrderDate, ShipAddress)";
            queryString += " VALUES (@CustomerID, @OrderDate, @ShipAddress);";
            queryString += " SELECT SCOPE_IDENTITY();";

            connection.ExecuteScalar<int>(queryString, o);

            return new { success = true };

        }

        [HttpDelete("{id}")]
        public Object Delete(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string deleteCommand = "DELETE FROM Orders WHERE OrderID = @id";

            connection.Execute(deleteCommand, new { id = id });

            return new { success = true };

        }
    }
}