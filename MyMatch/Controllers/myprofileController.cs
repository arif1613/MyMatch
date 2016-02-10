using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyMatch.Models;
using MyMatch.Models.chat;
using MyMatch.Models.payment;
using MyMatch.Models.verification;
using MyMatch.Models.others;
using MyMatch.Views.Contactinfo;

namespace MyMatch.Controllers
{
    public class myprofileController : Controller
    {
        mymatch_profileDB db = new mymatch_profileDB();
        mymatch_ChatDB db1 = new mymatch_ChatDB();
        othersDB db2 = new othersDB();
        veificationDB db3 = new veificationDB();
        //
        // GET: /myprofile/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                //checking interested peoples
                var x = db.show_interests.Where(r => r.username == User.Identity.Name).OrderByDescending(r => r.interest_date).ToList();
                ViewBag.interest_count = x.Count();

                //checking saved profiles
                var x1 = db.saved_profiles.Where(r => r.username == User.Identity.Name).OrderByDescending(r => r.save_date).ToList();
                ViewBag.saved_profiles = x1.Count();

                profile_logininfo v = db.logininfos.Where(r => r.username == User.Identity.Name).FirstOrDefault();
                var v1 = db.logininfos.Select(r => r.username).ToList();
                ViewBag.name = v1;

                membership_check mc = new membership_check();
                string status = mc.member_status(User.Identity.Name);
                ViewBag.status = status;
                return View(v);
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }
        }

        //my picture
        public ActionResult my_picture(string user_name)
        {
            var x = db.logininfos.Where(r => r.username == user_name).FirstOrDefault();
            return PartialView(x);
        }

        //editing picture
        public ActionResult edit_picture()
        {
            var x = db.logininfos.Where(r => r.username == User.Identity.Name).FirstOrDefault();
            return PartialView(x);
        }
        [HttpPost]
        public ActionResult edit_picture1(HttpPostedFileBase edit_picture1)
        {
            string s = User.Identity.Name;

            if (edit_picture1 != null && edit_picture1.ContentLength > 0)
            {
                //deleting pictures
                var pic1 = s + "..1.png";
                var file1 = Path.Combine(Server.MapPath("~/Images/Pictures"), pic1);
                FileInfo fi1 = new FileInfo(file1);
                if (fi1.Exists)
                {
                    fi1.Delete();
                }

                var filename1 = Path.GetFileName(edit_picture1.FileName);
                string newfilename1 = s + "..1.png";
                var file_path1 = Path.Combine(Server.MapPath("~/Images/Pictures"), newfilename1);
                edit_picture1.SaveAs(file_path1);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult edit_picture2(HttpPostedFileBase edit_picture2)
        {
            string s = User.Identity.Name;


            if (edit_picture2 != null && edit_picture2.ContentLength > 0)
            {

                //deleting pictures
                var pic2 = s + "..2.png";
                var file2 = Path.Combine(Server.MapPath("~/Images/Pictures"), pic2);
                FileInfo fi2 = new FileInfo(file2);
                if (fi2.Exists)
                {
                    fi2.Delete();
                }
                var filename2 = Path.GetFileName(edit_picture2.FileName);
                string newfilename2 = s + "..2.png";
                var file_path2 = Path.Combine(Server.MapPath("~/Images/Pictures"), newfilename2);
                edit_picture2.SaveAs(file_path2);
            }
            return RedirectToAction("Index");
        }
        public ActionResult deleteprofile(int id)
        {
            profile_logininfo v = db.logininfos.Find(id);
            return View(v);
        }

        [HttpPost]
        public ActionResult delete_profile(int id)
        {

            profile_logininfo v = db.logininfos.Find(id);
            string s = v.username;
            db.logininfos.Remove(v);
            profile_basicinfo pbinfo = db.basicinfos.Where(r => r.username == s).FirstOrDefault();
            db.basicinfos.Remove(pbinfo);
            profile_careerinfo c = db.careerinfos.Where(r => r.username == s).FirstOrDefault();
            db.careerinfos.Remove(c);
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
            var pv1 = db.profile_viewers.Where(r => r.profile_name == s).ToList();
            if (pv1.Count() > 0)
            {
                foreach (var x1 in pv1)
                {
                    db.profile_viewers.Remove(x1);
                }
            }

            //deleting message database

            var msg = db.messages.Where(r => r.msg_sender == s).ToList();
            if (msg.Count() > 0)
            {
                foreach (var y in msg)
                {
                    db.messages.Remove(y);
                }
            }

            //

            //deleting interested people data base
            var interest = db.show_interests.Where(r => r.interested_people == s).ToList();
            if (interest.Count() > 0)
            {
                foreach (var z in interest)
                {
                    db.show_interests.Remove(z);
                }
            }
            var interest1 = db.show_interests.Where(r => r.username == s).ToList();
            if (interest1.Count() > 0)
            {
                foreach (var z1 in interest1)
                {
                    db.show_interests.Remove(z1);
                }
            }

            //delete saved profiles
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
                foreach (var i1 in sp1)
                {
                    db.saved_profiles.Remove(i1);
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

            //deleting chart
            var chart_pic = "pv_" + s + ".png";
            var file3 = Path.Combine(Server.MapPath("~/Images"), chart_pic);
            FileInfo fi3 = new FileInfo(file3);
            if (fi3.Exists)
            {
                fi3.Delete();
            }
            //delete chat histories
            var chat_user = db1.ousers.Where(r => r.ouser == s).ToList();
            if (chat_user.Count() > 0)
            {
                db1.ousers.Remove(chat_user.FirstOrDefault());
            }
            var chat_message = db1.chatmessages.Where(r => r.from == s).ToList();
            foreach (var k in chat_message)
            {
                db1.chatmessages.Remove(k);
            }

            var ou = db1.ousers.Where(r => r.ouser == s).ToList();
            foreach (var k1 in ou)
            {
                db1.ousers.Remove(k1);
            }

            //deleting others

            var r1 = db2.other_infos.Where(r => r.username == s).ToList();
            if (r1.Count() > 0)
            {
                foreach (var a in r1)
                {
                    db2.other_infos.Remove(a);
                }
            }
            //deleting verification
            var verm = db3.mobile_temp.Where(r => r.musername_temp == s).ToList();
            if (verm.Count() > 0)
            {
                foreach (var k in verm)
                {
                    db3.mobile_temp.Remove(k);
                }
            }

            var verm1 = db3.mobile_main.Where(r => r.mobilenumber_main == s).ToList();
            if (verm1.Count() > 0)
            {
                foreach (var k1 in verm1)
                {
                    db3.mobile_main.Remove(k1);
                }
            }
            var verm2 = db3.email_temp.Where(r => r.username_temp == s).ToList();
            if (verm2.Count() > 0)
            {
                foreach (var k2 in verm2)
                {
                    db3.email_temp.Remove(k2);
                }
            }

            var verm3 = db3.email_main.Where(r => r.username_main == s).ToList();
            if (verm3.Count() > 0)
            {
                foreach (var k3 in verm3)
                {
                    db3.email_main.Remove(k3);
                }
            }

            db.SaveChanges();
            db1.SaveChanges();
            db2.SaveChanges();
            db3.SaveChanges();

            //deleting membership
            Membership.DeleteUser(v.username);
            FormsAuthentication.SignOut();


            return RedirectToAction("Index", "Home");
        }

        //interested peoples
        public ActionResult ïnterested_people(int page_number)
        {
            var x = db.show_interests.Where(r => r.username == User.Identity.Name)
                .OrderByDescending(r => r.interest_date).ToList();
            ViewBag.count = x.Count();

            //paging
            decimal p = x.Count();
            ViewBag.totalpage = Math.Ceiling(p / 10);
            ViewBag.pagenumber = page_number;
            var v1 = x.Skip((page_number - 1) * 10).Take(10);
            //
            return View(v1);
        }

        public ActionResult saved_profiles(int page_number)
        {
            var x = db.saved_profiles.Where(r => r.username == User.Identity.Name).OrderByDescending(r => r.save_date).ToList();
            ViewBag.count = x.Count();
            //paging
            decimal p = x.Count();
            ViewBag.totalpage = Math.Ceiling(p / 10);
            ViewBag.pagenumber = page_number;
            var v1 = x.Skip((page_number - 1) * 10).Take(10);
            //
            return View(v1);
        }
        //remove from list
        public ActionResult remove_from_list(string interested_people)
        {
            var x = db.show_interests.Where(r => r.username == User.Identity.Name).ToList();
            var si = x.Where(r => r.interested_people == interested_people).ToList();
            if (si.Count() > 0)
            {
                foreach (var p in si)
                {
                    db.show_interests.Remove(p);
                }
                db.SaveChanges();
                return RedirectToAction("ïnterested_people", new { page_number = 1 });

            }
            else
            {
                return RedirectToAction("ïnterested_people", new { page_number = 1 });
            }
        }

        public ActionResult remove_from_saving(string saved_profile, int page_number)
        {
            var x = db.saved_profiles.Where(r => r.username == User.Identity.Name).ToList();
            var x1 = x.Where(r => r.profile_name == saved_profile).ToList();
            if (x1.Count() > 0)
            {
                foreach (var p in x1)
                {
                    db.saved_profiles.Remove(p);
                }

                db.SaveChanges();
                return RedirectToAction("saved_profiles", new { page_number = page_number });
            }
            else
            {
                return RedirectToAction("saved_profiles", new { page_number = 1 });
            }

        }

        //photo galary
        public ActionResult photo_galary(string user)
        {
            ViewBag.user = user;
            return View();
        }
    }
}