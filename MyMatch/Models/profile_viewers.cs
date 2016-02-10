using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMatch.Models
{
    public class profile_viewers
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Username:")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Profile Viewer:")]
        public string profile_name { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "View Date:")]
        public DateTime visit_date { get; set; }

    }
}