using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reviewIt.Core.ViewModels
{
   public class BusinessProfileViewModel
    {
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Categories { get; set; }
        public string ClosedorMoved { get; set; }
        public string OpeningHours { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string About { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double OverallRating { get; set; }
        public double star1 { get; set; }
        public double star2 { get; set; }
        public double star3 { get; set; }
        public double star4 { get; set; }
        public double star5 { get; set; }


    }
}
