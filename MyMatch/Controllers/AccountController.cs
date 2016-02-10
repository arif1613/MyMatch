using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyMatch.Models;
using System.Threading;
using System.IO;
using System.Data.Entity;
using MyMatch.Models.chat;
using System.Net.Mail;
using System.Net;
using MyMatch.Models.payment;
using MyMatch.Models.others;

namespace MyMatch.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login
        mymatch_profileDB db = new mymatch_profileDB();
        mymatch_ChatDB db1 = new mymatch_ChatDB();
        [AllowAnonymous]
        public ActionResult Login()
        {
            return PartialView();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    membership_check mc = new membership_check();
                    mc.change_status(model.UserName);
                    Thread.Sleep(500);
                    return RedirectToAction("Index", "myprofile");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("login_error", model);
        }

        public ActionResult login_error(LoginModel lm)
        {
            return View();
        }
        //
        // GET: /Account/LogOff
        [Authorize]
        public ActionResult LogOff()
        {
            var v = db1.ousers.Where(r => r.ouser == User.Identity.Name).ToList();
            if (v.Count() > 0)
            {
                db1.ousers.Remove(v.FirstOrDefault());
            }
            db1.SaveChanges();
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return PartialView();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model, HttpPostedFileBase picture_file1, HttpPostedFileBase picture_file2)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);
                
                if (createStatus == MembershipCreateStatus.Success)
                {
                    primary_registration pr = new primary_registration();

                    //Adding datas to database
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
                    message.Body = string.Format("Hello {0}, Welcome to IndianBibah.com. Your IndianBibah.com login informations are:" + "<br />" + "<a style=" + "color:maroon" + ">" +
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

        public ActionResult Register_error(RegisterModel rm)
        {

            return View(rm);
        }

        //
        // POST: /Account/Disassociate



        //
        // GET: /Account/ChangePassword

        public ActionResult ChangePassword()
        {
            return PartialView();
        }

        //
        // POST: /Account/ChangePassword

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, userIsOnline: true);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    var x = db.logininfos
                        .Where(r => r.username == User.Identity.Name).FirstOrDefault();
                    x.password = model.NewPassword;
                    db.Entry(x).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "myprofile");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            return View(model);
        }



        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
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
        #endregion
    }
}
