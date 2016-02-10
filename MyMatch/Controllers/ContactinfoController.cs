using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyMatch.Models;
using MyMatch.Models.payment;
using MyMatch.Models.verification;

namespace MyMatch.Controllers
{
    public class ContactinfoController : Controller
    {
        //
        // GET: /Contactinfo/
        mymatch_profileDB db = new mymatch_profileDB();
        veificationDB db3 = new veificationDB();
        string member_name;
        public ActionResult contactinfo_Index(string user_name)
        {
            membership_check mc = new membership_check();
            string status = mc.member_status(User.Identity.Name);
            member_name = user_name;

            if (status == "Normal" && user_name != User.Identity.Name)
            {
                return PartialView("contactinfo_normal", user_name);
            }
            else
            {
                var x = db.contactinfos.Where(r => r.username == user_name).FirstOrDefault();
                if (x.email.Split('+').First() == "UV")
                {
                    ViewBag.email_ver = "unverified";
                }
                else
                {
                    ViewBag.email_ver = "verified";
                }

                if (x.contactnumber.Split(',').First() == "UV")
                {
                    ViewBag.mobile_ver = "unverified";
                }
                else
                {
                    ViewBag.mobile_ver = "verified";
                }

                if (User.Identity.Name == user_name)
                {
                    x.contactnumber = x.contactnumber.Split(',').Last();
                    x.email = x.email.Split('+').Last();
                    x.fblink = x.fblink.Split('+').Last();
                    x.orkutlink = x.orkutlink.Split('+').Last();
                    x.permenant_address = x.permenant_address.Split('+').Last();
                    x.permenant_address_city = x.permenant_address_city.Split('+').Last();
                    x.present_address = x.present_address.Split('+').Last();
                    x.present_address_city = x.present_address_city.Split('+').Last();
                    x.twitterlink = x.twitterlink.Split('+').Last();
                }
                else
                {
                    x.contactnumber = x.contactnumber.Split(',').Last();
                    x.email = x.email.Split('+').Last();

                    if (x.fblink.Split('+').First() == "hidden")
                    {
                        x.fblink = "Information is hidden";
                    }
                    else
                    {
                        x.fblink = x.fblink.Split('+').Last();
                    }
                    if (x.orkutlink.Split('+').First() == "hidden")
                    {
                        x.orkutlink = "Information is hidden";
                    }
                    else
                    {
                        x.orkutlink = x.orkutlink.Split('+').Last();
                    }
                    if (x.permenant_address.Split('+').First() == "hidden")
                    {
                        x.permenant_address = "Information is hidden";
                    }
                    else
                    {
                        x.permenant_address = x.permenant_address.Split('+').Last();
                    }
                    if (x.permenant_address_city.Split('+').First() == "hidden")
                    {
                        x.permenant_address_city = "Information is hidden";
                    }
                    else
                    {
                        x.permenant_address_city = x.permenant_address_city.Split('+').Last();
                    }
                    if (x.present_address.Split('+').First() == "hidden")
                    {
                        x.present_address = "Information is hidden";
                    }
                    else
                    {
                        x.present_address = x.present_address.Split('+').Last();
                    }
                    if (x.present_address_city.Split('+').First() == "hidden")
                    {
                        x.present_address_city = "Information is hidden";
                    }
                    else
                    {
                        x.present_address_city = x.present_address_city.Split('+').Last();
                    }
                    if (x.twitterlink.Split('+').First() == "hidden")
                    {
                        x.twitterlink = "Information is hidden";
                    }
                    else
                    {
                        x.twitterlink = x.twitterlink.Split('+').Last();
                    }

                }
                return PartialView(x);
            }
        }

        //normal user 
        public ActionResult contactinfo_normal(string user)
        {
            //ViewBag.member_name = user;
            return PartialView(user);
        }

        //editing
        public ActionResult contactinfo_edit(int id)
        {
            var x = db.contactinfos.Find(id);
            if (x.contactnumber.Split(',').Last() == "Not provided yet")
            {
                x.contactnumber = "";
            }
            else
            {
                x.contactnumber = x.contactnumber.Split(',').Last();
            }
            if (x.email.Split('+').Last() == "Not provided yet")
            {
                x.email = "";
            }
            else
            {
                x.email = x.email.Split('+').Last();
            }
            if (x.fblink == "visible+Not provided yet")
            {
                x.fblink = "";
            }
            else
            {
                x.fblink = x.fblink.Split('+').Last();
            }
            if (x.orkutlink == "visible+Not provided yet")
            {
                x.orkutlink = "";
            }
            else
            {
                x.orkutlink = x.orkutlink.Split('+').Last();
            }
            if (x.permenant_address == "visible+Not provided yet")
            {
                x.permenant_address = "";
            }
            else
            {
                x.permenant_address = x.permenant_address.Split('+').Last();
            }
            if (x.permenant_address_city == "visible+Not provided yet")
            {
                x.permenant_address_city = "";
            }
            else
            {
                x.permenant_address_city = x.permenant_address_city.Split('+').Last();
            }
            if (x.present_address == "visible+Not provided yet")
            {
                x.present_address = "";
            }
            else
            {
                x.present_address = x.present_address.Split('+').Last();
            }
            if (x.present_address_city == "visible+Not provided yet")
            {
                x.present_address_city = "";
            }
            else
            {
                x.present_address_city = x.present_address_city.Split('+').Last();
            }
            if (x.twitterlink == "visible+Not provided yet")
            {
                x.twitterlink = "";
            }
            else
            {
                x.twitterlink = x.twitterlink.Split('+').Last();
            }
            List<string> li = new List<string> { "Andaman&Nicobor","Andhra Pradesh","Arunachal Pradesh","Assam","Bihar","Chandigarh",
            "Chattishgarh","Dadra&Nagar","Daman&Diu","Delhi","Goa","Gujrat","Haryana","Himachal Pradesh","Jammu&Kashmir","Jharkhand",
            "Karnataka","Kerala","Lakshadeep","Madhya Pradesh","Maharashtra","Manipur","Meghalaya","Mizoram","Nagaland","Orissa",
            "Pondicherry","Punjab","Rajasthan","Sikkim","Tamil Nadu","Tripura","Uttar Pradesh","Uttaranchal","West Bengal","Others"};
            ViewBag.cities = li;

            return PartialView(x);
        }


        [HttpPost]
        public ActionResult contactinfo_edit(profile_contactinfo pconinfo, string present_address_option, string present_address_city_option,
            string permenant_address_option, string permenant_address_city_option, string fblink_option,
            string orkutlink_option, string twitterlink_option)
        {
            if (!string.IsNullOrEmpty(pconinfo.present_address))
            {
                pconinfo.present_address = string.Format("{0}+{1}", present_address_option, pconinfo.present_address);
            }
            else
            {
                pconinfo.present_address = "visible+Not provided yet";
            }
            if (!string.IsNullOrEmpty(pconinfo.present_address_city))
            {
                pconinfo.present_address_city = string.Format("{0}+{1}", present_address_city_option, pconinfo.present_address_city);
            }
            else
            {
                pconinfo.present_address_city = "visible+Not provided yet";
            }
            if (!string.IsNullOrEmpty(pconinfo.permenant_address))
            {
                pconinfo.permenant_address = string.Format("{0}+{1}", permenant_address_option, pconinfo.permenant_address);
            }
            else
            {
                pconinfo.permenant_address = "visible+Not provided yet";
            }
            if (!string.IsNullOrEmpty(pconinfo.permenant_address_city))
            {
                pconinfo.permenant_address_city = string.Format("{0}+{1}", permenant_address_city_option, pconinfo.permenant_address_city);
            }
            else
            {
                pconinfo.permenant_address_city = "visible+Not provided yet";
            }

            if (!string.IsNullOrEmpty(pconinfo.contactnumber))
            {
                var p1 = db3.mobile_main.Where(r => r.musername_main == User.Identity.Name).ToList();
                if (p1.Count() > 0)
                {
                    if (p1.FirstOrDefault().mobilenumber_main == pconinfo.contactnumber.Split(',').Last())
                    {
                        pconinfo.contactnumber = string.Format("V,{0}", pconinfo.contactnumber);
                    }
                    else
                    {
                        pconinfo.contactnumber = string.Format("UV,{0}", pconinfo.contactnumber);
                    }
                }
                else
                {
                    pconinfo.contactnumber = string.Format("UV,{0}", pconinfo.contactnumber);
                }
            }
            else
            {
                pconinfo.contactnumber = "UV,Not provided yet";
            }


            if (!string.IsNullOrEmpty(pconinfo.email))
            {
                var p = db3.email_main.Where(r => r.username_main == User.Identity.Name).ToList();
                if (p.Count() > 0)
                {
                    if (p.FirstOrDefault().email_main.Split('+').Last() == pconinfo.email)
                    {
                        pconinfo.email = string.Format("V+{0}", pconinfo.email);
                    }
                    else
                    {
                        pconinfo.email = string.Format("UV+{0}", pconinfo.email);
                    }
                }
                else
                {
                    pconinfo.email = string.Format("UV+{0}", pconinfo.email);
                }
            }
            else
            {
                pconinfo.email = "UV+Not provided yet";
            }

            if (!string.IsNullOrEmpty(pconinfo.fblink))
            {
                pconinfo.fblink = string.Format("{0}+{1}", fblink_option, pconinfo.fblink);
            }
            else
            {
                pconinfo.fblink = "visible+Not provided yet";
            }
            if (!string.IsNullOrEmpty(pconinfo.orkutlink))
            {
                pconinfo.orkutlink = string.Format("{0}+{1}", orkutlink_option, pconinfo.orkutlink);
            }
            else
            {
                pconinfo.orkutlink = "visible+Not provided yet";
            }
            if (!string.IsNullOrEmpty(pconinfo.twitterlink))
            {
                pconinfo.twitterlink = string.Format("{0}+{1}", twitterlink_option, pconinfo.twitterlink);
            }
            else
            {
                pconinfo.twitterlink = "visible+Not provided yet";
            }
            db.Entry(pconinfo).State = EntityState.Modified;
            db.SaveChanges();
            Thread.Sleep(1000);
            return RedirectToAction("contactinfo_Index", new { user_name = User.Identity.Name });
        }
    }
}
