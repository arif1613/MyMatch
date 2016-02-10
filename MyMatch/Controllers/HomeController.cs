using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using Microsoft.AspNet.SignalR;
using MyMatch.Models;
using MyMatch.Models.chat;
using MyMatch.Models.payment;
using MyMatch.Models.verification;

namespace MyMatch.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        mymatch_profileDB db = new mymatch_profileDB();
        mymatch_ChatDB db1 = new mymatch_ChatDB();
        MyMatch.Models.verification.veificationDB db3 = new veificationDB();

        public ActionResult sitemap()
        {
            return View();
        }

        public ActionResult index_intro()
        {
            return PartialView();
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Index()
        {
            var v = db.show_interests.Select(r => r.username).ToList();
            ViewBag.name = v;
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "myprofile");
            }
            else
            {
                return View(v);
            }

        }


        public ActionResult Chat()
        {
            string s = User.Identity.Name;

            var s1 = db1.ousers.Where(r => r.ouser == s).ToList();
            if (s1.Count() > 0)
            {
                ViewBag.chat_status = "online";
            }
            else
            {
                ViewBag.chat_status = "offline";
            }


            //total user in chat room
            var v = db.show_interests.Where(r => r.username == s).Select(r => r.interested_people).ToList();
            var v1 = db.show_interests.Where(r => r.interested_people == s).Select(r => r.username).ToList();
            List<string> user_list = new List<string>();
            foreach (string z in v)
            {
                user_list.Add(z);
            }
            foreach (string z1 in v1)
            {
                user_list.Add(z1);
            }

            user_list = user_list.Distinct().ToList();

            //online user count for premium user
            membership_check mc = new membership_check();
            string status = mc.member_status(s);

            List<string> online_user_count = new List<string>();
            var x = db1.ousers.Select(r => r.ouser).ToList();

            if (status == "Premium")
            {
                foreach (string k in user_list)
                {
                    foreach (string k1 in x)
                    {
                        if (k == k1)
                        {
                            online_user_count.Add(k1);
                        }
                    }
                }
            }
            else
            {
                foreach (string k in v)
                {
                    foreach (string k1 in x)
                    {
                        if (k == k1)
                        {
                            online_user_count.Add(k1);
                        }
                    }
                }

            }
            ViewBag.total_user = user_list;
            ViewBag.onlined = online_user_count;
            ViewBag.count = online_user_count.Count();

            //chat history
            string to;
            var h = db1.chatmessages.Where(r => r.from == s).OrderByDescending(r => r.dt).ToList();

            if (h.Count() > 0)
            {
                to = h.FirstOrDefault().to;
                ViewBag.receiver = to;
            }
            else
            {
                to = "You are connected as " + s;
            }
            var h1 = h.Where(r => r.to == to).Take(3).ToList();
            var m = db1.chatmessages.Where(r => r.from == to).OrderByDescending(r => r.dt).ToList();
            var m1 = m.Where(r => r.to == s).Take(3).ToList();

            List<ChatMessage> mli = new List<ChatMessage>();

            foreach (var g in h1)
            {
                mli.Add(g);
            }
            foreach (var g1 in m1)
            {
                mli.Add(g1);
            }
            List<ChatMessage> mli1 = new List<ChatMessage>();
            mli1 = mli.OrderBy(r => r.dt).ToList();

            List<string> li1 = new List<string>();
            foreach (var x1 in mli1)
            {
                if (x1.from == s)
                {
                    string p = string.Format("{0}: {1}", x1.from, x1.msg);

                    li1.Add(p);
                }
                else
                {
                    string p1 = string.Format("{0}: {1}", x1.from, x1.msg);

                    li1.Add(p1);
                }

            }

            ViewBag.chat_history = li1;

            Thread.Sleep(1000);
            return PartialView();


        }
        public ActionResult Chat_normal()
        {
            membership_check mc = new membership_check();
            string status = mc.member_status(User.Identity.Name);
            if (status == "Normal")
            {
                return PartialView();
            }
            else
            {
                return null;
            }

        }
        public ActionResult password_retrival()
        {
            Thread.Sleep(500);
            return PartialView();
        }
        [HttpPost]
        public ActionResult password_retrival(string email)
        {
            var v = db.logininfos.Where(r => r.emailaddress == email).ToList();
            if (v.Count() > 0)
            {
                //send mail
                MailMessage message = new MailMessage();
                message.From = new MailAddress("info@indianbibah.com");
                message.To.Add(email);
                const string fromPassword = "aa1613++";
                message.Subject = "indianbibah.com login informations";
                message.Body = string.Format("Your indianbibah.com login informations are:" + "<br />" + "<a style=" + "color:maroon" + ">" +
                    "Username:  " + "</a>" + "{0}" + "<br />" + "<a style=" + "color:maroon" + ">" + "Password:  " + "</a>" + "{1}",
                v.FirstOrDefault().username, v.FirstOrDefault().password);

                message.Body += "<br /><br /><br /><br />" + "<p style=" + "color:gray" + ">" +
                    "This is an automatic generated mail. Please do not reply to this mail." + "</p>";
                message.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient
                {
                    Host = "mail.indianbibah.com",
                    Port = 587,
                    EnableSsl = false,
                    Credentials = new NetworkCredential("info@indianbibah.com", fromPassword)
                };

                smtp.Send(message);
                Thread.Sleep(500);
                return Content("Your login info has been sent to your e-mail address.");

            }
            else
            {

                Thread.Sleep(500);
                return Content("You are not registered in our database. Please register below.");
            }
        }

        //payment
        [HttpPost]
        public ActionResult payment1()
        {
            string s = User.Identity.Name;
            string url = paypal.paypal_pay(10, s);
            string email = db.logininfos.Where(r => r.username == s).FirstOrDefault().emailaddress;
            url += "&email=" + email;
            return Redirect(url);
            //Response.Redirect(url);              

        }
        [HttpPost]
        public ActionResult payment2()
        {
            string s = User.Identity.Name;
            string url = paypal.paypal_pay(25, s);
            string email = db.logininfos.Where(r => r.username == s).FirstOrDefault().emailaddress;
            url += "&email=" + email;
            return Redirect(url);
            //Response.Redirect(url);              

        }

        [AllowAnonymous]
        public ActionResult Register1()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register1(RegisterModel model, HttpPostedFileBase picture_file1, HttpPostedFileBase picture_file2)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);
                // Attempt to register the user
                if (createStatus == MembershipCreateStatus.Success)
                {
                    primary_registration pr = new primary_registration();
                    pr.primaryprofile(model);

                    if (picture_file1 != null && picture_file1.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(picture_file1.FileName);
                        string x1 = model.UserName;
                        string newfilename = x1 + "..1.png";
                        var file_path1 = Path.Combine(Server.MapPath("~/Images/Pictures"), newfilename);
                        picture_file1.SaveAs(file_path1);
                    }

                    if (picture_file2 != null && picture_file2.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(picture_file2.FileName);
                        string x1 = model.UserName;
                        string newfilename = x1 + "..2.png";
                        var file_path2 = Path.Combine(Server.MapPath("~/Images/Pictures"), newfilename);
                        picture_file2.SaveAs(file_path2);
                    }

                    FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: true);

                    //send mail
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("registration@indianbibah.com");
                    // any address where the email will be sending
                    message.To.Add(model.email);
                    //Password of your gmail address
                    const string fromPassword = "reg1613++";
                    // Passing the values and make a email formate to display
                    message.Subject = "Welcome to IndianBibah";
                    message.Body = string.Format("Hello {0}. Welcome to IndianBibah.com. Your IndianBibah.com login informations are:" + "<br />" + "<a style=" + "color:maroon" + ">" +
                    "Username:  " + "</a>" + "{1}" + "<br />" + "<a style=" + "color:maroon" + ">" + "Password:  " + "</a>" + "{2}",
                        model.UserName, model.UserName, model.Password);

                    message.Body += "<br /><br /><br /><br />" + "<p style=" + "color:gray" + ">" +
                        "This is an automatic generated mail. Please do not reply to this mail." + "</p>";
                    message.IsBodyHtml = true;

                    // smtp settings
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "mail.indianbibah.com",
                        Port = 587,
                        EnableSsl = false,
                        //DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                        Credentials = new NetworkCredential("registration@indianbibah.com", fromPassword)
                        //  smtp.Timeout = 20000;

                    };

                    smtp.Send(message);
                    Thread.Sleep(1000);
                    return RedirectToAction("Index", "myprofile");

                }
                else
                {
                    ModelState.AddModelError("register_error", ErrorCodeToString(createStatus));
                }
            }

            return RedirectToAction("Index", "myprofile");
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        public ActionResult our_price()
        {
            Thread.Sleep(1500);
            return PartialView();
        }

        public ActionResult price_comparison() {
            Thread.Sleep(1500);
            return PartialView();        
        }

        public ActionResult feature_comparison() {
            Thread.Sleep(1500);
            return PartialView();
        }
    }
}
