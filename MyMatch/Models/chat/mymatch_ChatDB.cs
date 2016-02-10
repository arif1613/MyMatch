using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyMatch.Models.chat
{
    public class mymatch_ChatDB:DbContext
    {
        public DbSet<ChatMessage> chatmessages { get; set; }
        public DbSet<online_user> ousers { get; set; }
    }

    public class wfDB : DbContext
    {
    }
}