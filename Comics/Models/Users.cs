using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comics.Models
{
    public class Users
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }

    }
}