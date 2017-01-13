using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reviewIt.Domain.Models
{
   public  class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        public string PostReview { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Rating { get; set; }
        public string Createdby { get; set; }



    }
}
