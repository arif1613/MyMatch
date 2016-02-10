using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMatch.Models;

namespace MyMatch.Controllers
{
    [Authorize]
    public class lookingforController : Controller
    {
        mymatch_profileDB db = new mymatch_profileDB();

        //about me

        public ActionResult aboutme_Index(string user_name)
        {
            var x = db.lookingfors.Where(r => r.username == user_name).FirstOrDefault();
            if (User.Identity.Name == user_name)
            {
                x.aboutme = x.aboutme.Split('+').Last();
            }
            else {
                if (x.aboutme.Split('+').First() == "hidden")
                {
                    x.aboutme = "Information is hidden";
                }
                else
                {
                    x.aboutme = x.aboutme.Split('+').Last();
                }
            
            }
            return PartialView(x);
        }

        public ActionResult aboutme_edit(int id)
        {
            var x = db.lookingfors.Find(id);
            if (x.aboutme == "visible+Not provided yet")
            {
                x.aboutme = "";
            }
            else
            {
                x.aboutme = x.aboutme.Split('+').Last();
            }
            return PartialView(x);
        }

        [HttpPost]
        public ActionResult aboutme_edit(lookingfor lf, string aboutme_option)
        {

            if (!string.IsNullOrEmpty(lf.aboutme))
            {
                lf.aboutme = string.Format("{0}+{1}", aboutme_option, lf.aboutme);
            }
            else
            {
                lf.aboutme = "visible+Not provided yet";
            }
            db.Entry(lf).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("aboutme_Index", new { user_name = User.Identity.Name });
        }

        //looking for

        public ActionResult lookingfor_Index(string user_name)
        {
            var x = db.lookingfors.Where(r => r.username == user_name).FirstOrDefault();
            if (User.Identity.Name == user_name)
            {
                x.looking_for = x.looking_for.Split('+').Last();
            }
            else
            {
                if (x.looking_for.Split('+').First() == "hidden")
                {
                    x.looking_for = "Information is hidden";
                }
                else
                {
                    x.looking_for = x.looking_for.Split('+').Last();
                }

            } return PartialView(x);
        }

        public ActionResult lookingfor_edit(int id)
        {
            var x = db.lookingfors.Find(id);
            if (x.looking_for == "visible+Not provided yet")
            {
                x.looking_for = "";
            }
            else
            {
                x.looking_for = x.looking_for.Split('+').Last();
            }
            return PartialView(x);
        }

        [HttpPost]
        public ActionResult lookingfor_edit(lookingfor lf, string lookingfor_option)
        {

            if (!string.IsNullOrEmpty(lf.looking_for))
            {
                lf.looking_for = string.Format("{0}+{1}", lookingfor_option, lf.looking_for);
            }
            else
            {
                lf.looking_for = "visible+Not provided yet";
            }
            db.Entry(lf).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("lookingfor_Index", new { user_name=User.Identity.Name});
        }
    }
}

