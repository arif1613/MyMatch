using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMatch.Models.verification
{
    public class email_ver_main
    {
        public long ID { get; set; }
        public string username_main { get; set; }
        public string email_main { get; set; }
        public DateTime evdate_main { get; set; }
        public int evcode_main { get; set; }
    }
}