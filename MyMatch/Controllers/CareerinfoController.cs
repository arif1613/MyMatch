using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMatch.Models;

namespace MyMatch.Controllers
{
    public class CareerinfoController : Controller
    {
        //
        // GET: /Careerinfo/
        mymatch_profileDB db = new mymatch_profileDB();
        public ActionResult careerinfo_Index(string user_name)
        {
            var x = db.careerinfos.Where(r => r.username == user_name).FirstOrDefault();
            if (User.Identity.Name == user_name)
            {
                x.education = x.education.Split('+').Last();
                x.education_institute = x.education_institute.Split('+').Last();
                x.profession = x.profession.Split('+').Last();
                x.salary = x.salary.Split('+').Last();
                x.work_company = x.work_company.Split('+').Last();
            }
            else {
                if (x.education.Split('+').First() == "hidden")
                {
                    x.education = "Information is hidden";
                }
                else
                {
                    x.education = x.education.Split('+').Last();
                }
                if (x.education_institute.Split('+').First() == "hidden")
                {
                    x.education_institute = "Information is hidden";
                }
                else
                {
                    x.education_institute = x.education_institute.Split('+').Last();
                }
                if (x.profession.Split('+').First() == "hidden")
                {
                    x.profession = "Information is hidden";
                }
                else
                {
                    x.profession = x.profession.Split('+').Last();
                }
                if (x.salary.Split('+').First() == "hidden")
                {
                    x.salary = "Information is hidden";
                }
                else
                {
                    x.salary = x.salary.Split('+').Last();
                }
                if (x.work_company.Split('+').First() == "hidden")
                {
                    x.work_company = "Information is hidden";
                }
                else
                {
                    x.work_company = x.work_company.Split('+').Last();
                }
            }
            return PartialView(x);
        }

        //editing
        public ActionResult careerinfo_edit(int id)
        {
            var x = db.careerinfos.Find(id);
            if (x.education == "visible+Not provided yet")
            {
                x.education = "";
            }
            else
            {
                x.education = x.education.Split('+').Last();
            }
            if (x.education_institute == "visible+Not provided yet")
            {
                x.education_institute = "";
            }
            else
            {
                x.education_institute = x.education_institute.Split('+').Last();
            }
            if (x.profession == "visible+Not provided yet")
            {
                x.profession = "";
            }
            else
            {
                x.profession = x.profession.Split('+').Last();
            }
            if (x.salary == "visible+Not provided yet")
            {
                x.salary = "";
            }
            else
            {
                x.salary = x.salary.Split('+').Last();
            }
            if (x.work_company == "visible+Not provided yet")
            {
                x.work_company = "";
            }
            else
            {
                x.work_company = x.work_company.Split('+').Last();
            }
            return PartialView(x);
        }

        [HttpPost]
        public ActionResult careerinfo_edit(profile_careerinfo pcinfo, string education_option, string education_institute_option,
            string profession_option, string work_company_option, string salary_option)
        {
            if (!string.IsNullOrEmpty(pcinfo.education))
            {
                pcinfo.education = string.Format("{0}+{1}", education_option, pcinfo.education);
            }
            else
            {
                pcinfo.education = "visible+Not provided yet";
            }

            if (!string.IsNullOrEmpty(pcinfo.education_institute))
            {
                pcinfo.education_institute = string.Format("{0}+{1}", education_institute_option, pcinfo.education_institute);
            }
            else
            {
                pcinfo.education_institute = "visible+Not provided yet";
            }
            if (!string.IsNullOrEmpty(pcinfo.profession))
            {
                pcinfo.profession = string.Format("{0}+{1}", profession_option, pcinfo.profession);
            }
            else
            {
                pcinfo.profession = "visible+Not provided yet";
            }
            if (!string.IsNullOrEmpty(pcinfo.salary))
            {
                pcinfo.salary = string.Format("{0}+{1}", salary_option, pcinfo.salary);
            }
            else
            {
                pcinfo.salary = "visible+Not provided yet";
            }
            if (!string.IsNullOrEmpty(pcinfo.work_company))
            {
                pcinfo.work_company = string.Format("{0}+{1}", work_company_option, pcinfo.work_company);
            }
            else
            {
                pcinfo.work_company = "visible+Not provided yet";
            }
            db.Entry(pcinfo).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("careerinfo_Index", new { user_name=User.Identity.Name});
        }

    }
}
