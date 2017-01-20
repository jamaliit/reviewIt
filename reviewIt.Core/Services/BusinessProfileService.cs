using CRM.Core.ViewModels;
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
    public class BusinessProfileService
    {
        private reviewItContext db;
        private IRepository<BusinessProfile> businessRepository;
        private IRepository<BusinessCategory> businessCategoryRepository;
        private BusinessProfile business;

        public BusinessProfileService()
        {
            db = new reviewItContext();
            businessRepository = new Repository<BusinessProfile>(db);
            businessCategoryRepository = new Repository<BusinessCategory>(db);
            //<UserProfile>(db);
        }


        public void CreateBusinessprofile(BusinessProfileViewModel businessVM)
        {
            business = new BusinessProfile
            {
                BusinessId = businessVM.BusinessId,
                BusinessName = businessVM.BusinessName,
                Categories = businessVM.Categories,
                About = businessVM.About,
                City = businessVM.City,
               // ClosedorMoved = businessVM.ClosedorMoved,
               // OpeningHours = businessVM.OpeningHours,
                Phone = businessVM.Phone,
                Website = businessVM.Website,
                Location = businessVM.Location,
                Email = businessVM.Email,
                UserName = businessVM.UserName,
                CategoryId = businessVM.CategoryId,
                isBusiness = false,
                ImageName = "Default.png",

            };

            businessRepository.Insert(business);
            db.SaveChanges();

        }

        public void UpdateBusinessProfile(BusinessProfileViewModel businessVM,string path)
        {
            business = new BusinessProfile
            {
                BusinessId = businessVM.BusinessId,
                BusinessName = businessVM.BusinessName,
                Categories = businessVM.Categories,
                About = businessVM.About,
                UserName = businessVM.UserName,
                Location = businessVM.Location,
                ImageName = path,
                City = businessVM.City,
                //ClosedorMoved = businessVM.ClosedorMoved,
               // OpeningHours = businessVM.OpeningHours,
                Phone = businessVM.Phone,
                Website = businessVM.Website,
               // Location = businessVM.Location,
                Email = businessVM.Email,
                CategoryId = businessVM.CategoryId,
                isBusiness = true,
                
            };

            businessRepository.Update(business);
            db.SaveChanges();

        }
        public void DeleteBusinessprofile(int businessID)
        {
            businessRepository.Delete(new BusinessProfile { BusinessId = businessID });
            db.SaveChanges();
        }

        public IEnumerable<BusinessProfileViewModel> GetAllBusinessprofile()
        {
            IEnumerable<BusinessProfileViewModel> data = (from s in businessRepository.Get()
                                                          where s.isBusiness == false
                                                          join d in businessCategoryRepository.Get() on s.CategoryId equals d.CategoryId
                                                          select new BusinessProfileViewModel
                                                          {

                                                              BusinessId = s.BusinessId,
                                                              BusinessName = s.BusinessName,
                                                              Categories = d.CategoryName,
                                                              About = s.About,
                                                              City = s.City,
                                                              //ClosedorMoved = s.ClosedorMoved,
                                                             // OpeningHours = s.OpeningHours,
                                                              Phone = s.Phone,
                                                              Website = s.Website,
                                                              Location = s.Location,
                                                              Email = s.Email,
                                                              UserName = s.UserName,

                                                          }).AsEnumerable();
            return data;
        }


        public BusinessProfileViewModel GetBusinessProfileByID(int businessID)
        {
            BusinessProfileViewModel data = (from s in businessRepository.Get()
                                             where s.BusinessId == businessID
                                             //join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                             select new BusinessProfileViewModel
                                             {
                                                 BusinessId = s.BusinessId,
                                                 BusinessName = s.BusinessName,
                                                 UserName = s.UserName,
                                                 ImageName=s.ImageName,
                                                 //Categories = s.Categories,
                                                 About = s.About,
                                                 City = s.City,
                                                // ClosedorMoved = s.ClosedorMoved,
                                                // OpeningHours = s.OpeningHours,
                                                 Phone = s.Phone,
                                                 Website = s.Website,
                                                 Location = s.Location,
                                                 Email = s.Email,
                                                 CategoryId = s.CategoryId,

                                             }).SingleOrDefault();
            return data;
        }

        public BusinessProfileViewModel GetOwnBusinessProfileByID(string userName)
        {
            double fiveStar = db.Review.Count(t => t.Rating == 5 && t.BusinessName == userName);
            double fourStar = db.Review.Count(t => t.Rating == 4 && t.BusinessName == userName);
            double threeStar = db.Review.Count(t => t.Rating == 3 && t.BusinessName == userName);
            double twoStar = db.Review.Count(t => t.Rating == 2 && t.BusinessName == userName);
            double oneStar = db.Review.Count(t => t.Rating == 1 && t.BusinessName == userName);

            double totalStar = (fiveStar + fourStar + threeStar + twoStar + oneStar);
            double weightedRating = (fiveStar * 5 + fourStar * 4 + threeStar * 3 + twoStar * 2 + oneStar * 1);
             double overallRating;
             string avgRating;
             if (totalStar != 0)
             {
                 overallRating = weightedRating / totalStar;
                  avgRating = overallRating.ToString("0.00");
             }
             else avgRating = "0";
           
           // int total = db.Review.Count(t => t.Createdby == userName);
            BusinessProfileViewModel data = (from s in businessRepository.Get()
                                             where (s.Email == userName || s.BusinessName == userName)
                                             join d in businessCategoryRepository.Get() on s.CategoryId equals d.CategoryId
                                             select new BusinessProfileViewModel
                                             {
                                                 BusinessId = s.BusinessId,
                                                 BusinessName = s.BusinessName,
                                                 Categories = d.CategoryName,
                                                 UserName = s.UserName,
                                                 About = s.About,
                                                 ImageName = s.ImageName,
                                                 City = s.City,
                                                // ClosedorMoved = s.ClosedorMoved,
                                                // OpeningHours = s.OpeningHours,
                                                 Phone = s.Phone,
                                                 Website = s.Website,
                                                 Location = s.Location,
                                                 Email = s.Email,
                                                 OverallRating = avgRating,
                                                 CategoryId = s.CategoryId,
                                                 TotalReview = totalStar,
                                             }).SingleOrDefault();
            return data;
        }


        public BusinessProfileViewModel GetRequestedBusniessbyId(int businessID)
        {
            BusinessProfileViewModel data = (from s in businessRepository.Get()
                                             where s.BusinessId == businessID
                                             //join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                             select new BusinessProfileViewModel
                                             {
                                                 BusinessId = s.BusinessId,
                                                 BusinessName = s.BusinessName,
                                                 Categories = s.Categories,
                                                 About = s.About,
                                                 City = s.City,
                                                 // ClosedorMoved = businessVM.ClosedorMoved,
                                                 // OpeningHours = businessVM.OpeningHours,
                                                 Phone = s.Phone,
                                                 Website = s.Website,
                                                 Location = s.Location,
                                                 Email = s.Email,
                                                 UserName = s.UserName,
                                                 CategoryId = s.CategoryId,
                                          

                                             }).SingleOrDefault();
            return data;
        }

        public IEnumerable<DropDownViewModel> GetAllBusinessforDropDown()
        {
            IEnumerable<DropDownViewModel> data = (from s in businessRepository.Get()
                                                   //where s.IsAccount == true
                                                   // join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                                   select new DropDownViewModel
                                                   {

                                                       Value = s.BusinessId,
                                                       Text = s.BusinessName,

                                                   }).AsEnumerable();
            return data;
        }

        public BusinessProfileViewModel getBusinessName(int id)
        {
            BusinessProfileViewModel data = (from s in businessRepository.Get()
                                                   where s.BusinessId == id
                                                   // join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                                   select new BusinessProfileViewModel
                                                   {

                                                      BusinessName = s.BusinessName

                                                   }).SingleOrDefault();
            return data;
        }


        public BusinessProfileViewModel CountAllRating(string userName)
        {
           // var _context = new TestEntities();

           // ArrayList xValue = new ArrayList();
           // ArrayList yValue = new ArrayList();
           
            double fiveStar = db.Review.Count(t => t.Rating == 5 && t.BusinessName == userName);
            double fourStar = db.Review.Count(t => t.Rating == 4 && t.BusinessName == userName);
            double threeStar = db.Review.Count(t => t.Rating == 3 && t.BusinessName == userName);
            double twoStar = db.Review.Count(t => t.Rating == 2 && t.BusinessName == userName);
            double oneStar = db.Review.Count(t => t.Rating == 1 && t.BusinessName == userName);

            BusinessProfileViewModel data = (from s in businessRepository.Get()
                                             //where (s.Email == userName || s.BusinessName == userName)
                                             //join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                             select new BusinessProfileViewModel
                                             {                                               
                                                 star1 =oneStar,
                                                 star2=twoStar,
                                                 star3=threeStar,
                                                 star4=fourStar,
                                                 star5=fiveStar
                                             }).FirstOrDefault();
            return data;
           
        }


        public BusinessProfileViewModel GetBusinessProfileByName(string name)
        {
            BusinessProfileViewModel data = (from s in businessRepository.Get()
                                             where s.BusinessName == name
                                             //join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                             select new BusinessProfileViewModel
                                             {
                                                 BusinessId = s.BusinessId,
                                                 BusinessName = s.BusinessName,
                                                 UserName = s.UserName,
                                                 ImageName = s.ImageName,
                                                 Categories = s.Categories,
                                                 About = s.About,
                                                 City = s.City,
                                                 // ClosedorMoved = s.ClosedorMoved,
                                                 // OpeningHours = s.OpeningHours,
                                                 Phone = s.Phone,
                                                 Website = s.Website,
                                                 Location = s.Location,
                                                 Email = s.Email,

                                             }).SingleOrDefault();
            return data;
        }

    }
}


