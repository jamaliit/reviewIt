using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace reviewIt.Domain.Models
{
    public class UserProfile
    {
        [Key]
        public int UserId { get; set; }
        public string Name {get; set;}
        public string Location { get; set; }
        public string Email { get; set; }
        public string About { get; set; }
        public string ImageName { get; set; }

    }
}
