using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMatch.Models
{
    public class profile_contactinfo
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Username:")]
        public string username { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Present Address:")]
        public string present_address { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Present City:")]
        public string present_address_city { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Permenant Address:")]
        public string permenant_address { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Permenant City:")]
        public string permenant_address_city { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail:")]
        public string email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number:")]
        public string contactnumber { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "FaceBook Link:")]
        public string fblink { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Twitter Link:")]
        public string twitterlink { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Orkut Link:")]
        public string orkutlink { get; set; }

    }
}