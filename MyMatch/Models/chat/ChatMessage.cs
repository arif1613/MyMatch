using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMatch.Models
{
    public class ChatMessage
    {
        public long ID { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string msg { get; set; }
        public DateTime dt { get; set; }

    }
}