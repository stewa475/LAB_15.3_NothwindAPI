using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindAPI.Models
{
    public class Customers
    {
        //CustomerID, CompanyName, ContactName, Country
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Country { get; set; }
    }
}
