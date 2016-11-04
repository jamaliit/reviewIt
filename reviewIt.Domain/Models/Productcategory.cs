using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reviewIt.Domain.Models
{
   public class Productcategory
    {
       [Key]
       public int ProductcategoryID { get; set; }
       public int BusinessCategoryId { get; set; }
       public string CategoryName { get; set; }

    }
}
