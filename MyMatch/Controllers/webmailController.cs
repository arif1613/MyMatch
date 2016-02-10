using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using MyMatch.Models;

namespace MyMatch.Controllers.webmail
{

    [Authorize(Roles = "aa1613")]
    public class webmailController : Controller
    {
        //
        // GET: /webmail/
        mymatch_profileDB db = new mymatch_profileDB();

        public ActionResult webmail_Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult send_message(string subject, string msg)
        {
            //send mail
            var v = db.logininfos.Select(r => r.emailaddress).ToList();
            // smtp settings
            foreach (var e in v)
            {
                //send mail
                MailMessage message = new MailMessage();
                var fromAddress = "info@indianbibah.com";
                // any address where the email will be sending
                var toAddress = e;
                //Password of your gmail address
                const string fromPassword = "aa1613++";
                // Passing the values and make a email formate to display
                string sub = subject;
                string body = msg;
                // smtp settings
                SmtpClient smtp = new SmtpClient
                {
                    Host = "mail.indianbibah.com",
                    Port = 587,
                    EnableSsl = false,
                    //DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    Credentials = new NetworkCredential(fromAddress, fromPassword)
                };
                try
                {
                    smtp.Send(fromAddress, toAddress, sub, body);
                }
                catch
                {
                }
            }
            Thread.Sleep(1000);
            return RedirectToAction("webmail_Index");
        }

        [HttpPost]
        public ActionResult send_mail(string addresses, string subject, string msg)
        {

            var v = addresses.Split(',').ToList();

            foreach (var e in v)
            {        //send mail
                MailMessage message = new MailMessage();
                var fromAddress = "info@indianbibah.com";
                // any address where the email will be sending
                var toAddress = e;
                //Password of your gmail address
                const string fromPassword = "aa1613++";
                // Passing the values and make a email formate to display
                string sub = subject;
                string body = msg;
                // smtp settings
                SmtpClient smtp = new SmtpClient
                {
                    Host = "mail.indianbibah.com",
                    Port = 587,
                    EnableSsl = false,
                    //DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    Credentials = new NetworkCredential(fromAddress, fromPassword)
                };
                try
                {
                    smtp.Send(fromAddress, toAddress, sub, body);
                }
                catch
                {
                }
            }
            Thread.Sleep(1000);
            return RedirectToAction("webmail_Index");

        }

    }
}
