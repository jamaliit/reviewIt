using reviewIt.Core.Services;
using reviewIt.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace reviewIt.WebUI.Controllers
{
    public class UserProfileController : Controller
    {
        private UserService userService = new UserService();
        // GET: UserProfile
        public ActionResult Index()
        {
            IEnumerable<UserProfileViewModel> data = userService.GetAllUserprofile();
            return View(data);
        }

        public ActionResult UserOwnProfile(string user)
        {
            UserProfileViewModel data = userService.GetUserOwnprofile(user);
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserProfileViewModel userVM)
        {
            var RedirectUrl = "/User/Index";
            if (ModelState.IsValid)
            {
                userService.CreateUserprofile(userVM);
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
            UserProfileViewModel user = userService.GetUserByID(id);
            if(user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserProfileViewModel userVM)
        {
              var RedirectUrl = "/User/Index";
            if (ModelState.IsValid)
            {
                userService.UpdateUserProfile(userVM);
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
            UserProfileViewModel user = userService.GetUserByID(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }       

        public ActionResult Delete(int id)
        {
            UserProfileViewModel user = userService.GetUserByID(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            userService.DeleteUserprofile(id);
            return Json(Request.UrlReferrer.ToString());
        }
    }
}