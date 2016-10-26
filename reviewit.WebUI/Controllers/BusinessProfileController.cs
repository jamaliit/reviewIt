using reviewIt.Core.Services;
using reviewIt.Core.ViewModels;
using reviewIt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace reviewIt.WebUI.Controllers
{
    public class BusinessProfileController : Controller
    {
       // private reviewItContext db; 
            
        private MailController mail = new MailController();
        private CommonController commonController = new CommonController();
        private BusinessProfileService businessProfileService = new BusinessProfileService();
        // GET: UserProfile
        public ActionResult Index()
        {
            IEnumerable<BusinessProfileViewModel> data = businessProfileService.GetAllBusinessprofile();
            return View(data);
        }

        public ActionResult IndividualBusinessProfile(string user)
        {           
            BusinessProfileViewModel businessProfiles = businessProfileService.GetOwnBusinessProfileByID(user);
            return View(businessProfiles);
        }



        public ActionResult Create()
        {
            return View();
        }
        public ActionResult RequestforBusinessAccount()
        {
            ViewBag.businessCategoryList = commonController.GetAllBusinessCategory();
            return View();
        }

        [HttpPost]
        public ActionResult RequestforBusinessAccount(BusinessProfileViewModel businessProfileVM)
        {
            var RedirectUrl = "/BusinessProfile/Index";
            bool isBusiness = false;
             if(Membership.FindUsersByName(businessProfileVM.BusinessName).Count == 1)
            {
                ModelState.AddModelError("", "Business name already exists");
                ViewBag.businessCategoryList = commonController.GetAllBusinessCategory();             
               return View(businessProfileVM);
            }
             else if (Membership.FindUsersByEmail(businessProfileVM.Email).Count == 1)
             {
                 ModelState.AddModelError("", "Email already exists");
                 ViewBag.businessCategoryList = commonController.GetAllBusinessCategory();
                 return View(businessProfileVM);
             }
             else  if (ModelState.IsValid)
            {
                businessProfileService.CreateBusinessprofile(businessProfileVM);
                return Json(RedirectUrl, JsonRequestBehavior.AllowGet);
            }
            else
            {
                RedirectUrl = null;
                return Json(RedirectUrl, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(int id)
        {
            BusinessProfileViewModel businessProfiles = businessProfileService.GetBusinessProfileByID(id);
            if (businessProfiles == null)
            {
                return HttpNotFound();
            }
            return View(businessProfiles);
        }

        [HttpPost]
        public ActionResult Edit(BusinessProfileViewModel businessProfileVM)
        {
            var RedirectUrl = "/BusinessProfile/IndividualBusinessProfile";
            if (ModelState.IsValid)
            {
                businessProfileService.UpdateBusinessProfile(businessProfileVM);
                return Json(RedirectUrl, JsonRequestBehavior.AllowGet);
            }
            else
            {
                RedirectUrl = null;
                return Json(RedirectUrl, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Details(int id)
        {
            BusinessProfileViewModel businessProfiles = businessProfileService.GetBusinessProfileByID(id);
            if (businessProfiles == null)
            {
                return HttpNotFound();
            }
            return View(businessProfiles);
        }

        public ActionResult Delete(int id)
        {
            BusinessProfileViewModel businessProfiles = businessProfileService.GetBusinessProfileByID(id);
            if (businessProfiles == null)
            {
                return HttpNotFound();
            }

            businessProfileService.DeleteBusinessprofile(id);
            return Json(Request.UrlReferrer.ToString());
        }
      

        //[HttpPost]
        //public ActionResult RequestforBusinessAccount()
        //{
        //    return View();
        //}

        public ActionResult ApproveRequest()
        {
            IEnumerable<BusinessProfileViewModel> data = businessProfileService.GetAllBusinessprofile();
            return View(data);
        }
       
        [HttpPost]
        public ActionResult ApproveRequest(int Businessid)
        {
            //MembershipCreateStatus createStatus;

            BusinessProfileViewModel businessProfiles = businessProfileService.GetRequestedBusniessbyId(Businessid);
            if (businessProfiles == null)
            {
                return HttpNotFound();
            }
           // string password = Membership.GeneratePassword(6, 1);

            // MembershipUser newUser = Membership.CreateUser(businessProfiles.BusinessName,password,businessProfiles.Email,null, null, true, null, out createStatus);

            //if (createStatus == MembershipCreateStatus.Success)
            //{
                string receiverMail, mailSubject, mailBody;
                receiverMail = businessProfiles.Email;
                mailSubject = "reviewIt Registration Complete";
                mailBody = "Url: http://localhost:59080/Account/BusinessRegister/" +@Businessid;
                // +@businessProfiles.BusinessName + " <br/><br/><br/> Password:" + @password + " <br/><br/><br/> You can log in by using your user name or your Email and given password. If you want to change your password , log in and then click on Change password.<br/><br/><br/> Regards <br/><br/><br/> reviewIt Authority. ";
                mail.sendMail(receiverMail, mailSubject, mailBody);
                return RedirectToAction("ApproveRequest");
            //}
            //else
            //{
            //    ViewBag.Error = ErrorCodeToString(createStatus);
            //}
            
            //return View("ApproveRequest");
        }

        //private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        //{
        //    switch (createStatus)
        //    {
        //        case MembershipCreateStatus.DuplicateUserName:
        //            return "User name already exists. Please enter a different user name.";

        //        case MembershipCreateStatus.DuplicateEmail:
        //            return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

        //        case MembershipCreateStatus.InvalidPassword:
        //            return "The password provided is invalid. Please enter a valid password value.";

        //        case MembershipCreateStatus.InvalidEmail:
        //            return "The e-mail address provided is invalid. Please check the value and try again.";

        //        case MembershipCreateStatus.InvalidAnswer:
        //            return "The password retrieval answer provided is invalid. Please check the value and try again.";

        //        case MembershipCreateStatus.InvalidQuestion:
        //            return "The password retrieval question provided is invalid. Please check the value and try again.";

        //        case MembershipCreateStatus.InvalidUserName:
        //            return "The user name provided is invalid. Please check the value and try again.";

        //        case MembershipCreateStatus.ProviderError:
        //            return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please Home your system administrator.";

        //        case MembershipCreateStatus.UserRejected:
        //            return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please Home your system administrator.";

        //        default:
        //            return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please Home your system administrator.";
        //    }
        //}
    }
}