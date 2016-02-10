using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMatch.Models
{
    public class saved_profiles
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Username:")]
        public string username { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Profile Name:")]
        public string profile_name { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Saved Date:")]
        public DateTime save_date { get; set; }

    }
}