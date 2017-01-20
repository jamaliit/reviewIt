using reviewIt.Domain.Models;
using reviewIt.Domain.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
  public class OverallRatingService
    {

        private reviewItContext db;
        private IRepository<Review> reviewRepository;
        private Review review;

        public OverallRatingService()
        {
            db = new reviewItContext();
            //reviewRepository = new Repository<Review>(db);
            //<UserProfile>(db);
        }


      
    }
}
