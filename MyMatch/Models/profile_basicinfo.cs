using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMatch.Models
{
    public class profile_basicinfo
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Username:")]
        public string username { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name:")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date of Birth:")]
        public DateTime DOB { get; set; }

        [Required]
        [Display(Name = "Age:")]
        public int age { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Sex:")]
        public string sex { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Languages (seperate with ','):")]
        public string languages { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Marital Status:")]
        public string marital_status { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = "Height(cm.):")]
        public string height { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Weight(kg.):")]
        public string weight { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Religious View:")]
        public string Religion { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Religion Cast:")]
        public string religion_cast { get; set; }

    }
}