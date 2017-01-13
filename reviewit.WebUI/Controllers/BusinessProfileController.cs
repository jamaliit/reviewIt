using reviewIt.Core.Services;
using reviewIt.Core.ViewModels;
using reviewIt.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        private AdvertisementService advertisementService = new AdvertisementService();
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

        public ActionResult PartialViewBusinessandAdvertisement(string user)
        {

            var BusinessandAdvertisementViewModel = new BusinessandAdvertisementViewModel();
            BusinessandAdvertisementViewModel.advertisement = advertisementService.GetAllAdvertisement(user).OrderByDescending(x =>x.CreatedDate);
            BusinessandAdvertisementViewModel.business = businessProfileService.GetOwnBusinessProfileByID(user);
            return View(BusinessandAdvertisementViewModel);
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

            if (ModelState.IsValid)
            {
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
            else
            {
                businessProfileService.CreateBusinessprofile(businessProfileVM);
                return RedirectToAction("BusinessView", "Account");
            }
        }
            else
            {
                ViewBag.businessCategoryList = commonController.GetAllBusinessCategory();
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.businessCategoryList = commonController.GetAllBusinessCategory();
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
            if (ModelState.IsValid)
            {
                var fileName = "";
                if (businessProfileVM.ImageName == null)
                {

                    if (businessProfileVM.File == null)
                    {
                        fileName = "Default.png";

                    }
                    else
                    {

                        fileName = Path.GetFileName(businessProfileVM.File.FileName);
                        var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                        businessProfileVM.File.SaveAs(path);
                    }
                }
                else
                {
                    fileName = businessProfileVM.ImageName;
                }              
                
                businessProfileService.UpdateBusinessProfile(businessProfileVM,fileName);
                return RedirectToAction("IndividualBusinessProfile", "BusinessProfile", new { user = User.Identity.Name });
            }
            else
            {
                return RedirectToAction("UserOwnProfile", "UserProfile", new { user = User.Identity.Name });
            }
        }

        public ActionResult UploadProfilePicture(int id)
        {
            BusinessProfileViewModel business = businessProfileService.GetBusinessProfileByID(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
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

        [Authorize(Roles="Admin")]
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
            businessProfileService.UpdateBusinessProfile(businessProfiles, "Default.png");
            if (businessProfiles == null)
            {
                return HttpNotFound();
            }

            string receiverMail, mailSubject, mailBody;
            receiverMail = businessProfiles.Email;
            mailSubject = "reviewIt Registration";
            mailBody = "Dear" + @businessProfiles.UserName + ",<br/><br/> Your request has been accepted.Please complete your registration. Url: http://localhost:59080/Account/BusinessRegister/" + @Businessid + "<br/><br/> Regards <br/><br/> reviewIt Authority";
            // +@businessProfiles.BusinessName + " <br/><br/><br/> Password:" + @password + " <br/><br/><br/> You can log in by using your user name or your Email and given password. If you want to change your password , log in and then click on Change password.<br/><br/><br/> Regards <br/><br/><br/> reviewIt Authority. ";
            mail.sendMail(receiverMail, mailSubject, mailBody);
            return RedirectToAction("ApproveRequest");
        }     
    }
}