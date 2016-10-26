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
            };

            userRepository.Insert(user);
            db.SaveChanges();

        }

        public void UpdateUserProfile(UserProfileViewModel userVM)
        {
            user = new UserProfile
           {
               UserId = userVM.UserId,
               Name = userVM.Name,
               Location = userVM.Location,
               Email = userVM.Email,
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
                                                 TotalReviews = total

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

       
    }
}

