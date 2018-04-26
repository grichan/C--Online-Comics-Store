using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comics.Models
{
    public class Orders
    {
        public int id { get; set; }
        public string username { get; set; }
        public string product_ids{ get; set; }
        public decimal total { get; set; }
        public int qty { get; set; }
        public DateTime date { get; set; }
    }
}