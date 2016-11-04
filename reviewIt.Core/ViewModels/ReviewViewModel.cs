using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reviewIt.Core.ViewModels
{
    public class ReviewViewModel
    {
        public int ReviewId { get; set; }
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        public string ProductName { get; set; }
        public string PostReview { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Rating { get; set; }
        public string Createdby { get; set; }
        public int TotalReview { get; set; }
        public string CategoryName { get; set; }
        public double star1 { get; set; }
        public double star2 { get; set; }
        public double star3 { get; set; }
        public double star4 { get; set; }
        public double star5 { get; set; }
        public int month{ get; set; }
        public int Jan { get; set; }
        public int Feb { get; set; }
        public int Mar { get; set; }
        public int Apr { get; set; }
        public int May { get; set; }
        public int June { get; set; }
        public int July { get; set; }
        public int Aug { get; set; }
        public int Sep { get; set; }
        public int Oct { get; set; }
        public int Nov { get; set; }
        public int Dec { get; set; }
        public int day { get; set; }
      
    }
}
