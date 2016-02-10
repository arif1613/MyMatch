using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using MyMatch.Models;

namespace MyMatch.Controllers
{
    public class user_profileController : Controller
    {
        mymatch_profileDB db = new mymatch_profileDB();
        //
        // GET: /user_profile/

        public ActionResult user_profile_Index(string user_name)
        {
            //show interest checking
            string p;
            var v = db.show_interests.Where(r => r.interested_people == User.Identity.Name).ToList();
            var v1 = v.Where(r => r.username == user_name).ToList();
            if (v1.Count() > 0)
            {
                p = "interested";
            }
            else
            {
                p = "not interested";
            }
            ViewBag.interest = p;
            
            //Saved profile checking
            var sp = db.saved_profiles.Where(r => r.username == User.Identity.Name).ToList();
            var sp1 = sp.Where(r => r.profile_name == user_name).ToList();
            if (sp1.Count() > 0)
            {
                ViewBag.saved = "saved";

            }
            else {
                ViewBag.saved = "not saved";
            }
            if (!string.IsNullOrEmpty(user_name) && user_name != "Search by Username...")
            {
                if (user_name.Contains(',') && user_name.Split(',').First() != User.Identity.Name)
                {
                    string s = user_name.Split(',').First();
                    var x = db.logininfos.Where(r => r.username == s).ToList();
                    if (x.Count() > 0)
                    {
                        var p1 = db.profile_viewers.Where(r => r.username == User.Identity.Name).ToList();
                        var p2 = p1.Where(r => r.profile_name == s).ToList();
                        profile_viewers pv = new profile_viewers();
                        if (p2.Count() <= 0)
                        {
                            pv.profile_name = s;
                            pv.username = User.Identity.Name;
                            pv.visit_date = DateTime.UtcNow;
                            db.profile_viewers.Add(pv);
                        }
                        else
                        {
                            p2.FirstOrDefault().visit_date = DateTime.UtcNow;
                            db.Entry(p2.FirstOrDefault()).State = EntityState.Modified;

                            //db.SaveChanges();
                        }
                        db.SaveChanges();
                                               
                        var x1 = db.logininfos.Where(r => r.username == s).FirstOrDefault();
                        return View(x1);
                    }
                    else
                    {
                        return RedirectToAction("Index", "myprofile");

                    }
                }
                else
                {
                    if (user_name != User.Identity.Name)
                    {
                        var x = db.logininfos.Where(r => r.username == user_name).ToList();
                        if (x.Count() > 0)
                        {
                            var p1 = db.profile_viewers.Where(r => r.username == User.Identity.Name).ToList();
                            var p2 = p1.Where(r => r.profile_name == user_name).ToList();
                            profile_viewers pv = new profile_viewers();
                            if (p2.Count() <= 0)
                            {
                                pv.profile_name = user_name;
                                pv.username = User.Identity.Name;
                                pv.visit_date = DateTime.UtcNow;
                                db.profile_viewers.Add(pv);

                            }
                            else
                            {
                                p2.FirstOrDefault().visit_date = DateTime.UtcNow;
                                db.Entry(p2.FirstOrDefault()).State = EntityState.Modified;
                            }
                            db.SaveChanges();

                            var x1 = db.logininfos.Where(r => r.username == user_name).FirstOrDefault();
                            return View(x1);
                        }

                        return RedirectToAction("Index", "myprofile");
                    }
                    else
                    {
                        return RedirectToAction("Index", "myprofile");

                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "myprofile");
            }
        }

        //show interest
        public ActionResult Show_interest(string u_name)
        {
            show_interest si = new show_interest();
            si.username = u_name;
            si.interested_people = User.Identity.Name;
            si.interest_date = DateTime.Now;
            db.show_interests.Add(si);
            db.SaveChanges();

            //send mail
            MailMessage message = new MailMessage();
            var z = db.logininfos.Where(r => r.username == u_name).ToList();
            string email = z.FirstOrDefault().emailaddress;

            message.From = new MailAddress("info@indianbibah.com");
            message.To.Add(email);
            const string fromPassword = "aa1613++";
            message.Subject = string.Format("{0} has showed interest to your profile", User.Identity.Name);
            message.Body = string.Format("Hello  {0}," + "<br /><br /><br />" + "<a style=" + "color:maroon>" +
                "{1}  " + "</a>" + "has showed interest to your profile on  {2}  at  {3}. Visit your profile to contact with {4}.",
            u_name, User.Identity.Name, DateTime.UtcNow.ToString("dd-MMM"), DateTime.UtcNow.ToString("hh:mm"), User.Identity.Name);

            message.Body += "<br /><br /><br /><br />" + "<p style=" + "color:gray" + ">" +
                "This is an automatic generated mail. Please do not reply to this mail." + "</p>";
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient
            {
                Host = "mail.indianbibah.com",
                Port = 587,
                EnableSsl = false,
                Credentials = new NetworkCredential("info@indianbibah.com", fromPassword)
            };
            smtp.Send(message);
            Thread.Sleep(1000);
            var x = db.show_interests.Where(r => r.username == u_name).FirstOrDefault();
            return RedirectToAction("user_profile_Index", new { user_name = u_name });
        }

        //remove interest
        public ActionResult remove_interest(string u_name)
        {
            var si = db.show_interests.Where(r => r.interested_people == User.Identity.Name).ToList();
            var si1 = si.Where(r => r.username == u_name).FirstOrDefault();
            db.show_interests.Remove(si1);
            db.SaveChanges();
            return RedirectToAction("user_profile_Index", new { user_name = u_name });
        }

        public ActionResult save_profile(string u_name) {

            saved_profiles sp = new saved_profiles();
            sp.username = User.Identity.Name;
            sp.profile_name = u_name;
            sp.save_date = DateTime.UtcNow;
            db.saved_profiles.Add(sp);
            db.SaveChanges();
            return RedirectToAction("user_profile_Index", new { user_name = u_name });
        }

        public ActionResult delete_saving(string u_name) {

            var v = db.saved_profiles.Where(r => r.profile_name == u_name).ToList();
            var v1 = v.Where(r => r.username == User.Identity.Name).ToList();
            if (v1.Count() > 0) {
                foreach (var p in v1) {
                    db.saved_profiles.Remove(p);
                }
            
            }
            db.SaveChanges();
            return RedirectToAction("user_profile_Index", new { user_name = u_name });
        }

    }
}
