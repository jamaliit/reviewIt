using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reviewIt.Core.ViewModels
{
   public class UserProfileViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public int TotalReviews { get; set; }
        public double NoOfUserWhodoesNotGiveReviews { get; set; }
        public double NoOfUserGiveReviews { get; set; }

    }
}
