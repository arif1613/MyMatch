using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMatch.Models;
using MyMatch.Models.chat;

namespace MyMatch.Controllers
{
    [Authorize(Roles = "aa1613")]
    public class RefreshDBController : Controller
    {
        mymatch_profileDB db = new mymatch_profileDB();
        mymatch_ChatDB db1 = new mymatch_ChatDB();
        //
        // GET: /RefreshDB/

        public ActionResult refresh_DBIndex()
        {
            return View();
        }

        //delete profile viewers db
        public ActionResult del_viewerDB(DateTime fromdate, DateTime todate)
        {
            var v = db.profile_viewers.Where(r => r.visit_date >= fromdate).ToList();
            var v1 = v.Where(r => r.visit_date <= todate).ToList();
            foreach (var k in v1)
            {
                db.profile_viewers.Remove(k);
            }
            db.SaveChanges();
            return RedirectToAction("refresh_DBIndex");
        }

        //delete message db
        public ActionResult del_messageDB(DateTime fromdate, DateTime todate) {
            var v = db.messages.Where(r => r.msg_date >= fromdate).ToList();
            var v1 = v.Where(r => r.msg_date <= todate).ToList();
            foreach (var k in v1) {
                db.messages.Remove(k);
            }
            db.SaveChanges();

            return RedirectToAction("refresh_DBIndex");
        }

        //delete message db
        public ActionResult del_chatDB(DateTime fromdate, DateTime todate)
        {
            var v = db1.chatmessages.Where(r => r.dt>= fromdate).ToList();
            var v1 = v.Where(r => r.dt <= todate).ToList();
            foreach (var k in v1)
            {
                db1.chatmessages.Remove(k);
            }
            db1.SaveChanges();

            return RedirectToAction("refresh_DBIndex");
        }

    }
}
