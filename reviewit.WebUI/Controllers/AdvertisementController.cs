using CRM.Core.Services;
using CRM.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace reviewIt.WebUI.Controllers
{
    public class AdvertisementController : Controller
    {
        private AdvertisementService advertisementService = new AdvertisementService();
        // GET: UserProfile
        public ActionResult Index()
        {
            IEnumerable<AdvertisementViewModel> data = advertisementService.GetAllAdvertisement();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AdvertisementViewModel addVM)
        {
            var RedirectUrl = "/Advertisement/Index";
            if (ModelState.IsValid)
            {
                advertisementService.CreateAdvertisement(addVM);
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
            var RedirectUrl = "/Advertisement/Index";
            if (ModelState.IsValid)
            {
                advertisementService.UpdateAdvertisement(addVM);
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
            return Json(Request.UrlReferrer.ToString());
        }
    }
}