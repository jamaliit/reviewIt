using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reviewIt.Domain.Models
{
  public  class BusinessCategory
    {
      [Key]
      public int CategoryId { get; set; }
      public String CategoryName { get; set; }

    }
}
