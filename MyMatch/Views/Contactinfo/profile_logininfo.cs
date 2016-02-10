using System;
using System.ComponentModel.DataAnnotations;

namespace MyMatch.Views.Contactinfo
{
    public class profile_logininfo
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Username:")]
        public string username { get; set; }
        [Required]
        [Display(Name = "Password:")]
        public string password { get; set; }
        [Required]
        [Display(Name = "E-mail:")]
        [DataType(DataType.EmailAddress)]
        public string emailaddress { get; set; }
        [Required]
        [Display(Name = "Status:")]
        public string user_status { get; set; }
        [Required]
        [Display(Name = "Update Date:")]
        [DataType(DataType.DateTime)]
        public DateTime update_date { get; set; }

    }
}