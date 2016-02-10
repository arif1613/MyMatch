using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using MyMatch.Models;
using MyMatch.Models.chat;

namespace MyMatch.Controllers
{
    [Authorize]
    public class messageController : Controller
    {
        mymatch_ChatDB db1 = new mymatch_ChatDB();
        mymatch_profileDB db = new mymatch_profileDB();
        //
        // GET: /message/

        [HttpPost]
        public ActionResult messagesending(string msg_body, string msg_receiver)
        {
            if (!string.IsNullOrEmpty(msg_body))
            {
                message msg = new message();
                msg.username = User.Identity.Name;
                msg.msg_sender = User.Identity.Name;
                msg.msg_receiver = msg_receiver + "+unread";
                msg.msg_date = DateTime.UtcNow;
                msg.msg_body = msg_body;
                if (ModelState.IsValid)
                {
                    db.messages.Add(msg);
                    db.SaveChanges();
                    Thread.Sleep(1000);
                    return RedirectToAction("user_profile_Index", "user_profile", new { user_name = msg_receiver });
                }
            }
            return RedirectToAction("user_profile_Index", "user_profile", new { user_name = msg_receiver });
        }

        public ActionResult message_inbox()
        {

            int page_number = 1;

            var v = db.messages.OrderByDescending(r => r.msg_date).ToList();
            string u = User.Identity.Name;
            List<message> li = new List<message>();
            foreach (var k in v)
            {
                if (k.msg_receiver.Split('+').First() == u)
                {
                    li.Add(k);
                }
            }
            var v1 = li.Where(r => r.msg_receiver.Split('+').Last() == "unread").ToList();
            foreach (var x in v1)
            {
                string s = x.msg_receiver.Split('+').First();
                x.msg_receiver = s + "+read";
                db.Entry(x).State = EntityState.Modified;
            }
            db.SaveChanges();
            //paging
            ViewBag.total_message = li.Count();
            decimal total_message = v.Count();
            ViewBag.total_page = Math.Ceiling(total_message / 20);
            ViewBag.page_number = page_number;
            var v2 = v.Skip((page_number - 1) * 20).Take(20);
            //
            return View(li);
        }

        [HttpPost]
        public ActionResult message_inbox(int page_number)
        {

            var v = db.messages.OrderByDescending(r => r.msg_date).ToList();
            string u = User.Identity.Name;
            List<message> li = new List<message>();
            foreach (var k in v)
            {
                if (k.msg_receiver.Split('+').First() == u)
                {
                    li.Add(k);
                }
            }
            var v1 = li.Where(r => r.msg_receiver.Split('+').Last() == "unread").ToList();
            foreach (var x in v1)
            {
                string s = x.msg_receiver.Split('+').First();
                x.msg_receiver = s + "+read";
                db.Entry(x).State = EntityState.Modified;
            }
            db.SaveChanges();
            //paging
            ViewBag.total_message = li.Count();
            decimal total_message = v.Count();
            ViewBag.total_page = Math.Ceiling(total_message / 20);
            ViewBag.page_number = page_number;
            var v2 = v.Skip((page_number - 1) * 20).Take(20);
            //
            return View(li);
        }

        public ActionResult message_sent()
        {
            int page_number = 1;
            var v = db.messages.OrderByDescending(r => r.msg_date).Where(r => r.msg_sender == User.Identity.Name).ToList();
            //paging
            ViewBag.total_message = v.Count();
            decimal total_message = v.Count();
            ViewBag.total_page = Math.Ceiling(total_message / 20);
            ViewBag.page_number = page_number;
            var v1 = v.Skip((page_number - 1) * 20).Take(20);
            //
            return View(v1);
        }

        [HttpPost]
        public ActionResult message_sent(int page_number)
        {
            var v = db.messages.OrderByDescending(r => r.msg_date).Where(r => r.msg_sender == User.Identity.Name).ToList();
            //paging
            ViewBag.total_message = v.Count();
            decimal total_message = v.Count();
            ViewBag.total_page = Math.Ceiling(total_message / 20);
            ViewBag.page_number = page_number;
            var v1 = v.Skip((page_number - 1) * 20).Take(20);
            //
            return View(v1);
        }
        //delete message
        public ActionResult del_message(int id, int page_number)
        {
            var v = db.messages.Find(id);
            db.messages.Remove(v);
            db.SaveChanges();
            return RedirectToAction("message_sent", new { page_number = page_number });

        }

        //delete sent message
        public ActionResult del_sent_message(int id, int page_number)
        {
            var v = db.messages.Find(id);
            db.messages.Remove(v);
            db.SaveChanges();
            return RedirectToAction("message_inbox", new { page_number = page_number });

        }

        //show message

        public ActionResult message_show(int id)
        {
            var x = db.messages.Find(id);
            x.msg_receiver = x.msg_receiver.Split('+').First();
            return PartialView(x);
        }

        public ActionResult message_sent_show(int id)
        {
            var x = db.messages.Find(id);
            x.msg_receiver = x.msg_receiver.Split('+').First();
            return PartialView(x);
        }
        public ActionResult message_partial()
        {
            string u = User.Identity.Name;
            var v = db.messages.OrderByDescending(r => r.msg_date).ToList();
            List<message> li = new List<message>();
            foreach (var k in v)
            {
                if (k.msg_receiver.Split('+').First() == u)
                {
                    li.Add(k);
                }
            }
            var v1 = li.Where(r => r.msg_receiver.Split('+').Last() == "unread").ToList();
            ViewBag.total = v1.Count();
            var v2 = li.Take(5);
            return PartialView(v2);
        }

        public ActionResult chat_history()
        {
            //int page_number = 1;
            string s = User.Identity.Name;
            var v = db1.chatmessages.Where(r => r.from == s).OrderByDescending(r => r.dt).ToList();
            var v1=v.Select(r=>r.to).Distinct();
            List<ChatMessage> li = new List<ChatMessage>();
            foreach (string x in v1)
            {
                var p = v.Where(r => r.to == x).FirstOrDefault();
                li.Add(p);
            }
            

            return View(li);
        }

        //chat details
        public ActionResult Details_chat(long id)
        {
            var v = db1.chatmessages.Find(id);
            var to = v.to;
            var from = v.from;
            var x = db1.chatmessages.Where(r => r.from == from).OrderByDescending(r => r.dt).ToList();
            var x1 = x.Where(r => r.to == to).ToList();
            var y = db1.chatmessages.Where(r => r.from == to).OrderByDescending(r => r.dt).ToList();
            var y1 = y.Where(r => r.to == from).ToList();
            IList<ChatMessage> li = new List<ChatMessage>();
            foreach (var a in x1) {
                li.Add(a);
            }
            foreach (var b in y1) {
                li.Add(b);
            }
            IList<ChatMessage> li1 = new List<ChatMessage>();
            li1 = li.OrderByDescending(r => r.dt).ToList();
            return PartialView(li1);

        }

        //delete chat history
        public ActionResult Delete_chat(long id) {
            var v = db1.chatmessages.Find(id);
            var from = v.from;
            var to = v.to;
            var x = db1.chatmessages.Where(r => r.from == from).ToList();
            var x1 = x.Where(r => r.to == to).ToList();
            foreach (var k in x1) {
                db1.chatmessages.Remove(k);
            }
            db1.SaveChanges();
            return RedirectToAction("chat_history");
        }
    }

}
