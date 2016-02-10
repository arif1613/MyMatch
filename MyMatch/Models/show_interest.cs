using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMatch.Models
{
    public class show_interest
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Username:")]
        public string username { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Interested People:")]
        public string interested_people { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Interest Date:")]
        public DateTime interest_date { get; set; }

    }
}