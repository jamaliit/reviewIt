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
        private Review review;

        public ReviewService()
        {
            db = new reviewItContext();
            reviewRepository = new Repository<Review>(db);
            businessRepository = new Repository<BusinessProfile>(db);
            businessCategoryRepository = new Repository<BusinessCategory>(db);
            //<UserProfile>(db);
        }


        public void CreateReview(string businessName,int businessId, string productName, int rating, string post, string owner,DateTime date)
        {
         
            review = new Review
            {
              BusinessId = businessId,
              BusinessName = businessName,
              ProductName=productName,
              Rating=rating,
              PostReview=post,
              Createdby= owner,
              CreatedDate = date,
            };

            reviewRepository.Insert(review);
            db.SaveChanges();

        }

        public void UpdateReview(int id,int businessId, string productName, int rating, string post, string owner,DateTime date)
        {
            review = new Review
            {
                ReviewId=id,
                BusinessId = businessId,
                ProductName = productName,
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
                                                     Createdby =s.Createdby,
                                                 }).AsEnumerable();
            return data;
        }

        public IEnumerable<ReviewViewModel> GetAllReviewByBusinessId(int id)
        {
            IEnumerable<ReviewViewModel> data = (from s in reviewRepository.Get()
                                                 where s.BusinessId == id
                                                 // join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                                 select new ReviewViewModel
                                                 {
                                                     ReviewId = s.ReviewId,
                                                     BusinessId = s.BusinessId,
                                                     PostReview = s.PostReview,
                                                     Rating = s.Rating,
                                                     Createdby = s.Createdby,
                                                 }).AsEnumerable();
            return data;
        }

        public IEnumerable<ReviewViewModel> GetAllReviewByUserId(string user)
        {
            IEnumerable<ReviewViewModel> data = (from s in reviewRepository.Get()
                                                 where s.Createdby == user
                                                 // join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                                 select new ReviewViewModel
                                                 {
                                                     ReviewId = s.ReviewId,
                                                     BusinessId = s.BusinessId,
                                                     BusinessName =s.BusinessName,
                                                     ProductName=s.ProductName,
                                                     PostReview = s.PostReview,
                                                     Rating = s.Rating,
                                                     Createdby = s.Createdby,
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
                                        ProductName=s.ProductName,
                                        Createdby=s.Createdby
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
                                                  
                                                  ReviewId= s.ReviewId,
                                                  BusinessName = s.BusinessName,
                                                  CategoryName = c.CategoryName,
                                                  Rating= s.Rating,
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
                ReviewViewModel data1= new ReviewViewModel{
                 CategoryName = CategoryName,
                  TotalReview = countReview,             
                };
                list.Add(data1);
                
            }

                //ReviewViewModel[] data2 = (from s in reviewRepository.Get()
                //                           join d in businessRepository.Get() on s.BusinessName equals d.BusinessName
                //                           join c in businessCategoryRepository.Get() on d.CategoryId equals c.CategoryId
                //                           select new ReviewViewModel
                //                           {
                //                               CategoryName = item,
                //                               star5 = fiveStar,
                //                               star4 = fourStar,
                //                               star3 = threeStar,
                //                               star2 = twoStar,
                //                               star1 = oneStar,
                //                           }).ToArray();



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
                                                     Rating= s.Rating,
                                                     
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
                                             Sep =sep,
                                             Oct = oct,
                                             Nov = nov,
                                             Dec = dec

                                             }).FirstOrDefault();
            return monthWiseData;
        } 
          
        }
    }

    


