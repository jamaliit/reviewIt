using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reviewIt.Domain.Models
{
    public class reviewItContext : DbContext
    {
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<BusinessProfile> BusinessProfile { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<BusinessCategory> BusinessCategory { get; set; }

    }
}
