using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace reviewIt.Core.ViewModels
{
   public class BusinessProfileViewModel
    {
        public int BusinessId { get; set; }
       
        [Required(ErrorMessage = "Business Name is required")]
        public string BusinessName { get; set; }
        public string Location { get; set; }
        public bool isBusiness { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string ImageName { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Categories { get; set; }
       // public string ClosedorMoved { get; set; }
        //public string OpeningHours { get; set; }
         [Required(ErrorMessage = "Email is required")] 
       public string Email { get; set; }
        
       [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }
        public string About { get; set; }
        
       [Required(ErrorMessage = "Please select Business Category")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double TotalReview { get; set; }

      
        public String OverallRating { get; set; }
        public double star1 { get; set; }
        public double star2 { get; set; }
        public double star3 { get; set; }
        public double star4 { get; set; }
        public double star5 { get; set; }


    }
}
