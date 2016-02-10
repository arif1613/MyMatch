using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyMatch.Models.verification
{
    public class veificationDB : DbContext
    {
        public DbSet<email_ver_temp> email_temp { get; set; }
        public DbSet<email_ver_main> email_main { get; set; }
        public DbSet<mobile_ver_temp> mobile_temp { get; set; }
        public DbSet<mobile_ver_main> mobile_main { get; set; }
    }
}