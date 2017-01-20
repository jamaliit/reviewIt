using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace reviewIt.Core.ViewModels
{
   public class UserProfileViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string About { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string ImageName { get; set; }
        public int TotalReviews { get; set; }
        public double NoOfUserWhodoesNotGiveReviews { get; set; }
        public double NoOfUserGiveReviews { get; set; }
        public double star1 { get; set; }
        public double star2 { get; set; }
        public double star3 { get; set; }
        public double star4 { get; set; }
        public double star5 { get; set; }
    }
}
