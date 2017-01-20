using reviewIt.Core.Services;
using reviewIt.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace reviewIt.WebUI.Controllers
{
     [Authorize]
    public class AdvertisementController : Controller
    {
        private AdvertisementService advertisementService = new AdvertisementService();
        // GET: UserProfile
       
        public ActionResult Index(string user)
        {
            IEnumerable<AdvertisementViewModel> data = advertisementService.GetAllAdvertisement(user);
            return View(data.OrderByDescending(x =>x.CreatedDate));
        }

        public ActionResult IndexforAll()
        {
            IEnumerable<AdvertisementViewModel> data = advertisementService.GetAllBusinessAdvertisement();
            return View(data.OrderByDescending(x => x.CreatedDate));
        }

        public ActionResult AllPhotos(string user)
        {
            IEnumerable<AdvertisementViewModel> data = advertisementService.GetAllAdvertisement(user);
            return View(data);
        }
       
        [Authorize(Roles="Business Owner")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AdvertisementViewModel addVM)
        {
           
            if (ModelState.IsValid)
            {
                var fileName ="";
                if (addVM.File != null)
                {
                    fileName = Path.GetFileName(addVM.File.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    addVM.File.SaveAs(path);
                }
              //  addVM.CreatedDate = DateTime.Now;
                advertisementService.CreateAdvertisement(addVM,fileName,User.Identity.Name);
                return RedirectToAction("IndividualBusinessProfile", "BusinessProfile", new { user = User.Identity.Name });
            }
            else
            {
               return View();
            }
        }

        public ActionResult Edit(int id)
        {
            AdvertisementViewModel advertisement = advertisementService.GetAdvertisementByID(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        [HttpPost]
        public ActionResult Edit(AdvertisementViewModel addVM)
        {
            if (ModelState.IsValid)
            {
                var fileName = "";
                if (addVM.File == null)
                {
                    fileName = "Default.png";

                }
                else
                {
                    fileName = Path.GetFileName(addVM.File.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    addVM.File.SaveAs(path);
                }
                advertisementService.UpdateAdvertisement(addVM,fileName,User.Identity.Name);
                return RedirectToAction("PartialViewBusinessandAdvertisement", "BusinessProfile", new { user = User.Identity.Name });
            }
            else
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            AdvertisementViewModel advertisement = advertisementService.GetAdvertisementByID(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        public ActionResult Delete(int id)
        {
            AdvertisementViewModel advertisement = advertisementService.GetAdvertisementByID(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }

            advertisementService.DeleteAdvertisement(id);
            return RedirectToAction("PartialViewBusinessandAdvertisement", "BusinessProfile", new { user = User.Identity.Name });

        }
    }
}