using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comics.Models
{
    public class Items
    {
        //COMICS
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; } //PUBLISHER
        public decimal price { get; set; }
        public int qty { get; set; }
        public string iamgeURL { get; set; }


    }
}