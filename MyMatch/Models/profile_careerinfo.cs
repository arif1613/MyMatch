using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMatch.Models
{
    public class profile_careerinfo
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Username:")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Education Level:")]
        public string education { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Education Institute:")]
        public string education_institute { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Profession:")]
        public string profession { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Company:")]
        public string work_company { get; set; }

        [Display(Name = "Salary:")]
        public string salary { get; set; }
                
    }
}