using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyMatch.Views.Contactinfo;

namespace MyMatch.Models
{

    public class primary_registration
    {
        mymatch_profileDB db = new mymatch_profileDB();
        lookingfor lf = new lookingfor();
        profile_basicinfo pbinfo = new profile_basicinfo();
        profile_careerinfo pcinfo = new profile_careerinfo();
        profile_contactinfo pconinfo = new profile_contactinfo();
        profile_logininfo plinfo = new profile_logininfo();

        public void primaryprofile(RegisterModel rm) {
            //adding data in looking for
            lf.username = rm.UserName;
            lf.looking_for = "visible+Not provided yet";
            lf.aboutme = "visible+Not provided yet";
            db.lookingfors.Add(lf);

            //adding data in basic info
            pbinfo.username = rm.UserName;
            pbinfo.weight = "visible+Not provided yet";
            pbinfo.sex = "Not provided yet";
            pbinfo.religion_cast = "Not provided yet";
            pbinfo.Religion = "Not provided yet";
            pbinfo.Name = "visible+Not provided yet";
            pbinfo.marital_status = "Not provided yet";
            pbinfo.languages = "visible+Not provided yet";
            pbinfo.height = "visible+Not provided yet";
            pbinfo.DOB = rm.DOB;
            pbinfo.age = DateTime.Now.Year - rm.DOB.Year;
            db.basicinfos.Add(pbinfo);

            //adding data in career info
            pcinfo.education = "visible+Not provided yet";
            pcinfo.education_institute = "visible+Not provided yet";
            pcinfo.profession = "visible+Not provided yet";
            pcinfo.salary = "visible+Not provided yet";
            pcinfo.username = rm.UserName;
            pcinfo.work_company = "visible+Not provided yet";
            db.careerinfos.Add(pcinfo);

            //adding data in contactinfo
            pconinfo.contactnumber = "UV,Not provided yet";
            pconinfo.email = "UV+"+rm.email;
            pconinfo.fblink = "visible+Not provided yet";
            pconinfo.orkutlink = "visible+Not provided yet";
            pconinfo.permenant_address = "visible+Not provided yet";
            pconinfo.permenant_address_city = "visible+Not provided yet";
            pconinfo.present_address = "visible+Not provided yet";
            pconinfo.present_address_city = "visible+Not provided yet";
            pconinfo.twitterlink = "visible+Not provided yet";
            pconinfo.username = rm.UserName;
            db.contactinfos.Add(pconinfo);

            //adding data in login info
            plinfo.emailaddress = rm.email;
            plinfo.password = rm.Password;
            plinfo.update_date = DateTime.UtcNow.Date;
            plinfo.user_status = "Normal";
            plinfo.username = rm.UserName;
            db.logininfos.Add(plinfo);

            db.SaveChanges();

        }
    }
}