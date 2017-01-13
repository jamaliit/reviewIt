using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reviewIt.Domain.Models
{
   public class BusinessProfile
    {
        [Key]
       public int BusinessId { get; set; }

       public string BusinessName { get; set; }
       public string Location { get; set; }
       public string City { get; set; }
       public string ImageName { get; set; }
       public bool isBusiness { get; set; }
       public string Phone { get; set; }
       public string Website { get; set; }
       public string Categories { get; set; }
       //public string ClosedorMoved { get; set; }
      // public string OpeningHours { get; set; }
       public string Email { get; set; }
      
       public string UserName { get; set; }
      
       public string About { get; set; }
      
     //  [Required]
       public int CategoryId { get; set; }




    }
}
