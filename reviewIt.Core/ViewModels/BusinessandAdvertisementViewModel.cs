using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reviewIt.Core.ViewModels
{
   public class BusinessandAdvertisementViewModel
    {
       public IEnumerable<AdvertisementViewModel> advertisement { get; set; }
       public BusinessProfileViewModel business { get; set; }
    }
}
