using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using reviewIt.Core.Services;
using reviewIt.Core.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace reviewIt.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private BusinessProfileService businessProfileService = new BusinessProfileService();
        private CommonController commonController = new CommonController();

        private ReviewService reviewService = new ReviewService();

        private UserService userProfileService = new UserService();
        private AdvertisementService advertisementService = new AdvertisementService();
        // GET: UserProfile

        public ActionResult Index()
        {
            //BusinessProfileViewModel chartData = businessProfileService.CountAllRating(User.Identity.Name);
            //return View(chartData);
            return View();
        }


        public JsonResult OverallRatingChartforBusiness(string user)
        {
            
                BusinessProfileViewModel chartData = businessProfileService.CountAllRating(user);

                return Json(chartData, JsonRequestBehavior.AllowGet);
            
        }


            
        public JsonResult OverallRatingChartforUser(string user)
        {
            
                UserProfileViewModel chartData = userProfileService.CountAllRating(user);
                return Json(chartData, JsonRequestBehavior.AllowGet);
          
        }

        //Draw Chart with Html Helper(bar chart)

        //public ActionResult DrawChartwithHtmlHelper()
        //{
        //    BusinessProfileViewModel data = businessProfileService.ChartBar(User.Identity.Name);
        //    var chart = new Chart(width: 300, height: 200)
        //         .AddSeries(
        //                     chartType: "bar",
        //                        xValue: new[] { "1*", "2*", "3*", "4*", "5*" },
        //                        yValues: new[] { data.star1, data.star2, data.star3, data.star4, data.star5 })

        //                     .GetBytes("png");
        //    return File(chart, "image/bytes");
        //}

        [Authorize(Roles="Admin")]
        public JsonResult TotalUservsNoOfUserwhoGivesReviews()
        {
            UserProfileViewModel data = userProfileService.CountUserGiveReviewsorNot();
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        //public ActionResult CategoryWiseRating()
        //{
        //    IEnumerable<ReviewViewModel> data = reviewService.getCategoryWiseRating();
        //    //return Json(data, JsonRequestBehavior.AllowGet);
        //    return View(data);
        //}

         [Authorize(Roles = "Admin")]
        public ActionResult CategoryWiseRating()
        {
            return View();
        }
        public JsonResult CategoryWiseRatingChart()
        {
            IEnumerable<ReviewViewModel> data = reviewService.getCategoryWiseRating();
            return Json(data, JsonRequestBehavior.AllowGet);

        }

       [Authorize(Roles = "Admin,Business Owner")]
        public ActionResult CategoryWiseRatingWithinDate()
        {
            ViewBag.businessCategoryList = commonController.GetAllBusinessCategory();
            ViewBag.AllBusinessList = commonController.GetAllBusiness();

            return View();
        }

        public ActionResult CategoryWiseRatingWithinDateRange(string businessName, int year)
        {
            //string Businessname = commonController.GetBusinessName(businessId);
            try
            {
                ReviewViewModel data = reviewService.getCategoryWiseRatingWithinDateRange(businessName, year);


                var title = new Title() { Text = "Month Wise Review" };

                var subtitle = new Subtitle() { Text = " " + year };
                string seriesName = "Reviews";

                DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
                    .SetTitle(title)
                    .SetSubtitle(subtitle)
                    .SetXAxis(new XAxis
                                {
                                    Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
                                })
                    .SetSeries(new Series
                                {
                                    Data = new Data(new object[] { data.Jan, data.Feb, data.Mar, data.Apr, data.May, data.June, data.July, data.Aug, data.Sep, data.Oct, data.Nov, data.Dec }),
                                    Name = seriesName
                                });

                return View(chart);
            }
            catch
            {
                ViewBag.Message = "No data available in this year";
                return View(ViewBag.Message);
            }
        }


        [Authorize(Roles="User")]
        public ActionResult MonthDaywiseReviewCalenderChart()
        {
            return View();
        }
        public JsonResult MonthDaywiseReviewCalenderChartforUser(string user,int year)
        {
            IEnumerable<ReviewViewModel> data = reviewService.getMonthDaywiseReview(user, year);
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult MonthwiseRating(int year)
        {

            IEnumerable<ReviewViewModel> data = reviewService.getMonthDaywiseReview(User.Identity.Name, year);
            return View(data);

        }

        public ActionResult Search()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Search(string name, string option)
        {
            if(option == "User")
            {
                UserProfileViewModel data = userProfileService.GetUserOwnprofile(name);
                if(data!=null)
                {
                    return RedirectToAction("UserOwnProfile", "UserProfile", new { user = name });
                }

                else
                {
                ViewBag.Message = "User not found";
                return View();
                }
            }

            else
            {

                BusinessProfileViewModel data = businessProfileService.GetBusinessProfileByName(name);
               if(data!=null)
                {
                    return RedirectToAction("IndividualBusinessProfile","BusinessProfile", new{user = name});
                }

                else{
                ViewBag.Message = "Business not found";
                return View();
                }
               
            }
//return View();
        }

        public ActionResult Ranking()
        {
            ViewBag.businessCategoryList = commonController.GetAllBusinessCategory();
           // IEnumerable<ReviewViewModel> data = reviewService.getBusinessRanking(businessCategory);
            return View();

        }

         [HttpPost]
        public ActionResult BusinessRanking(int CategoryId)
        {
            if (ModelState.IsValid)
            {
                string businessCategory = commonController.GetBusinessCategoryName(CategoryId);
                IEnumerable<ReviewViewModel> data = reviewService.getBusinessRanking(businessCategory);
                if (data != null)
                {
                    return View(data.OrderByDescending(s => s.ranking));
                }
                else
                {
                    ViewBag.message = "There is no busiess in your selected category ";
                    return RedirectToAction("Ranking");
                }
            }
            else
            {
                return RedirectToAction("Ranking");
            }

        }


        //public ActionResult GoogleMap()
        //{
        //    return View();
        //}

        //public ActionResult LocationWiseRating()
        //{
        //    return View();
        //}
    }
}