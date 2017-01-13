using reviewIt.Core.Services;
using reviewIt.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace reviewIt.WebUI.Controllers
{
    public class ReviewController : Controller
    {

        private ReviewService reviewService = new ReviewService();
        private CommonController commonController = new CommonController();
        // GET: UserProfile
        public ActionResult Index()
        {
            IEnumerable<ReviewViewModel> data = reviewService.GetAllReview();
            return View(data);
        }
        public ActionResult AllBusinessReview(int id)
        {
            IEnumerable<ReviewViewModel> data = reviewService.GetAllReviewByBusinessId(id);
            return View(data);
        }

        public ActionResult AllUserReview(string user)
        {
            IEnumerable<ReviewViewModel> data = reviewService.GetAllReviewByUserId(user);
            return View(data);
        }

        public ActionResult Create()
        {
            ViewBag.AllBusinessList = commonController.GetAllBusiness();
            return View();
        }
        [HttpPost]
        public ActionResult Create(int businessId,int rating,string post,string owner,DateTime date)
        {
            var RedirectUrl = "/Review/Index";
            if (ModelState.IsValid)
            {
                string Businessname = commonController.GetBusinessName(businessId);
                reviewService.CreateReview(Businessname, businessId,rating,post,owner,date);
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
            ViewBag.AllBusinessList = commonController.GetAllBusiness();
            ReviewViewModel reviews = reviewService.GetReviewByID(id);
            if (reviews == null)
            {
                return HttpNotFound();
            }
            return View(reviews);
        }

        [HttpPost]
        public ActionResult Edit(int id,int businessId, int rating, string post, string owner,DateTime date)
        {
            var RedirectUrl = "/Review/Index";
            if (ModelState.IsValid)
            {
                string Businessname = commonController.GetBusinessName(businessId);
                reviewService.UpdateReview(id, Businessname,businessId, rating, post, owner, date);
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
            ReviewViewModel reviews = reviewService.GetReviewByID(id);
            if (reviews == null)
            {
                return HttpNotFound();
            }
            return View(reviews);
        }

        public ActionResult Delete(int id)
        {
            ReviewViewModel reviews = reviewService.GetReviewByID(id);
            if (reviews == null)
            {
                return HttpNotFound();
            }

            reviewService.DeleteReview(id);
            return Json(Request.UrlReferrer.ToString());
        }

       
    }
}
