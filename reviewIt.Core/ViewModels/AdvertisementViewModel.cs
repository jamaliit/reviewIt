using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace reviewIt.Core.ViewModels
{
    public class AdvertisementViewModel
    {
        
        public int AdvertiseID { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Post About is required")]
        public string About { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string FilePath { get; set; }
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        public string BusinessImage { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
