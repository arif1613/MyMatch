using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyMatch.Models;

namespace MyMatch.Models
{

    public class quicksearch_basicinfo
    {
        mymatch_profileDB db = new mymatch_profileDB();
        List<profile_basicinfo> pbli = new List<profile_basicinfo>();
        List<profile_basicinfo> final = new List<profile_basicinfo>();
        string username;

        public quicksearch_basicinfo()
        {
        }
        public quicksearch_basicinfo(string user)
        {
            username = user;
            var s = db.basicinfos.Where(r => r.username == username).Select(r => r.sex).FirstOrDefault();
            if (s == "Male")
            {
                var v = db.basicinfos.Where(r => r.sex == "Female").ToList();
                pbli = v;
            }
            else
            {
                var v = db.basicinfos.Where(r => r.sex == "Male").ToList();
                pbli = v;
            }
        }
        public quicksearch_basicinfo(string religion, string username)
            : this(username)
        {
            string rel = religion.Split(',').First();
            string cast = religion.Split(',').Last();
            var v = pbli.Where(r => r.Religion == rel).ToList();
            var v1 = v.Where(r => r.religion_cast == cast).OrderBy(r => r.username).ToList();
            pbli = v1;
        }
        public quicksearch_basicinfo(int age, string religion, string username)
            : this(religion, username)
        {
            var x = pbli.Where(r => r.age == age).OrderBy(r => r.username).ToList();
            pbli = x;

        }
        public quicksearch_basicinfo(string m_status, string religion, string username)
            : this(religion, username)
        {
            var x = pbli.Where(r => r.marital_status == m_status).OrderBy(r => r.username).ToList();
            pbli = x;
        }

        public List<profile_basicinfo> check_gender(string user)
        {
            username = user;
            var s = db.basicinfos.Where(r => r.username == username).Select(r => r.sex).FirstOrDefault();
            if (s == "Male")
            {
                var v = db.basicinfos.Where(r => r.sex == "Female").ToList();
                return v;
            }
            else
            {
                var v = db.basicinfos.Where(r => r.sex == "Male").ToList();
                return v;
            }

        }

        public List<profile_basicinfo> search_final()
        {
            return pbli;
        }

    }

}