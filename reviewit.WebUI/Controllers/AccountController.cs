using reviewIt.Core.Services;
using reviewIt.Core.ViewModels;
using reviewIt.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace reviewIt.WebUI.Controllers
{

    public class AccountController : Controller
    {
       // private MailController mail = new MailController();
        private BusinessProfileService businessProfileService = new BusinessProfileService();
        private UserProfileController userController = new UserProfileController();
        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                MembershipUser user = null;
                string userName;
                
                if (model.UserName.Contains("@"))
                {
                    // Could change model.userName from email to username but this will change form values
                    //string receiverMail, mailSubject,mailBody;
                    //receiverMail = model.UserName;
                    //mailSubject = " LogIn by User in ReviewIt.com";
                    //mailBody = "User attempt to login to reviewit.com by email";
                    //mail.sendMail(receiverMail, mailSubject, mailBody);
                    // if login fails (wrong pwd) which will be confusing to users, so keep in sep username string                                       
                   
                    userName = Membership.GetUserNameByEmail(model.UserName);
                    if (!String.IsNullOrEmpty(userName))
                    {
                        user = Membership.GetUser(userName);
                    }
                }
                else
                {

                    user = Membership.GetUser(model.UserName);
                    userName = model.UserName;
                }


                if (Membership.ValidateUser(userName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(userName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }                  

                    else
                    {
                        if( Roles.IsUserInRole("Admin"))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        return RedirectToAction("IndexforAll", "Advertisement");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            var model = new RegisterModel
            {
                //Email = data.Email,
                Role = "User"

            };
            return View(model);
        }

        public ActionResult BusinessRegister( int id)
        {
            
            BusinessProfileViewModel data = businessProfileService.GetRequestedBusniessbyId(id);            
           
            //RegisterModel model;
            var model = new RegisterModel
            {
                UserName = data.BusinessName,
                Email = data.Email,
                Role = "BusinessOwner"
                
            };
            return View(model);
        }
        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;

                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    if (model.Role == "BusinessOwner")
                    {
                        Roles.AddUserToRole(model.UserName, "Business Owner");
                    }
                    else
                    {
                        Roles.AddUserToRole(model.UserName, "User");
                        var data = new UserProfileViewModel
                        {
                            Name = model.UserName,
                            Email = model.Email,
                            ImageName = "Default.png",
                        };
                        userController.Create(data);

                    }
                    return RedirectToAction("IndexforAll", "Advertisement");


                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult BusinessView()
        {
            return View();
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
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
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
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
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please Home your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please Home your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please Home your system administrator.";
            }
        }
        #endregion
    }
}
