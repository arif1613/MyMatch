using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MyMatch.Models;
using MyMatch.Models.payment;

namespace MyMatch.Controllers
{
    [Authorize]
    public class Profile_ViewersController : Controller
    {
        mymatch_profileDB db = new mymatch_profileDB();

        //
        // GET: /Profile_Viewers/
        public ActionResult profile_viewer_Index()
        {
            var v = db.profile_viewers.OrderByDescending(r => r.visit_date)
                .Where(r => r.profile_name == User.Identity.Name).ToList();
            ViewBag.total = v.Count();

            List<int> i = new List<int>();
            List<int> li = new List<int>();
            DateTime dt = DateTime.UtcNow;
            var z = v.Where(r => r.visit_date.Day >= (dt.Day - 7)).AsEnumerable().ToList();
            var z1 = z.Select(r => r.visit_date.Day).Distinct();
            foreach (var n in z1)
            {
                var z2 = z.Where(r => r.visit_date.Day == n).ToList();
                i.Add(z2.Count());
                li.Add(n);
            }
            // chart

            var mychart = new Chart(width: 250, height: 150, theme: ChartTheme.Blue);
            // mychart.AddTitle(dt.ToString("MMMM,yyyy"));
            mychart.AddSeries(
            "Default",
                xValue: li,
                yValues: i
                );
            //mychart.DataBindTable(i);
            string s = User.Identity.Name;
            var filename = string.Format("~/Images/pv_{0}.png", s);
            mychart.Save(filename);

            ViewBag.weeklytotal = li.Count();

            //membership check
            membership_check mc = new membership_check();
            string status = mc.member_status(s);
            ViewBag.status = status;
            if (status == "Premium")
            {
                //paging
                decimal p = v.Count();
                ViewBag.total_page = Math.Ceiling(p / 10);
                ViewBag.pagenumber = 1;
                var v1 = v.Take(10);
                //
                return View(v1);
            }
            else
            {
                var v2 = v.Take(5);
                //paging
                decimal p = v2.Count();
                ViewBag.total_page = Math.Ceiling(p / 5);
                ViewBag.pagenumber = 1;
                //
                return View(v2);
            }
        }
        [HttpPost]
        public ActionResult profile_viewer_Index(int page_number)
        {
            var v = db.profile_viewers.OrderByDescending(r => r.visit_date)
                .Where(r => r.profile_name == User.Identity.Name).ToList();
            ViewBag.total = v.Count();

            //membership check
            membership_check mc = new membership_check();
            string s = User.Identity.Name;
            string status = mc.member_status(s);
            ViewBag.status = status;
            if (status == "Premium")
            {
                //paging
                decimal p = v.Count();
                ViewBag.total_page = Math.Ceiling(p / 10);
                ViewBag.pagenumber = page_number;
                var v1 = v.Skip((page_number - 1) * 10).Take(10);
                //
                return View(v1);
            }
            else
            {
                var v1 = v.Take(5);
                //paging
                decimal p = v1.Count();
                ViewBag.total_page = Math.Ceiling(p / 5);
                ViewBag.pagenumber = 1;

                //
                return View(v1);

            }
        }

        //normal pv
        public ActionResult pv_normal()
        {

            return PartialView();

        }

        public ActionResult profile_viewers_show(string viewer)
        {
            var x = db.profile_viewers.Where(r => r.profile_name == User.Identity.Name).ToList();
            var x1 = x.OrderByDescending(r => r.visit_date).FirstOrDefault(r => r.username == viewer);
            return PartialView(x1);
        }

        public ActionResult profile_viewer_partial()
        {
            var v = db.profile_viewers.Where(r => r.profile_name == User.Identity.Name)
                .OrderByDescending(r => r.visit_date).ToList();
            var v1 = v.Take(5);
            ViewBag.total = v.Count();
            return PartialView(v);

        }


    }
}
