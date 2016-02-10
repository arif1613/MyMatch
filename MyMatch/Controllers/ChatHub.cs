using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using MyMatch.Models;
using MyMatch.Models.chat;
using MyMatch.Models.payment;

namespace MyMatch.Controllers
{
    public class ChatHub : Hub
    {

        mymatch_profileDB db = new mymatch_profileDB();
        mymatch_ChatDB db1 = new mymatch_ChatDB();
        List<string> li = new List<string>();

        public void Connect(string name)
        {
            var v = db1.ousers.Where(r => r.ouser == name).ToList();
            online_user ou = new online_user();
            if (v.Count() > 0)
            {
                v.FirstOrDefault().ouser = name;
                db1.Entry(v.FirstOrDefault()).State = EntityState.Modified;
            }
            else
            {
                ou.ouser = name;
                db1.ousers.Add(ou);
            }
            db1.SaveChanges();


            Clients.All.onConnected(name);
            //Clients.Caller.onConnected1(name, li);

        }
        public void Connect1(string name)
        {
            var p = db.show_interests.Where(r => r.username == name).Select(r => r.interested_people).Distinct().ToList();
            var x = db1.ousers.Select(r => r.ouser).Distinct().ToList();
            List<string> li = new List<string>();
            foreach (var i in p)
            {
                foreach (var j in x)
                {
                    if (j == i)
                    {
                        li.Add(j);
                    }
                }
            }
            membership_check mc = new membership_check();
            string status = mc.member_status(name);
            if (status == "Premium")
            {
                var k = db.show_interests.Where(r => r.interested_people == name).Select(r => r.username).Distinct().ToList();

                foreach (var r in k)
                {
                    foreach (var l in x)
                    {
                        if (r == l)
                        {
                            li.Add(l);
                        }
                    }
                }
            }

            //   Clients.All.onConnected(name);
            Clients.Caller.onConnected1(name, li);

        }
        public void Send(string name, string message)
        {
            string time = DateTime.UtcNow.ToString("(dd,MMM  HH:mm)");
            Clients.All.addNewMessageToPage(name, time, message);
        }


        public void Disconnect(string name)
        {
            var v = db1.ousers.Where(r => r.ouser == name).FirstOrDefault();
            db1.ousers.Remove(v);
            db1.SaveChanges();
            Clients.All.onDisconnected1(name);
            Clients.Caller.onDisconnected2();

        }

        public void Showwindow(string u, string s)
        {

            var v = db1.chatmessages.Where(r => r.from == u).OrderByDescending(r => r.dt).ToList();
            var v1 = v.Where(r => r.to == s).Take(3).ToList();
            var m = db1.chatmessages.Where(r => r.from == s).OrderByDescending(r => r.dt).ToList();
            var m1 = m.Where(r => r.to == u).Take(3).ToList();

            List<ChatMessage> mli = new List<ChatMessage>();

            foreach (var g in v1)
            {
                mli.Add(g);
            }
            foreach (var g1 in m1)
            {
                mli.Add(g1);
            }
            List<ChatMessage> mli1 = new List<ChatMessage>();
            mli1 = mli.OrderBy(r => r.dt).ToList();

            List<string> li = new List<string>();
            foreach (var x in mli1)
            {
                if (x.from == u)
                {
                    string p = string.Format("{0}({1}):  {2}", x.from, x.dt.ToString("dd,MMM hh:mm"), x.msg);

                    li.Add(p);
                }
                else
                {
                    string p1 = string.Format("{0}({1}):  {2}", x.from, x.dt.ToString("dd,MMM hh:mm"), x.msg);

                    li.Add(p1);
                }

            }
            Clients.Caller.primessage(s, li);//show chat window to caller
        }

        public void PrivateMessage(string u, string s, string msg)
        {
            ChatMessage ms = new ChatMessage();

            ms.from = u;
            ms.to = s;
            ms.msg = msg;
            ms.dt = DateTime.Now;
            db1.chatmessages.Add(ms);
            db1.SaveChanges();
            var v = db1.chatmessages.Where(r => r.from == u).OrderByDescending(r => r.dt).ToList();
            var v1 = v.Where(r => r.to == s).Take(3).ToList();
            var m = db1.chatmessages.Where(r => r.from == s).OrderByDescending(r => r.dt).ToList();
            var m1 = m.Where(r => r.to == u).Take(3).ToList();

            List<ChatMessage> mli = new List<ChatMessage>();
            foreach (var g in v1)
            {
                mli.Add(g);
            }
            foreach (var g1 in m1)
            {
                mli.Add(g1);
            }
            List<ChatMessage> mli1 = new List<ChatMessage>();
            mli1 = mli.OrderBy(r => r.dt).ToList();
            List<ChatMessage> mli2 = new List<ChatMessage>();
            mli2 = mli1.Where(r => r.dt.Day == DateTime.UtcNow.Day).ToList();
            List<string> li = new List<string>();
            foreach (var x in mli2)
            {
                if (x.from == u)
                {
                    string p = string.Format("{0}({1}):  {2}", x.from, x.dt.ToString("dd,MMM hh:mm"), x.msg);

                    li.Add(p);
                }
                else
                {
                    string p1 = string.Format("{0}({1}):  {2}", x.from, x.dt.ToString("dd,MMM hh:mm"), x.msg);

                    li.Add(p1);
                }

            }
            Clients.All.show_pri_win(u, s, msg);//show window to selected user
            Clients.Caller.privatemsg(u, s, msg);

            //Clients.Caller.privatemsg1(name, msg);
        }

        //public void list_hover(string username,string client_name)
        //{           
        //   var v=db1.     
        //}
    }
}
