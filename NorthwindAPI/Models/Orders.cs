using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindAPI.Models
{
    public class Orders
    {
        //OrderID, CustomerID, OrderDate, ShipAddress
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShipAddress { get; set; }
    }
}
