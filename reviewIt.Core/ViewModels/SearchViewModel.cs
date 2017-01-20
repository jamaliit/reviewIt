using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reviewIt.Core.ViewModels
{
   public class SearchViewModel
    {
       [Required(ErrorMessage="Please enter a Name")]
       public string Name { get; set; }

        [Required(ErrorMessage = "Please select an option")]
       public string Option { get; set; }


    }

}
