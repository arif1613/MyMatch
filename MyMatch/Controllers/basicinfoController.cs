using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using MyMatch.Models;

namespace MyMatch.Controllers
{
    [Authorize]
    public class basicinfoController : Controller
    {
        //
        // GET: /basicinfo/
        mymatch_profileDB db = new mymatch_profileDB();
        profile_basicinfo x = new profile_basicinfo();
        public ActionResult basicinfo_Index(string user_name)
        {
            var x = db.basicinfos.Where(r => r.username == user_name).FirstOrDefault();
            if (User.Identity.Name == user_name)
            {
                x.Name = x.Name.Split('+').Last();
                x.height = x.height.Split('+').Last();
                x.languages = x.languages.Split('+').Last();
                x.weight = x.weight.Split('+').Last();
            }
            else {
                if (x.Name.Split('+').First() == "hidden")
                {
                    x.Name = "Information is hidden";
                }
                else {
                    x.Name = x.Name.Split('+').Last();
                }
                if (x.height.Split('+').First() == "hidden")
                {
                    x.height = "Information is hidden";
                }
                else
                {
                    x.height = x.height.Split('+').Last();
                }
                if (x.languages.Split('+').First() == "hidden")
                {
                    x.languages = "Information is hidden";
                }
                else
                {
                    x.languages = x.languages.Split('+').Last();
                }
                if (x.weight.Split('+').First() == "hidden")
                {
                    x.weight = "Information is hidden";
                }
                else
                {
                    x.weight = x.weight.Split('+').Last();
                }
                
            }
            return PartialView(x);
        }

        //editing
        public ActionResult Edit(int id)
        {
            profile_basicinfo x = db.basicinfos.Find(id);

            if (x.Name == "visible+Not provided yet")
            {
                x.Name = "";
            }
            else
            {
                x.Name = x.Name.Split('+').Last();
            }
            if (x.height == "visible+Not provided yet")
            {
                x.height = "";
            }
            else
            {
                x.height = x.height.Split('+').Last();
            }
            if (x.languages == "visible+Not provided yet")
            {
                x.languages = "";
            }
            else
            {
                x.languages = x.languages.Split('+').Last();
            }
            if (x.marital_status == "Not provided yet")
            {
                x.marital_status = "";
            }
            
            if (x.Religion == "Not provided yet")
            {
                x.Religion = "";
            }
            
            if (x.religion_cast == "Not provided yet")
            {
                x.religion_cast = "";
            }
            if (x.sex == "Not provided yet")
            {
                x.sex = "";
            }
            if (x.weight == "visible+Not provided yet")
            {
                x.weight = "";
            }
            else
            {
                x.weight = x.weight.Split('+').Last();
            }
            return PartialView(x);
        }
        [HttpPost]
        public ActionResult Edit(profile_basicinfo pbinfo, string name_option, string languages_option,
            string height_option, string weight_option)
        {
            if (!string.IsNullOrEmpty(pbinfo.Name))
            {
                pbinfo.Name = string.Format("{0}+{1}", name_option, pbinfo.Name);
            }
            else
            {
                pbinfo.Name = "visible+Not provided yet";
            }

            if (string.IsNullOrEmpty(pbinfo.DOB.ToString()))
            {
                DateTime dt = db.basicinfos.Where(r => r.username == User.Identity.Name)
                    .Select(r => r.DOB).FirstOrDefault();
                pbinfo.DOB = dt;
            }

            if (!string.IsNullOrEmpty(pbinfo.languages))
            {
                pbinfo.languages = string.Format("{0}+{1}", languages_option, pbinfo.languages);
            }
            else
            {
                pbinfo.languages = "visible+Not provided yet";
            }

            if (!string.IsNullOrEmpty(pbinfo.height))
            {
                pbinfo.height = string.Format("{0}+{1}", height_option, pbinfo.height);
            }
            else
            {
                pbinfo.height = "visible+Not provided yet";
            }

            if (!string.IsNullOrEmpty(pbinfo.weight))
            {
                pbinfo.weight = string.Format("{0}+{1}", weight_option, pbinfo.weight);
            }
            else
            {
                pbinfo.weight = "visible+Not provided yet";
            }
            if (!string.IsNullOrEmpty(pbinfo.religion_cast))
            {
                pbinfo.religion_cast = pbinfo.religion_cast;
            }
            else
            {
                pbinfo.religion_cast = "Not provided yet";
            }

            //age count
            int i = DateTime.UtcNow.Year - pbinfo.DOB.Year;
            pbinfo.age = i;

            //updating data
            db.Entry(pbinfo).State = EntityState.Modified;
            db.SaveChanges();
            Thread.Sleep(1000);
            return RedirectToAction("basicinfo_Index", new { user_name = User.Identity.Name });

        }
    }
}
