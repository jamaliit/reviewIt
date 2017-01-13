using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reviewIt.Core.ViewModels
{
    public class RankingViewModel
    {
        [Required(ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }
    }
}
