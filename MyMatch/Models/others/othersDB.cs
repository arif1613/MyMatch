using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MyMatch.Models.others;

namespace MyMatch.Models.others
{
    public class othersDB:DbContext
    {
        public DbSet<others> other_infos { get; set; }
    }
}