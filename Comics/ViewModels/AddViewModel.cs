using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comics.ViewModels
{
    public class AddViewModel
    {

        public HttpPostedFileWrapper file { get; set; }
        public int qty { get; set; }
        public string title { get; set; }
        public string category { get; set; }
        public decimal price { get; set; }
    }
}