using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMatch.Models.others
{
    public class others
    {
        public int ID { get; set; }
        public string username { get; set; }
        public string Father_name { get; set; }
        public string Mother_name { get; set; }
        public string smoking_habbit { get; set; }
        public string drinking_habbit { get; set; }
        public string eating_habbit { get; set; }
        public string body_type { get; set; }
        public string complexion { get; set; }
        public string physical_status { get; set; }

    }
}