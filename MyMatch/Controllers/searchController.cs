using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMatch.Models;
using MyMatch.Models.payment;

namespace MyMatch.Controllers
{
    public class searchController : Controller
    {

        mymatch_profileDB db = new mymatch_profileDB();
        membership_check mc = new membership_check();
        //search by username

        public JsonResult quicksearch_username(string term)
        {
            var v = db.basicinfos.OrderBy(r => r.username).ToList();
            var v1 = v.Where(r => r.username.Contains(term)).Take(7).Select(r => new { label = string.Format("{0}, {1} ,Age:{2}", r.username,r.marital_status, r.age) });
            return Json(v1, JsonRequestBehavior.AllowGet);
        }

        public ActionResult search_Index()
        {
            string s = User.Identity.Name;
            string ver_email = db.contactinfos.Where(r => r.username == s).Select(r => r.email).FirstOrDefault();
            string ver_mobile = db.contactinfos.Where(r => r.username == s).Select(r => r.contactnumber).FirstOrDefault();
            string ver_email1 = ver_email.Split('+').First();
            string ver_mobile1 = ver_mobile.Split(',').First();
            if ((ver_mobile1 == "V") && (ver_email1 == "V"))
            {
                return PartialView();
            }
            else {
                return Content("Please verify both your contact e-mail address and mobile number to use our search facility.");
            }
        }

        //search by present city

        public JsonResult quicksearch_presentcity(string term)
        {
            string s = User.Identity.Name;
            List<profile_contactinfo> li = new List<profile_contactinfo>();
            string sex = db.basicinfos.Where(r => r.username == s).Select(r => r.sex).FirstOrDefault();
            if (sex == "Male")
            {
                var d = db.basicinfos.Where(r => r.sex == "Female").ToList();
                foreach (var i in d)
                {
                    profile_contactinfo x = db.contactinfos.Where(r => r.username == i.username).FirstOrDefault();
                    li.Add(x);
                }
                var v = li.Where(r => r.present_address_city.StartsWith("visible"))
                    .OrderBy(r => r.present_address_city).ToList();

                var v1 = v.Where(r => r.present_address_city != "visible+Not provided yet").ToList();

                var v2 = v1.Where(r => r.present_address_city.Split('+').Last().Contains(term))
                    .Select(r => new { label = r.present_address_city.Split('+').Last() }).Take(5).Distinct();

                return Json(v2, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var d = db.basicinfos.Where(r => r.sex == "Male").ToList();
                foreach (var i in d)
                {
                    profile_contactinfo x = db.contactinfos.Where(r => r.username == i.username).FirstOrDefault();
                    li.Add(x);
                }
                var v = li.Where(r => r.present_address_city.StartsWith("visible"))
                    .OrderBy(r => r.present_address_city).ToList();

                var v1 = v.Where(r => r.present_address_city != "visible+Not provided yet").ToList();

                var v2 = v1.Where(r => r.present_address_city.Split('+').Last().Contains(term))
                    .Select(r => new { label = r.present_address_city.Split('+').Last() }).Take(5).Distinct();

                return Json(v2, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult search_by_presentcity(string presentcity, int page_number)
        {
            if (!string.IsNullOrEmpty(presentcity) && presentcity != "Search present city...")
            {
                List<profile_contactinfo> li = new List<profile_contactinfo>();
                string s = User.Identity.Name;
                quicksearch_contactinfo qc = new quicksearch_contactinfo(presentcity, s);
                var v = qc.show_result();
                ViewBag.total = v.Count();

                string status = mc.member_status(s);
                ViewBag.status = status;
                if (status == "Premium")
                {
                    //paging
                    decimal p = v.Count();
                    ViewBag.totalpage = Math.Ceiling(p / 15);
                    ViewBag.pagenumber = page_number;
                    var v1 = v.Skip((page_number - 1) * 15).Take(15);
                    ViewBag.presentcity = presentcity;
                    //

                    return View(v1);
                }
                else {
                    var v2 = v.Take(10);
                    //paging
                    decimal p = v2.Count();
                    ViewBag.totalpage = Math.Ceiling(p / 10);
                    ViewBag.pagenumber = page_number;
                    var v3 = v2.Skip((page_number - 1) * 10).Take(10);
                    ViewBag.presentcity = presentcity;
                    //
                    return View(v3);
                }
            }
            else
            {
                return RedirectToAction("Index", "myprofile");
            }
        }

        //search by permenant city

        public JsonResult quicksearch_permenantcity(string term)
        {
            string s = User.Identity.Name;
            List<profile_contactinfo> li = new List<profile_contactinfo>();
            string sex = db.basicinfos.Where(r => r.username == s).Select(r => r.sex).FirstOrDefault();
            if (sex == "Male")
            {
                var d = db.basicinfos.Where(r => r.sex == "Female").ToList();
                foreach (var i in d)
                {
                    profile_contactinfo x = db.contactinfos.Where(r => r.username == i.username).FirstOrDefault();
                    li.Add(x);
                }
                var v = li.Where(r => r.permenant_address_city.StartsWith("visible"))
                    .OrderBy(r => r.permenant_address_city).ToList();

                var v1 = v.Where(r => r.permenant_address_city != "visible+Not provided yet").ToList();

                var v2 = v1.Where(r => r.permenant_address_city.Split('+').Last().Contains(term))
                    .Select(r => new { label = r.permenant_address_city.Split('+').Last() }).Take(5).Distinct();
                return Json(v2, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var d = db.basicinfos.Where(r => r.sex == "Male").ToList();
                foreach (var i in d)
                {
                    profile_contactinfo x = db.contactinfos.Where(r => r.username == i.username).FirstOrDefault();
                    li.Add(x);
                }
                var v = li.Where(r => r.permenant_address_city.StartsWith("visible"))
                    .OrderBy(r => r.permenant_address_city).ToList();

                var v1 = v.Where(r => r.permenant_address_city != "visible+Not provided yet").ToList();

                var v2 = v1.Where(r => r.permenant_address_city.Split('+').Last().Contains(term))
                    .Select(r => new { label = r.permenant_address_city.Split('+').Last() }).Take(5).Distinct();
                return Json(v2, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult search_by_permenantcity(string permenantcity, int page_number)
        {
            string s = User.Identity.Name;
            if (!string.IsNullOrEmpty(permenantcity) && permenantcity != "Search permanent State...")
            {
                searchby_permenantcity qc = new searchby_permenantcity(permenantcity, User.Identity.Name);
                var v = qc.show();
                ViewBag.total = v.Count();
                string status = mc.member_status(s);
                ViewBag.status = status;

                if (status == "Premium")
                {
                    //paging
                    decimal p = v.Count();
                    ViewBag.totalpage = Math.Ceiling(p / 15);
                    ViewBag.pagenumber = page_number;
                    var v1 = v.Skip((page_number - 1) * 15).Take(15);
                    ViewBag.permenantcity = permenantcity;
                    //
                    return View(v1);
                }
                else {
                    var v2 = v.Take(10);
                    //paging
                    decimal p = v2.Count();
                    ViewBag.totalpage = Math.Ceiling(p / 10);
                    ViewBag.pagenumber = page_number;
                    var v3 = v2.Skip((page_number - 1) * 10).Take(10);
                    ViewBag.permenantcity = permenantcity;
                    //
                    return View(v3);
                }
            }
            else
            {
                return RedirectToAction("Index", "myprofile");
            }
        }

        //search by religion

        public JsonResult quicksearch_religion(string term)
        {

            quicksearch_basicinfo pb = new quicksearch_basicinfo();
            var x = pb.check_gender(User.Identity.Name);


            var v2 = x.Where(r => r.Religion.Contains(term))
                    .Select(r => new
                    {
                        label = string.Format("{0},{1}", r.Religion,
                            r.religion_cast)
                    }).Take(5).Distinct();
            return Json(v2, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult search_by_religion(string religion, int page_number)
        {

            if (!string.IsNullOrEmpty(religion) && religion != "Search by religion...")
            {
                string s = User.Identity.Name;
                quicksearch_basicinfo qb = new quicksearch_basicinfo(religion, s);
                var v = qb.search_final();
                ViewBag.total = v.Count();

                string status = mc.member_status(s);
                ViewBag.status = status;

                if (status == "Premium")
                {
                    //paging
                    decimal p = v.Count();
                    ViewBag.totalpage = Math.Ceiling(p / 15);
                    ViewBag.pagenumber = page_number;
                    var v1 = v.Skip((page_number - 1) * 15).Take(15);
                    ViewBag.religion = string.Format("{0}", religion);
                    //
                    return View(v1);
                }
                else
                {
                    var v2 = v.Take(10);
                    //paging
                    decimal p = v2.Count();
                    ViewBag.totalpage = Math.Ceiling(p / 10);
                    ViewBag.pagenumber = page_number;
                    var v3 = v2.Skip((page_number - 1) * 10).Take(10);
                    ViewBag.religion = string.Format("{0}", religion);
                    //
                    return View(v3);
                }
            }
            else
            {
                return RedirectToAction("Index", "myprofile");

            }
        }

        //display search result

        public ActionResult display_search_result(string user_name)
        {
            var x = db.basicinfos.Where(r => r.username == user_name).FirstOrDefault();
            if (x.Name.Split('+').First() == "hidden")
            {
                x.Name = "Information is hidden";
            }
            if (x.Name.Split('+').First() == "visible")
            {
                x.Name = x.Name.Split('+').Last();
            }

            return PartialView(x);
        }


        //Filter by Permenant City

        public JsonResult quicksearch_permenantcity_filter(string present_city, string term)
        {

            string s = User.Identity.Name;
            List<profile_contactinfo> li = new List<profile_contactinfo>();
            string sex = db.basicinfos.Where(r => r.username == s).Select(r => r.sex).FirstOrDefault();
            if (sex == "Male")
            {
                var d = db.basicinfos.Where(r => r.sex == "Female").ToList();
                foreach (var i in d)
                {
                    profile_contactinfo x = db.contactinfos.Where(r => r.username == i.username).FirstOrDefault();
                    li.Add(x);
                }
                var v = li.Where(r => r.present_address_city.StartsWith("visible"))
                    .OrderBy(r => r.present_address_city).ToList();

                var v1 = v.Where(r => r.present_address_city != "visible+Not provided yet").ToList();
                string pc = string.Format("visible+{0}", present_city);
                var v2 = v1.Where(r => r.present_address_city == pc).ToList();
                var v3 = v2.Where(r => r.permenant_address_city.Split('+').Last().Contains(term))
                    .Select(r => new { label = r.permenant_address_city.Split('+').Last() }).Take(5).Distinct();
                return Json(v3, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var d = db.basicinfos.Where(r => r.sex == "Male").ToList();
                foreach (var i in d)
                {
                    profile_contactinfo x = db.contactinfos.Where(r => r.username == i.username).FirstOrDefault();
                    li.Add(x);
                }
                var v = li.Where(r => r.present_address_city.StartsWith("visible"))
                    .OrderBy(r => r.present_address_city).ToList();

                var v1 = v.Where(r => r.present_address_city != "visible+Not provided yet").ToList();
                string pc = string.Format("visible+{0}", present_city);
                var v2 = v1.Where(r => r.present_address_city == pc).ToList();
                var v3 = v2.Where(r => r.permenant_address_city.Split('+').Last().Contains(term))
                    .Select(r => new { label = r.permenant_address_city.Split('+').Last() }).Take(5).Distinct();
                return Json(v3, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult filter_by_permenantcity(string present_city, string permenant_city, int page_number)
        {
            string s = User.Identity.Name;
            if (!string.IsNullOrEmpty(permenant_city) && permenant_city != "Filter by permanent city...")
            {

                string u = User.Identity.Name;
                quicksearch_contactinfo qc = new quicksearch_contactinfo(permenant_city, present_city, u);
                var v = qc.show_result();
                ViewBag.total = v.Count();

                string status = mc.member_status(s);
                ViewBag.status = status;

                if (status == "Premium")
                {
                    //paging
                    decimal j = v.Count();
                    ViewBag.totalpage = Math.Ceiling(j / 15);
                    ViewBag.pagenumber = page_number;
                    var v1 = v.Skip((page_number - 1) * 15).Take(15);
                    ViewBag.presentcity = present_city;
                    ViewBag.permenantcity = permenant_city;
                    //
                    return View("search_by_presentcity", v1);
                }
                else {
                    var v2 = v.Take(5);
                    //paging
                    decimal j = v2.Count();
                    ViewBag.totalpage = Math.Ceiling(j / 5);
                    ViewBag.pagenumber = page_number;
                    var v3 = v2.Skip((page_number - 1) * 5).Take(5);
                    ViewBag.presentcity = present_city;
                    ViewBag.permenantcity = permenant_city;
                    //
                    return View("search_by_presentcity", v3);
                }
            }

            else
            {
                return RedirectToAction("Index", "myprofile");
            }
        }





        //Filter by Present City

        public JsonResult quicksearch_presentcity_filter(string permenant_city, string term)
        {

            string s = User.Identity.Name;
            List<profile_contactinfo> li = new List<profile_contactinfo>();
            string sex = db.basicinfos.Where(r => r.username == s).Select(r => r.sex).FirstOrDefault();
            if (sex == "Male")
            {
                var d = db.basicinfos.Where(r => r.sex == "Female").ToList();
                foreach (var i in d)
                {
                    profile_contactinfo x = db.contactinfos.Where(r => r.username == i.username).FirstOrDefault();
                    li.Add(x);
                }
                var v = li.Where(r => r.permenant_address_city.StartsWith("visible"))
                    .OrderBy(r => r.present_address_city).ToList();

                var v1 = v.Where(r => r.permenant_address_city != "visible+Not provided yet").ToList();
                string pc = string.Format("visible+{0}", permenant_city);
                var v2 = v1.Where(r => r.permenant_address_city == pc).ToList();
                var v3 = v2.Where(r => r.permenant_address_city.Split('+').Last().Contains(term))
                    .Select(r => new { label = r.present_address_city.Split('+').Last() }).Take(5).Distinct();
                return Json(v3, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var d = db.basicinfos.Where(r => r.sex == "Male").ToList();
                foreach (var i in d)
                {
                    profile_contactinfo x = db.contactinfos.Where(r => r.username == i.username).FirstOrDefault();
                    li.Add(x);
                }
                var v = li.Where(r => r.permenant_address_city.StartsWith("visible"))
                    .OrderBy(r => r.present_address_city).ToList();

                var v1 = v.Where(r => r.permenant_address_city != "visible+Not provided yet").ToList();
                string pc = string.Format("visible+{0}", permenant_city);
                var v2 = v1.Where(r => r.permenant_address_city == pc).ToList();
                var v3 = v2.Where(r => r.permenant_address_city.Split('+').Last().Contains(term))
                    .Select(r => new { label = r.present_address_city.Split('+').Last() }).Take(5).Distinct();
                return Json(v3, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult filter_by_presentcity(string permenantcity, string present_city, int page_number)
        {
            List<profile_contactinfo> p = new List<profile_contactinfo>();
            string s = User.Identity.Name;


            if (!string.IsNullOrEmpty(present_city) && present_city != "Filter by present city...")
            {
                searchby_permenantcity qc = new searchby_permenantcity(permenantcity, present_city, s);
                p = qc.show();

                ViewBag.total = p.Count();
                string status = mc.member_status(s);
                ViewBag.status = status;

                if (status == "Premium")
                {
                    //paging
                    decimal j = p.Count();
                    ViewBag.totalpage = Math.Ceiling(j / 15);
                    ViewBag.pagenumber = page_number;
                    var v = p.Skip((page_number - 1) * 15).Take(15);
                    ViewBag.presentcity = present_city;
                    ViewBag.permenantcity = permenantcity;
                    //
                    return View("search_by_permenantcity", v);
                }
                else {
                    var v2 = p.Take(5);
                    //paging
                    decimal j = v2.Count();
                    ViewBag.totalpage = Math.Ceiling(j / 5);
                    ViewBag.pagenumber = page_number;
                    var v3 = v2.Skip((page_number - 1) * 5).Take(5);
                    ViewBag.presentcity = present_city;
                    ViewBag.permenantcity = permenantcity;
                    //
                    return View("search_by_permenantcity", v3);
                
                }
            }

            else
            {
                return RedirectToAction("Index", "myprofile");
            }
        }

        //Filter by age in religion page

        public JsonResult quicksearch_religion_age(string religion, int term)
        {
            quicksearch_basicinfo pb = new quicksearch_basicinfo();
            var v = pb.check_gender(User.Identity.Name).OrderBy(r => r.username);
            string rel1 = religion.Split(',').First();
            string cast1 = religion.Split(',').Last();
            var v1 = v.Where(r => r.Religion == rel1).ToList();
            var v2 = v1.Where(r => r.religion_cast == cast1).ToList();
            var v3 = v2.Where(r => r.age == term)
                .Select(r => new { label = r.age }).Distinct();
            return Json(v3, JsonRequestBehavior.AllowGet);
        }

        public JsonResult quicksearch_religion_m_status(string religion, string term)
        {
            quicksearch_basicinfo pb = new quicksearch_basicinfo();
            var v = pb.check_gender(User.Identity.Name).OrderBy(r => r.username);
            string rel1 = religion.Split(',').First();
            string cast1 = religion.Split(',').Last();
            var v1 = v.Where(r => r.Religion == rel1).ToList();
            var v2 = v1.Where(r => r.religion_cast == cast1).ToList();
            var v3 = v2.Where(r => r.marital_status.Contains(term))
                .Select(r => new { label = r.marital_status }).Distinct();
            return Json(v3, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public ActionResult filter_religion_age(int age, string religion, int page_number)
        {
            string s = User.Identity.Name;
            List<profile_basicinfo> v = new List<profile_basicinfo>();
            if (!string.IsNullOrEmpty(age.ToString()) && age.ToString() != "Filter by age...")
            {

                quicksearch_basicinfo pb = new quicksearch_basicinfo(age, religion, s);
                v = pb.search_final();
                ViewBag.total = v.Count();

                string status = mc.member_status(s);
                ViewBag.status = status;

                if (status == "Premium")
                {
                    //paging
                    decimal j = v.Count();
                    ViewBag.totalpage = Math.Ceiling(j / 15);
                    ViewBag.pagenumber = page_number;
                    var v1 = v.Skip((page_number - 1) * 15).Take(15);
                    ViewBag.religion = religion;
                    ViewBag.age = age.ToString();
                    //

                    return View("search_by_religion", v);
                }
                else {
                    var v2 = v.Take(5);
                    //paging
                    decimal j = v2.Count();
                    ViewBag.totalpage = Math.Ceiling(j / 5);
                    ViewBag.pagenumber = page_number;
                    var v3 = v2.Skip((page_number - 1) * 5).Take(5);
                    ViewBag.religion = religion;
                    ViewBag.age = age.ToString();
                    //

                    return View("search_by_religion", v3);
                
                }
            }
            else
            {
                quicksearch_basicinfo pb = new quicksearch_basicinfo(religion, s);
                v = pb.search_final();
                ViewBag.total = v.Count();

                //paging
                decimal j = v.Count();
                ViewBag.totalpage = Math.Ceiling(j / 15);
                ViewBag.pagenumber = page_number;
                var v1 = v.Skip((page_number - 1) * 15).Take(15);
                ViewBag.religion = religion;
                ViewBag.age = age.ToString();
                //
                return View("search_by_religion", v);
            }
        }

        [HttpPost]
        public ActionResult filter_religion_m_status(string m_status, string religion, int page_number)
        {
            string s = User.Identity.Name;
            List<profile_basicinfo> v = new List<profile_basicinfo>();
            if (!string.IsNullOrEmpty(m_status) && m_status != "Filter by Marital status...")
            {

                quicksearch_basicinfo pb = new quicksearch_basicinfo(m_status, religion, s);
                v = pb.search_final();
                ViewBag.total = v.Count();

                string status = mc.member_status(s);
                ViewBag.status = status;

                if (status == "Premium")
                {
                    //paging
                    decimal j = v.Count();
                    ViewBag.totalpage = Math.Ceiling(j / 15);
                    ViewBag.pagenumber = page_number;
                    var v1 = v.Skip((page_number - 1) * 15).Take(15);
                    ViewBag.religion = religion;
                    ViewBag.m_status = m_status;
                    //

                    return View("search_by_religion", v);
                }
                else {
                    var v2 = v.Take(5);
                    //paging
                    decimal j = v2.Count();
                    ViewBag.totalpage = Math.Ceiling(j / 5);
                    ViewBag.pagenumber = page_number;
                    var v3 = v2.Skip((page_number - 1) * 5).Take(5);
                    ViewBag.religion = religion;
                    ViewBag.m_status = m_status;
                    //

                    return View("search_by_religion", v3);
                }
            }
            else
            {
                quicksearch_basicinfo pb = new quicksearch_basicinfo(religion, s);
                v = pb.search_final();
                ViewBag.total = v.Count();

                //paging
                decimal j = v.Count();
                ViewBag.totalpage = Math.Ceiling(j / 15);
                ViewBag.pagenumber = page_number;
                var v1 = v.Skip((page_number - 1) * 15).Take(15);
                ViewBag.religion = religion;
                ViewBag.m_status = m_status;
                //
                return View("search_by_religion", v);
            }
        }

        //search by profession

        public JsonResult quicksearch_profession_filter(string term, string present_city)
        {
            List<profile_contactinfo> v = new List<profile_contactinfo>();
            List<profile_careerinfo> v1 = new List<profile_careerinfo>();
            quicksearch_basicinfo pb = new quicksearch_basicinfo();
            var x = pb.check_gender(User.Identity.Name);

            foreach (var i in x)
            {
                var x1 = db.contactinfos.Where(r => r.username == i.username).FirstOrDefault();
                v.Add(x1);
            }
            var p = v.Where(r => r.present_address_city.Split('+').Last() == present_city).ToList();


            foreach (var j in p)
            {
                var x2 = db.careerinfos.Where(r => r.username == j.username).FirstOrDefault();
                v1.Add(x2);
            }

            var v2 = v1.Where(r => r.profession.Split('+').Last().Contains(term))
                .Select(r => new { label = r.profession.Split('+').Last() }).Take(5).Distinct();

            return Json(v2, JsonRequestBehavior.AllowGet);
        }

        //filter present city_profession
        [HttpPost]
        public ActionResult filter_presentcity_profession(string present_city, string profession, int page_number)
        {
            string s = User.Identity.Name;
            if (!string.IsNullOrEmpty(profession) && profession != "Filter by profession...")
            {
                List<profile_contactinfo> v = new List<profile_contactinfo>();
                List<profile_careerinfo> x = new List<profile_careerinfo>();
                filter_by_profession fpro = new filter_by_profession(present_city, profession, User.Identity.Name);

                x = fpro.show_career();

                foreach (var i in x)
                {
                    var k = db.contactinfos.FirstOrDefault(r => r.username == i.username);
                    v.Add(k);
                }

                ViewBag.total = v.Count();
                string status = mc.member_status(s);
                ViewBag.status = status;

                if (status == "Premium")
                {
                    //paging
                    decimal p = v.Count();
                    ViewBag.totalpage = Math.Ceiling(p / 15);
                    ViewBag.pagenumber = page_number;
                    var v1 = v.Skip((page_number - 1) * 15).Take(15);
                    ViewBag.presentcity = present_city;
                    ViewBag.pro = profession;
                    //

                    return View("search_by_presentcity", v1);
                }
                else {
                    var v2 = v.Take(5);
                    //paging
                    decimal p = v2.Count();
                    ViewBag.totalpage = Math.Ceiling(p / 5);
                    ViewBag.pagenumber = page_number;
                    var v3 = v2.Skip((page_number - 1) * 5).Take(5);
                    ViewBag.presentcity = present_city;
                    ViewBag.pro = profession;
                    //

                    return View("search_by_presentcity", v3);                
                }
            }
            else
            {
                return RedirectToAction("Index", "myprofile");
            }
        }

        //Json permanent city _profession
        public JsonResult quicksearch_permenantcity_profession_filter(string term, string permenant_city)
        {
            List<profile_contactinfo> v = new List<profile_contactinfo>();
            List<profile_careerinfo> v1 = new List<profile_careerinfo>();
            quicksearch_basicinfo pb = new quicksearch_basicinfo();
            var x = pb.check_gender(User.Identity.Name);

            foreach (var i in x)
            {
                var x1 = db.contactinfos.Where(r => r.username == i.username).FirstOrDefault();
                v.Add(x1);
            }
            var p = v.Where(r => r.permenant_address_city.Split('+').Last() == permenant_city).ToList();


            foreach (var j in p)
            {
                var x2 = db.careerinfos.Where(r => r.username == j.username).FirstOrDefault();
                v1.Add(x2);
            }

            var v2 = v1.Where(r => r.profession.Split('+').Last().Contains(term))
                .Select(r => new { label = r.profession.Split('+').Last() }).Take(5).Distinct();
            return Json(v2, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult filter_permanentcity_pro(string permenant_city, string pro, int page_number)
        {
            if (!string.IsNullOrEmpty(pro) && pro != "Filter by profession...")
            {
                List<profile_contactinfo> pc = new List<profile_contactinfo>();
                List<profile_contactinfo> pc1 = new List<profile_contactinfo>();
                List<profile_careerinfo> pcar = new List<profile_careerinfo>();
                searchby_permenantcity sp = new searchby_permenantcity(permenant_city, User.Identity.Name);
                pc = sp.show();

                foreach (var v in pc)
                {
                    var k = db.careerinfos.FirstOrDefault(r => r.username == v.username);
                    pcar.Add(k);
                }

                string s = "visible+" + pro;
                var v1 = pcar.Where(r => r.profession == s).ToList();
                foreach (var i in v1)
                {
                    var m = db.contactinfos.FirstOrDefault(r => r.username == i.username);
                    pc1.Add(m);
                }
                ViewBag.total = pc1.Count();
                string status = mc.member_status(s);
                ViewBag.status = status;

                if (status == "Premium")
                {
                    //paging
                    decimal p = pc1.Count();
                    ViewBag.totalpage = Math.Ceiling(p / 15);
                    ViewBag.pagenumber = page_number;
                    var v2 = pc1.Skip((page_number - 1) * 15).Take(15);
                    ViewBag.permenantcity = permenant_city;
                    ViewBag.pro = pro;
                    //
                    return View("search_by_permenantcity", v2);
                }
                else {
                    var pc2 = pc1.Take(5);
                    //paging
                    decimal p = pc2.Count();
                    ViewBag.totalpage = Math.Ceiling(p / 5);
                    ViewBag.pagenumber = page_number;
                    var v3 = pc2.Skip((page_number - 1) * 5).Take(5);
                    ViewBag.permenantcity = permenant_city;
                    ViewBag.pro = pro;
                    //
                    return View("search_by_permenantcity", v3);
                }

            }
            else
            {
                return RedirectToAction("Index", "myprofile");

            }

        }

        public ActionResult search_normal() {

            return PartialView();
        
        }

    }
}