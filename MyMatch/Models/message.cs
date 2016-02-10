using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMatch.Models
{
    public class message
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Username:")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Message Date:")]
        public DateTime msg_date { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "From:")]
        public string msg_sender { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "To:")]
        public string msg_receiver { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Message:")]
        public string msg_body { get; set; }

    }
}