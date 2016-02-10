using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MyMatch.Views.Contactinfo;

namespace MyMatch.Models
{
    public class mymatch_profileDB:DbContext
    {
        public DbSet<lookingfor> lookingfors { get; set; }
        public DbSet<message> messages { get; set; }
        public DbSet<profile_basicinfo> basicinfos { get; set; }
        public DbSet<profile_careerinfo> careerinfos { get; set; }
        public DbSet<profile_contactinfo> contactinfos { get; set; }
        public DbSet<profile_logininfo> logininfos { get; set; }
        public DbSet<profile_viewers> profile_viewers { get; set; }
        public DbSet<saved_profiles> saved_profiles { get; set; }
        public DbSet<show_interest> show_interests { get; set; }
    }
}