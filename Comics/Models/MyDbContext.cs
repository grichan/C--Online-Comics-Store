using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Comics.Models
{
    public class MyDbContext : DbContext
    {

        public MyDbContext()
        {

        }
        public DbSet<Items> Items { get; set; } // create corresponding db sets for classes 
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Users> Users { get; set; }



    }
}