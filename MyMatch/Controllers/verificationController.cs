using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMatch.Models.verification;
using MyMatch.Models;
using System.Data.Entity;
using System.Net.Mail;
using System.Net;
using System.Threading;
using Twilio;

namespace MyMatch.Controllers
{
    public class verificationController : Controller
    {
        //
        // GET: /verification/

        veificationDB db3 = new veificationDB();
        mymatch_profileDB db = new mymatch_profileDB();

        public ActionResult email_ver_Index()
        {
            Random rnd = new Random();
            int rand = rnd.Next(1111, 9999);

            var v = db.contactinfos.Where(r => r.username == User.Identity.Name).FirstOrDefault();
            string email = v.email.Split('+').Last();
            if (v.email.Split('+').First() == "V")
            {
                return Content("E-mail address is already verified.");
            }
            else
            {
                if (email.Contains('@'))
                {


                    //store in DB
                    email_ver_temp evt = new email_ver_temp();
                    evt.username_temp = User.Identity.Name;
                    evt.evdate_temp = DateTime.UtcNow;
                    evt.evcode_temp = rand;
                    evt.email_temp = email.Split('+').Last();
                    var x = db3.email_temp.Where(r => r.username_temp == User.Identity.Name).ToList();
                    if (x.Count() > 0)
                    {
                        x.FirstOrDefault().evcode_temp = rand;
                        x.FirstOrDefault().ID = x.FirstOrDefault().ID;
                        db3.Entry(x.FirstOrDefault()).State = EntityState.Modified;
                        db3.SaveChanges();
                    }
                    else
                    {
                        db3.email_temp.Add(evt);
                        db3.SaveChanges();
                    }

                    //sending verification code to email
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("verification@indianbibah.com");
                    // any address where the email will be sending
                    message.To.Add(email);
                    const string fromPassword = "ver1613++";
                    message.Subject = "IndianBibah e-mail verification code";
                    message.Body = string.Format("Hello {0}, Welcome to IndianBibah.com. Your e-mail verification code is:"
                        + "<a style=" + "color:maroon" + ">" + "{1}" + "</a>" + "<br />", User.Identity.Name, rand);

                    message.IsBodyHtml = true;

                    // smtp settings
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "mail.indianbibah.com",
                        Port = 587,
                        EnableSsl = false,
                        //DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                        Credentials = new NetworkCredential("verification@indianbibah.com", fromPassword),
                        Timeout = 20000

                    };
                    try
                    {
                        smtp.Send(message);
                        Thread.Sleep(5000);
                        return PartialView();
                    }
                    catch
                    {
                        return Content("E-mail address is not valid");
                    }
                }
                else
                {
                    return Content("E-mail address is not valid");
                }
            }
        }

        [HttpPost]
        public ActionResult email_ver_result(int e_code)
        {

            string s = User.Identity.Name;
            int code = new int();
            var v = db3.email_temp.Where(r => r.username_temp == s).ToList();
            if (v.Count() > 0)
            {
                code = v.FirstOrDefault().evcode_temp;
                db3.email_temp.Remove(v.FirstOrDefault());
            }
            if (code == e_code)
            {
                email_ver_main evm = new email_ver_main();
                evm.username_main = s;
                evm.evdate_main = DateTime.UtcNow;
                evm.evcode_main = e_code;
                evm.email_main = v.FirstOrDefault().email_temp;
                var x = db3.email_main.Where(r => r.username_main == s).ToList();
                if (x.Count() > 0)
                {
                    x.FirstOrDefault().evcode_main = code;
                    x.FirstOrDefault().ID = x.FirstOrDefault().ID;
                    db3.Entry(x.FirstOrDefault()).State = EntityState.Modified;
                }
                else
                {
                    db3.email_main.Add(evm);
                }
                db3.SaveChanges();

                //update contact info
                var v1 = db.contactinfos.Where(r => r.username == s).FirstOrDefault();
                v1.email = "V+" + v1.email.Split('+').Last();
                v1.ID = v1.ID;
                db.Entry(v1).State = EntityState.Modified;
                db.SaveChanges();
                Thread.Sleep(1000);
                return Content("Your e-mail address is verified. Refresh page to use updated features.");
            }
            else
            {
                Thread.Sleep(1000);
                return Content("Codes don't match. Please verify again.");
            }
        }

        public ActionResult mobile_ver_Index()
        {
            Random rnd = new Random();
            int mob_rand = rnd.Next(1111, 9999);
            string s = User.Identity.Name;
            var v = db3.mobile_temp.Where(r => r.musername_temp == s).ToList();
            var x = db.contactinfos.Where(r => r.username == s).FirstOrDefault();
            string mobile_number = x.contactnumber.Split(',').Last();
            if (mobile_number == "V")
            {
                return Content("Mobile number is already verified");
            }
            else
            {
                if (v.Count() > 0)
                {
                    DateTime dt = v.FirstOrDefault().mvdate_temp;
                    if (DateTime.UtcNow.Date > dt.Date)
                    {
                        mobile_ver_temp mvt = new mobile_ver_temp();
                        mvt.mvdate_temp = DateTime.UtcNow;
                        mvt.mvcode_temp = mob_rand;
                        mvt.musername_temp = s;
                        mvt.mobilenumber_temp = mobile_number;
                        mvt.ID = v.FirstOrDefault().ID;
                        db3.Entry(v.FirstOrDefault()).State = EntityState.Modified;
                        db3.SaveChanges();
                        //sending sms
                        string AccountSid = "ACc2f6e22c7bc2e5ed8c45ec02533a2b27";
                        string AuthToken = "776186a13eb53624565e301468c1e140";
                        var twilio = new TwilioRestClient(AccountSid, AuthToken);
                        twilio.SendSmsMessage("+14172384011", mobile_number, string.Format("Your IndianBibah Verification code is:{0}", mob_rand));
                        Thread.Sleep(1500);

                        return PartialView();

                    }
                    else
                    {
                        ViewBag.sent = "yes";
                        return PartialView();
                    }
                }
                else
                {
                    if (mobile_number.StartsWith("+"))
                    {
                        mobile_ver_temp mvt = new mobile_ver_temp();
                        mvt.mvdate_temp = DateTime.UtcNow;
                        mvt.mvcode_temp = mob_rand;
                        mvt.musername_temp = s;
                        mvt.mobilenumber_temp = mobile_number;
                        db3.mobile_temp.Add(mvt);
                        db3.SaveChanges();

                        //sending sms
                        string AccountSid = "ACc2f6e22c7bc2e5ed8c45ec02533a2b27";
                        string AuthToken = "776186a13eb53624565e301468c1e140";
                        var twilio = new TwilioRestClient(AccountSid, AuthToken);
                        twilio.SendSmsMessage("+14172384011", mobile_number, string.Format("Your IndianBibah Verification code is:{0}", mob_rand));
                        Thread.Sleep(1500);

                        return PartialView();
                    }
                    else
                    {
                        return Content("Mobile number is not in actual format.");
                    }

                }

            }
        }

        [HttpPost]
        public ActionResult mobile_ver_result(int m_code)
        {
            string s = User.Identity.Name;
            var v = db3.mobile_temp.Where(r => r.musername_temp == s).ToList();
            if (v.Count > 0)
            {
                if (v.FirstOrDefault().mvcode_temp == m_code)
                {
                    foreach (var t in v)
                    {
                        db3.mobile_temp.Remove(t);
                    }

                    mobile_ver_main mvm = new mobile_ver_main();
                    var x = db3.mobile_main.Where(r => r.musername_main == s).ToList();
                    if (x.Count() > 0)
                    {
                        x.FirstOrDefault().mvcode_main = m_code;
                        x.FirstOrDefault().ID = x.FirstOrDefault().ID;
                        db3.Entry(x.FirstOrDefault()).State = EntityState.Modified;
                        db3.SaveChanges();
                    }
                    else
                    {
                        mvm.mvdate_main = DateTime.UtcNow;
                        mvm.mvcode_main = m_code;
                        mvm.musername_main = s;
                        mvm.mobilenumber_main = v.FirstOrDefault().mobilenumber_temp;
                        db3.mobile_main.Add(mvm);
                    }

                    var x1 = db.contactinfos.Where(r => r.username == s).FirstOrDefault();
                    string mobile = x1.contactnumber.Split(',').Last();
                    x1.contactnumber = "V," + mobile;
                    x1.ID = x1.ID;
                    db.Entry(x1).State = EntityState.Modified;
                    db.SaveChanges();                    
                    db3.SaveChanges();

                    return Content("Your mobile number is verified. Refresh this page to use our features.");
                }
                else
                {
                    return Content("Codes don't match. Please try again.");
                }
            }

            return Content("Your mobile number is not valid. Please try again later.");

        }
    }
}
