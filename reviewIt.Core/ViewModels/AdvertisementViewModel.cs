using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core.ViewModels
{
    public class AdvertisementViewModel
    {
        
        public int AdvertiseID { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public string Image { get; set; }
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
    }
}
