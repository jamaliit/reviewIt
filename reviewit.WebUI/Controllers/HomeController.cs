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
        public ActionResult Index()
        {
            //BusinessProfileViewModel chartData = businessProfileService.CountAllRating(User.Identity.Name);
            //return View(chartData);
            return View();
        }


        public JsonResult OverallRatingChart()
        {
            BusinessProfileViewModel chartData = businessProfileService.CountAllRating(User.Identity.Name);
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

        public ActionResult CategoryWiseRating()
        {
            return View();
        }
        public JsonResult CategoryWiseRatingChart()
        {
            IEnumerable<ReviewViewModel> data = reviewService.getCategoryWiseRating();
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CategoryWiseRatingWithinDate()
        {
            ViewBag.businessCategoryList = commonController.GetAllBusinessCategory();
            ViewBag.AllBusinessList = commonController.GetAllBusiness();
            
            return View();
        }

        public ActionResult CategoryWiseRatingWithinDateRange(int businessId, int year)
        {
            string Businessname = commonController.GetBusinessName(businessId);
            ReviewViewModel data = reviewService.getCategoryWiseRatingWithinDateRange(Businessname,year);


            var title = new Title() { Text = "Month Wise Review" };

            var subtitle = new Subtitle() { Text = " " +year };
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
                               Data = new Data(new object[] { data.Jan, data.Feb, data.Mar, data.Apr,data.May, data.June,data.July,data.Aug,data.Sep,data.Oct,data.Nov,data.Dec }),
                               Name = seriesName                          
                           });

           return View(chart);
        }


        public ActionResult GoogleMap()
        {
            return View();
        }



        public ActionResult LocationWiseRating()
        {
            return View();
        }
    }
}