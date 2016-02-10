using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyMatch.Models;
using MyMatch.Views.Contactinfo;

namespace MyMatch.Controllers
{
    [Authorize(Roles = "aa1613")]
    public class profile_logininfosController : Controller
    {
        mymatch_profileDB db = new mymatch_profileDB();


        public JsonResult quicksearch_logininfo(string term)
        {

            var v = db.logininfos.Where(r => r.username.Contains(term)).Select(r => new { label = r.username })
                .Take(5).ToList();
            return Json(v, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Serach_result(string u_name)
        {
            var v = db.logininfos.Where(r => r.username == u_name).FirstOrDefault();
            return PartialView(v);

        }
        // GET: /profile_logininfos/
        public ActionResult login_info_Index()
        {
            var x = db.logininfos.OrderBy(r => r.update_date).ToList();
            ViewBag.total = x.Count();
            return View(x);
        }
        public ActionResult login_info_Edit(int id)
        {
            var v = db.logininfos.Find(id);
            return PartialView(v);
        }

        [HttpPost]
        public ActionResult login_info_Edit(profile_logininfo plmodel)
        {
            db.Entry(plmodel).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("login_info_Index");
        }


        public ActionResult login_info_Delete(int id)
        {

            var v = db.logininfos.Find(id);
            return PartialView(v);
        }

        [HttpPost]
        public ActionResult login_info_Delete(profile_logininfo v)
        {
            profile_logininfo v1 = db.logininfos.Find(v.ID);

            //delet login info
            string s = v1.username;
            db.logininfos.Remove(v1);

            //deleting basic info
            profile_basicinfo pbinfo = db.basicinfos.Where(r => r.username == s).FirstOrDefault();
            db.basicinfos.Remove(pbinfo);

            //deleting career info
            profile_careerinfo c = db.careerinfos.Where(r => r.username == s).FirstOrDefault();
            db.careerinfos.Remove(c);

            //deleting contact info
            profile_contactinfo con = db.contactinfos.Where(r => r.username == s).FirstOrDefault();
            db.contactinfos.Remove(con);

            //deleting looking for database
            var lf = db.lookingfors.Where(r => r.username == s).ToList();
            if (lf.Count() > 0)
            {
                foreach (var x in lf)
                {
                    db.lookingfors.Remove(x);
                }
            }

            //deleting profile viewers database
            var pv = db.profile_viewers.Where(r => r.username == s).ToList();
            if (pv.Count() > 0)
            {
                foreach (var x in pv)
                {
                    db.profile_viewers.Remove(x);
                }
            }
            //deleting profile viewers database
            var pv1 = db.profile_viewers.Where(r => r.profile_name == s).ToList();
            if (pv1.Count() > 0)
            {
                foreach (var x in pv1)
                {
                    db.profile_viewers.Remove(x);
                }
            }

            var msg = db.messages.Where(r => r.msg_sender == s).ToList();
            if (msg.Count() > 0)
            {
                foreach (var y in msg)
                {
                    db.messages.Remove(y);
                }
            }

            //delete interest database
            var interest = db.show_interests.Where(r => r.username == s).ToList();
            if (interest.Count() > 0)
            {
                foreach (var z in interest)
                {
                    db.show_interests.Remove(z);
                }
            }

            var interest1 = db.show_interests.Where(r => r.interested_people == s).ToList();
            if (interest1.Count() > 0)
            {
                foreach (var z in interest1)
                {
                    db.show_interests.Remove(z);
                }
            }

            //deleting save profile database

            var sp = db.saved_profiles.Where(r => r.username == s).ToList();
            if (sp.Count() > 0)
            {
                foreach (var i in sp)
                {
                    db.saved_profiles.Remove(i);
                }
            }

            var sp1 = db.saved_profiles.Where(r => r.profile_name == s).ToList();
            if (sp1.Count() > 0)
            {
                foreach (var i in sp1)
                {
                    db.saved_profiles.Remove(i);
                }
            }

            //deleting pictures
            var pic1 = s + "..1.png";
            var pic2 = s + "..2.png";
            var file1 = Path.Combine(Server.MapPath("~/Images/Pictures"), pic1);
            var file2 = Path.Combine(Server.MapPath("~/Images/Pictures"), pic2);
            FileInfo fi1 = new FileInfo(file1);
            FileInfo fi2 = new FileInfo(file2);
            if (fi1.Exists)
            {
                fi1.Delete();
            }
            if (fi2.Exists)
            {
                fi2.Delete();
            }


            //deleting membership

            Membership.DeleteUser(v.username);

            db.SaveChanges();
            if (User.Identity.Name == s)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("login_info_Index");
        }
    }
}
