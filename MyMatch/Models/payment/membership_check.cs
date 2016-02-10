using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MyMatch.Models;

namespace MyMatch.Models.payment
{

    public class membership_check
    {
        mymatch_profileDB db = new mymatch_profileDB();
        public string member_status(string username)
        {
            var v = db.logininfos.Where(r => r.username == username).ToList();
            if (v.Count() > 0)
            {
                if (v.FirstOrDefault().user_status == "Premium")
                {
                    return "Premium";
                }
                else
                {
                    return "Normal";
                }
            }
            else
            {
                return "Normal";
            }
        }
        public void change_status(string username) {
            var v = db.logininfos.Where(r => r.username == username).ToList();
            var dt = DateTime.UtcNow;
            if (v.Count() > 0) {
                if (v.FirstOrDefault().user_status=="Premium") {
                    if (dt > v.FirstOrDefault().update_date) {
                        v.FirstOrDefault().user_status = "Normal";
                        db.Entry(v.FirstOrDefault()).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                
                }
            }
        }
    }
}