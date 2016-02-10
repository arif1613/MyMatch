using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyMatch.Models;

namespace MyMatch.Models
{
    public class quicksearch_contactinfo
    {
        List<profile_contactinfo> pci = new List<profile_contactinfo>();
        mymatch_profileDB db = new mymatch_profileDB();

        public quicksearch_contactinfo(string s)
        {
            string gender = db.basicinfos.Where(r => r.username == s).Select(r => r.sex).FirstOrDefault();
            if (gender == "Male")
            {
                var v = db.basicinfos.Where(r => r.sex == "Female").ToList();
                foreach (var x in v)
                {
                    var p = db.contactinfos.Where(r => r.username == x.username).FirstOrDefault();
                    pci.Add(p);
                }
            }
            else
            {
                var v = db.basicinfos.Where(r => r.sex == "Male").ToList();
                foreach (var x in v)
                {
                    var p = db.contactinfos.Where(r => r.username == x.username).FirstOrDefault();
                    pci.Add(p);
                }
            }
        }

        //search by city
        public quicksearch_contactinfo(string city, string username)
            : this(username)
        {
            string p = string.Format("visible+{0}", city);
            var v = pci.Where(r => r.present_address_city == p).ToList();
            pci = v;
        }

        //filter by city
        public quicksearch_contactinfo(string present_city, string city, string username)
            : this(city, username)
        {
            string p = string.Format("visible+{0}", present_city);
            var v = pci.Where(r => r.permenant_address_city == p).ToList();
            pci = v;

        }


        //returning list of profiles
        public List<profile_contactinfo> show_result()
        {
            return pci.OrderBy(r => r.username).ToList();
        }

    }
}
public class searchby_permenantcity : quicksearch_contactinfo
{

    List<profile_contactinfo> pb = new List<profile_contactinfo>();
    public searchby_permenantcity(string permenant_city, string username)
        : base(username)
    {
        pb = base.show_result();
        string p = "visible+" + permenant_city;
        var v = pb.Where(r => r.permenant_address_city == p).ToList();
        pb = v;
    }
    public searchby_permenantcity(string permenant_city, string present_city, string username)
        : this(permenant_city, username)
    {

        string p1 = "visible+" + present_city;
        var v = pb.Where(r => r.present_address_city == p1).ToList();
        pb = v;
    }

    public List<profile_contactinfo> show()
    {
        return pb;
    }
}

public class filter_by_profession : quicksearch_contactinfo
{

    List<profile_contactinfo> pc = new List<profile_contactinfo>();
    public List<profile_careerinfo> pcar = new List<profile_careerinfo>();
    public List<profile_careerinfo> pcar1 = new List<profile_careerinfo>();
    mymatch_profileDB db = new mymatch_profileDB();

    public filter_by_profession(string present_city, string profession, string username)
        : base(present_city, username)
    {
        pc = base.show_result();//returns list of contactinfos

        foreach (var v in pc)
        {
            var m = db.careerinfos.Where(r => r.username == v.username).FirstOrDefault();
            pcar.Add(m);
        }
        string pro = "visible+" + profession;
        pcar1 = pcar.Where(r => r.profession == pro).OrderBy(r => r.username).ToList();
    }

    public List<profile_careerinfo> show_career()
    {
        return pcar1;
    }


}