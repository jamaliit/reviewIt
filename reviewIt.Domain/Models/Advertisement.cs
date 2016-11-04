using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reviewIt.Domain.Models
{
    public class Advertisement
    {

        [Key]
        public int AdvertiseID { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public string Image { get; set; }
        public int BusinessId { get; set; }
    }
}
