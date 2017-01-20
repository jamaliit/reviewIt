using reviewIt.Core.ViewModels;
using reviewIt.Domain.Models;
using reviewIt.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reviewIt.Core.Services
{
    public class ReviewService
    {
        private reviewItContext db;
        private IRepository<Review> reviewRepository;
        private IRepository<BusinessProfile> businessRepository;
        private IRepository<BusinessCategory> businessCategoryRepository;
        private IRepository<UserProfile> userRepository;
        private Review review;

        public ReviewService()
        {
            db = new reviewItContext();
            reviewRepository = new Repository<Review>(db);
            businessRepository = new Repository<BusinessProfile>(db);
            businessCategoryRepository = new Repository<BusinessCategory>(db);
            userRepository = new Repository<UserProfile>(db);
            //<UserProfile>(db);
        }

        public void CreateReview(string businessName, int businessId, int rating, string post, string owner, DateTime date)
        {

            review = new Review
            {
                BusinessId = businessId,
                BusinessName = businessName,
               
                Rating = rating,
                PostReview = post,
                Createdby = owner,
                CreatedDate = date,
            };

            reviewRepository.Insert(review);
            db.SaveChanges();

        }

        public void UpdateReview(int id, string Businessname,int businessId, int rating, string post, string owner, DateTime date)
        {
            review = new Review
            {
                ReviewId = id,
                BusinessId = businessId,
                 BusinessName = Businessname,
                Rating = rating,
                PostReview = post,
                Createdby = owner,
                CreatedDate = date,
            };

            reviewRepository.Update(review);
            db.SaveChanges();

        }
        public void DeleteReview(int reviewID)
        {
            reviewRepository.Delete(new Review { ReviewId = reviewID });
            db.SaveChanges();
        }
        public IEnumerable<ReviewViewModel> GetAllReview()
        {
            IEnumerable<ReviewViewModel> data = (from s in reviewRepository.Get()
                                                 //where s.IsAccount == true
                                                 // join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                                 select new ReviewViewModel
                                                 {
                                                     ReviewId = s.ReviewId,
                                                     BusinessId = s.BusinessId,
                                                     PostReview = s.PostReview,
                                                     Rating = s.Rating,
                                                     Createdby = s.Createdby,
                                                     CreatedDate = s.CreatedDate,
                                                 }).AsEnumerable();
            return data;
        }
        public IEnumerable<ReviewViewModel> GetAllReviewByBusinessId(int id)
        {
            IEnumerable<ReviewViewModel> data = (from s in reviewRepository.Get()
                                                 where s.BusinessId == id
                                                 join d in userRepository.Get() on s.Createdby equals d.Name                                                   
                                                  
                                                 // join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                                 select new ReviewViewModel
                                                 {
                                                     ReviewId = s.ReviewId,
                                                     BusinessId = s.BusinessId,
                                                     BusinessName = s.BusinessName,
                                                     UserImage = d.ImageName,
                                                     PostReview = s.PostReview,
                                                     Rating = s.Rating,
                                                     Createdby = s.Createdby,
                                                     CreatedDate = s.CreatedDate,
                                                 }).AsEnumerable();
            return data;
        }

        public IEnumerable<ReviewViewModel> GetAllReviewByUserId(string user)
        {
            IEnumerable<ReviewViewModel> data = (from s in reviewRepository.Get()
                                                 where s.Createdby == user
                                                 join d in userRepository.Get() on s.Createdby equals d.Name                                                   
                                                      
                                                 // join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                                 select new ReviewViewModel
                                                 {
                                                     ReviewId = s.ReviewId,
                                                     BusinessId = s.BusinessId,
                                                     BusinessName = s.BusinessName,
                                                     UserImage = d.ImageName,
                                                    
                                                     PostReview = s.PostReview,
                                                     Rating = s.Rating,
                                                     Createdby = s.Createdby,
                                                     CreatedDate = s.CreatedDate,
                                                 }).AsEnumerable();
            return data;
        }
        public ReviewViewModel GetReviewByID(int reviewID)
        {
            ReviewViewModel data = (from s in reviewRepository.Get()
                                    where s.ReviewId == reviewID
                                    //join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                    select new ReviewViewModel
                                    {
                                        ReviewId = s.ReviewId,
                                        BusinessId = s.BusinessId,
                                        PostReview = s.PostReview,
                                        Rating = s.Rating,
                                        
                                        Createdby = s.Createdby
                                    }).SingleOrDefault();
            return data;
        }
        public IEnumerable<ReviewViewModel> getCategoryWiseRating()
        {

            IEnumerable<ReviewViewModel> data = (from s in reviewRepository.Get()
                                                 join d in businessRepository.Get() on s.BusinessName equals d.BusinessName
                                                 join c in businessCategoryRepository.Get() on d.CategoryId equals c.CategoryId
                                                 select new ReviewViewModel
                                                 {

                                                     ReviewId = s.ReviewId,
                                                     BusinessName = s.BusinessName,
                                                     CategoryName = c.CategoryName,
                                                     Rating = s.Rating,
                                                     //star5 = reviewRepository.Get().Count(t => t.Rating == 5 && CategoryName == c.CategoryName),
                                                 }).AsEnumerable();


            var categoryName = db.BusinessCategory.Select(p => p.CategoryName);
            // double fiveStar, fourStar, threeStar, twoStar, oneStar;
            int countReview;
            string CategoryName;
            var list = new List<ReviewViewModel>();

            foreach (var item in categoryName)
            {

                CategoryName = item;
                countReview = data.Count(t => t.CategoryName == item);
                //  fiveStar = data.Count(t => t.Rating == 5 && t.CategoryName == item);
                // fourStar = data.Count(t => t.Rating == 4 && t.CategoryName == item);
                // threeStar = data.Count(t => t.Rating == 3 && t.CategoryName == item);
                //   twoStar = data.Count(t => t.Rating == 2 && t.CategoryName == item);
                // oneStar = data.Count(t => t.Rating == 1 && t.CategoryName == item);
                ReviewViewModel data1 = new ReviewViewModel
                {
                    CategoryName = CategoryName,
                    TotalReview = countReview,
                };
                list.Add(data1);

            }


            return list;
        }

        public ReviewViewModel getCategoryWiseRatingWithinDateRange(string BusinessName, int year)
        {

            IEnumerable<ReviewViewModel> data = (from s in reviewRepository.Get()
                                                 where s.CreatedDate.Year == year && s.BusinessName == BusinessName

                                                 //join d in businessRepository.Get() on s.BusinessName equals d.BusinessName
                                                 //join c in businessCategoryRepository.Get() on d.CategoryId equals c.CategoryId
                                                 select new ReviewViewModel
                                                 {
                                                     month = s.CreatedDate.Month,
                                                     Rating = s.Rating,

                                                 }).AsEnumerable();


            int jan = data.Count(t => t.month == 1);
            int feb = data.Count(t => t.month == 2);
            int mar = data.Count(t => t.month == 3);
            int apr = data.Count(t => t.month == 4);
            int may = data.Count(t => t.month == 5);
            int jun = data.Count(t => t.month == 6);
            int jul = data.Count(t => t.month == 7);
            int aug = data.Count(t => t.month == 8);
            int sep = data.Count(t => t.month == 9);
            int oct = data.Count(t => t.month == 10);
            int nov = data.Count(t => t.month == 11);
            int dec = data.Count(t => t.month == 12);


            ReviewViewModel monthWiseData = (from s in reviewRepository.Get()
                                             where s.CreatedDate.Year == year && s.BusinessName == BusinessName

                                             //join d in businessRepository.Get() on s.BusinessName equals d.BusinessName
                                             //join c in businessCategoryRepository.Get() on d.CategoryId equals c.CategoryId
                                             select new ReviewViewModel
                                             {
                                                 Jan = jan,
                                                 Feb = feb,
                                                 Mar = mar,
                                                 Apr = apr,
                                                 May = may,
                                                 June = jun,
                                                 July = jul,
                                                 Aug = aug,
                                                 Sep = sep,
                                                 Oct = oct,
                                                 Nov = nov,
                                                 Dec = dec

                                             }).FirstOrDefault();
            return monthWiseData;
        }

        //Calender Char
        public IEnumerable<ReviewViewModel> getMonthDaywiseReview(string UserName, int year)
        {

            IEnumerable<ReviewViewModel> data = (from s in reviewRepository.Get()
                                                 where s.CreatedDate.Year == year && s.Createdby == UserName

                                                 //join d in businessRepository.Get() on s.BusinessName equals d.BusinessName
                                                 //join c in businessCategoryRepository.Get() on d.CategoryId equals c.CategoryId
                                                 select new ReviewViewModel
                                                 {
                                                     month = s.CreatedDate.Month,
                                                     day = s.CreatedDate.Day,
                                                     Rating = s.Rating,

                                                 }).AsEnumerable();

            var data1 = (from s in db.Review
                         where s.CreatedDate.Year == year && s.Createdby == UserName
                         group s by new { month = s.CreatedDate.Month, day = s.CreatedDate.Day } into d
                         select new ReviewViewModel
                         {
                             month = d.Key.month,
                             day = d.Key.day,
                             Rating = d.Count(),
                         });
            //var data1 =  data.GroupBy(r => r.CreatedDate.Date, r => r.CreatedDate.Month).Select(r=>r.Count(r));




            return data1;
        }

        public IEnumerable<ReviewViewModel> getBusinessRanking(string businessCategory)
        {

           IEnumerable< ReviewViewModel> data = (from s in reviewRepository.Get()
                                                 join d in businessRepository.Get() on s.BusinessName equals d.BusinessName
                                                 join c in businessCategoryRepository.Get() on d.CategoryId equals c.CategoryId
                                                 where c.CategoryName == businessCategory
                                                 select new ReviewViewModel
                                                 {

                                                     //ReviewId = s.ReviewId,
                                                     BusinessName = s.BusinessName,
                                                     //CategoryName = c.CategoryName,
                                                     Rating =  s.Rating, 
                                                     //star5 = reviewRepository.Get().Count(t => t.Rating == 5 && CategoryName == c.CategoryName),
                                                 }).AsEnumerable();

           int totalBusiness = data.Select(m => m.BusinessName).Distinct().Count();
           if (totalBusiness != 0)
           {
               var meanRating = data.Average(s => s.Rating);

               int totalVotes = data.Count();

               int minVotes = totalVotes / totalBusiness;
               var BusinessName = data.Select(m => m.BusinessName).Distinct();
               var list = new List<ReviewViewModel>();

               foreach (var item in BusinessName)
               {
                   double avgRating = db.Review.Where(s => s.BusinessName == item).Average(s => s.Rating);
                   double noOfVotes = db.Review.Where(s => s.BusinessName == item).Count();

                   double upperValue = ((avgRating * noOfVotes) + (minVotes * meanRating));
                   double lowerValue = noOfVotes + minVotes;
                   double rankingValue = upperValue / lowerValue;

                   ReviewViewModel data1 = new ReviewViewModel
                   {
                       BusinessName = item,
                       ranking = rankingValue.ToString("0.00"),
                   };
                   if (noOfVotes >= minVotes)
                   {
                       list.Add(data1);
                   }
               }


               return list;
           }
           return null;
        }
    }
}

    


