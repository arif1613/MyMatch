using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMatch.Models
{
    public class lookingfor
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Username:")]
        public string username { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "About Me:")]
        public string aboutme { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Looking For:")]
        public string looking_for { get; set; }

    }
}