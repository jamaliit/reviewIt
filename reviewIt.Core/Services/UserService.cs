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
    public class UserService
    {
        private reviewItContext db;
        private IRepository<UserProfile> userRepository;
        private IRepository<Review> reviewRepository;
        private UserProfile user;

        public UserService()
        {
            db = new reviewItContext();
            userRepository = new Repository<UserProfile>(db);
            //<UserProfile>(db);
        }


        public void CreateUserprofile(UserProfileViewModel userVM)
        {
            user = new UserProfile
            {
                UserId = userVM.UserId,
                Name = userVM.Name,
                Location = userVM.Location,
                Email = userVM.Email,
                ImageName = userVM.ImageName,
            };

            userRepository.Insert(user);
            db.SaveChanges();

        }

        public void UpdateUserProfile(UserProfileViewModel userVM,string path)
        {
            user = new UserProfile
           {
               UserId = userVM.UserId,
               Name = userVM.Name,
               Location = userVM.Location,
               Email = userVM.Email,
               About = userVM.About,
               ImageName = path,
           };

            userRepository.Update(user);
            db.SaveChanges();
        }

        public void DeleteUserprofile(int userID)
        {
            userRepository.Delete(new UserProfile { UserId = userID });
            db.SaveChanges();
        }

        public IEnumerable<UserProfileViewModel> GetAllUserprofile()
        {
            IEnumerable<UserProfileViewModel> data = (from s in userRepository.Get()
                                                      //where s.IsAccount == true
                                                      // join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                                      select new UserProfileViewModel
                                                      {
                                                          UserId = s.UserId,
                                                          Name = s.Name,
                                                          Location = s.Location,
                                                          Email = s.Email,
                                                      }).AsEnumerable();
            return data;
        }


        public UserProfileViewModel GetUserByID(int userID)
        {
            UserProfileViewModel data = (from s in userRepository.Get()
                                         where s.UserId == userID
                                         //join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                         select new UserProfileViewModel
                                         {
                                             UserId = s.UserId,
                                             Name = s.Name,
                                             Location = s.Location,
                                             Email = s.Email,
                                             About =s.About,
                                             ImageName = s.ImageName,
                                             
                                         }).SingleOrDefault();
            return data;
        }

        public UserProfileViewModel GetUserOwnprofile(string userName)
        {
          //int total = (from s in db.Review where s.Createdby == userName select ).Count();
            int total = db.Review.Count(t => t.Createdby == userName);
           
            UserProfileViewModel data = (from s in userRepository.Get()
                                             where s.Email == userName || s.Name == userName
                                             //join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                             select new UserProfileViewModel
                                             {
                                                 UserId = s.UserId,
                                                 Name= s.Name,
                                                 Location = s.Location,
                                                 Email = s.Email,
                                                 About = s.About,
                                                 ImageName = s.ImageName,
                                                 TotalReviews = total,
                                                 

                                             }).SingleOrDefault();
            return data;
        }

        public UserProfileViewModel CountUserGiveReviewsorNot()
        {
            double totalUser = db.UserProfile.Count();
            //int noOfUserGivesReviews = db.Review

            double noOfUserGivesReviews = db.Review.Select(p => p.Createdby).Distinct().Count();

          //  double noOfUserGivesReviews = UserNameGivesReviews.Count();


            UserProfileViewModel data = (from s in userRepository.Get()
                                        // where s.Email == userName || s.Name == userName
                                         //join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                         select new UserProfileViewModel
                                         {
                                           NoOfUserWhodoesNotGiveReviews = totalUser - noOfUserGivesReviews ,
                                           NoOfUserGiveReviews = noOfUserGivesReviews

                                         }).FirstOrDefault();
            return data;           
           
        }



        public UserProfileViewModel CountAllRating(string userName)
        {

           // var _context = new TestEntities();

           // ArrayList xValue = new ArrayList();
           // ArrayList yValue = new ArrayList();
           
            double fiveStar = db.Review.Count(t => t.Rating == 5 && t.Createdby == userName);
            double fourStar = db.Review.Count(t => t.Rating == 4 && t.Createdby == userName);
            double threeStar = db.Review.Count(t => t.Rating == 3 && t.Createdby == userName);
            double twoStar = db.Review.Count(t => t.Rating == 2 && t.Createdby == userName);
            double oneStar = db.Review.Count(t => t.Rating == 1 && t.Createdby == userName);

            UserProfileViewModel data = (from s in userRepository.Get()
                                             //where (s.Email == userName || s.BusinessName == userName)
                                             //join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                             select new UserProfileViewModel
                                             {                                               
                                                 star1 =oneStar,
                                                 star2=twoStar,
                                                 star3=threeStar,
                                                 star4=fourStar,
                                                 star5=fiveStar
                                             }).FirstOrDefault();
            return data;
           
        }

        }
    }


