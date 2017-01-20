
using reviewIt.Core.Services;
using reviewIt.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace reviewIt.WebUI.Controllers
{
    public class EventController : Controller
    {

        private EventService eventService = new EventService();
        // GET: UserProfile
        public ActionResult Index()
        {
            IEnumerable<EventViewModel> data = eventService.GetAllEvent();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EventViewModel eventVM)
        {
            var RedirectUrl = "/Event/Index";
            if (ModelState.IsValid)
            {
                eventService.CreateEvent(eventVM);
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
            EventViewModel events = eventService.GetEventByID(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        [HttpPost]
        public ActionResult Edit(EventViewModel eventVM)
        {
            var RedirectUrl = "/Event/Index";
            if (ModelState.IsValid)
            {
                eventService.UpdateEvent(eventVM);
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
            EventViewModel events = eventService.GetEventByID(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        public ActionResult Delete(int id)
        {
            EventViewModel events = eventService.GetEventByID(id);
            if (events == null)
            {
                return HttpNotFound();
            }

            eventService.DeleteEvent(id);
            return Json(Request.UrlReferrer.ToString());
        }
    }
}