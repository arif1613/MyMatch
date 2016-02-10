using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using MyMatch.Models.others;

namespace MyMatch.Controllers.others
{
    public class othersController : Controller
    {
        //
        // GET: /others/
        othersDB db2 = new othersDB();

        public ActionResult others_Index(string username)
        {
            var v1 = db2.other_infos.Where(r => r.username == username).ToList();
            if (v1.Count() > 0)
            {
                var v = v1.FirstOrDefault();

                if (string.IsNullOrEmpty(v.smoking_habbit))
                {
                    v.smoking_habbit = "Not provided yet";
                }
                if (string.IsNullOrEmpty(v.physical_status))
                {
                    v.physical_status = "Not provided yet";
                }
                if (string.IsNullOrEmpty(v.Mother_name))
                {
                    v.Mother_name = "Not provided yet";
                }
                if (string.IsNullOrEmpty(v.Father_name))
                {
                    v.Father_name = "Not provided yet";
                }
                if (string.IsNullOrEmpty(v.eating_habbit))
                {
                    v.eating_habbit = "Not provided yet";
                }
                if (string.IsNullOrEmpty(v.drinking_habbit))
                {
                    v.drinking_habbit = "Not provided yet";
                }
                if (string.IsNullOrEmpty(v.complexion))
                {
                    v.complexion = "Not provided yet";
                }
                if (string.IsNullOrEmpty(v.body_type))
                {
                    v.body_type = "Not provided yet";
                }
                return PartialView(v);
            }
            else
            {
                MyMatch.Models.others.others ot = new Models.others.others();
                ot.username = username;
                ot.smoking_habbit = "Not provided yet";
                ot.physical_status = "Not provided yet";
                ot.Mother_name = "Not provided yet";
                ot.Father_name = "Not provided yet";
                ot.eating_habbit = "Not provided yet";
                ot.drinking_habbit = "Not provided yet";
                ot.complexion = "Not provided yet";
                ot.body_type = "Not provided yet";
                return PartialView(ot);

            }

        }

        public ActionResult edit_others()
        {
            string s = User.Identity.Name;
            var v = db2.other_infos.Where(r => r.username == s).ToList();

            List<string> li = new List<string> { "Slim", "Athletic", "Average", "Chuby", "Heavy" };
            ViewBag.body_type = li;
            List<string> li1 = new List<string> { "Very Fair", "Fair", "Wheatish", "Brown", "Dark" };
            ViewBag.complexion = li1;
            List<string> li2 = new List<string> { "Normal", "Physically Challenged" };
            ViewBag.physical_status = li2;
            List<string> li3 = new List<string> { "Vegetarian", "Non-vegetarian", "Eggetarian" };
            ViewBag.eating_habbit = li3;
            List<string> li4 = new List<string> { "Smoker", "Non-smoker", "Occationally smoker" };
            ViewBag.smoking_habbit = li4;
            List<string> li5 = new List<string> { "Drinker", "Non-drinker", "Occationally drinker" };
            ViewBag.drinking_habbit = li5;


            if (v.Count() > 0)
            {
                if (v.FirstOrDefault().Mother_name == "Not provided yet")
                {
                    v.FirstOrDefault().Mother_name = "";
                }
                if (v.FirstOrDefault().Father_name == "Not provided yet")
                {
                    v.FirstOrDefault().Father_name = "";
                }

                return PartialView(v.FirstOrDefault());
            }
            else
            {
                MyMatch.Models.others.others ot = new Models.others.others();
                ot.username = s;
                ot.smoking_habbit = "Not provided yet";
                ot.physical_status = "Not provided yet";
                ot.Mother_name = "";
                ot.Father_name = "";
                ot.eating_habbit = "Not provided yet";
                ot.drinking_habbit = "Not provided yet";
                ot.complexion = "Not provided yet";
                ot.body_type = "Not provided yet";
                return PartialView(ot);
            }
        }

        [HttpPost]
        public ActionResult edit_others(MyMatch.Models.others.others ot)
        {
            if (string.IsNullOrEmpty(ot.Mother_name))
            {
                ot.Mother_name = "Not provided yet";
            }
            if (string.IsNullOrEmpty(ot.Father_name))
            {
                ot.Father_name = "Not provided yet";
            }

            var v = db2.other_infos.Where(r => r.username == ot.username);

            if (v.Count() > 0)
            {
                db2.Entry(ot).State = EntityState.Modified;
                db2.SaveChanges();
                Thread.Sleep(1000);
                return RedirectToAction("others_Index", new { username = User.Identity.Name });
            }
            else
            {
                db2.other_infos.Add(ot);
                db2.SaveChanges();
                return RedirectToAction("others_Index", new { username = User.Identity.Name });
            }


        }

    }
}
