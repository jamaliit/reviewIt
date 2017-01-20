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
   public class AdvertisementService
    {
        private reviewItContext db;
        private IRepository<Advertisement> advertisementRepository;
        private IRepository<BusinessProfile> businessRepository;
        private Advertisement advertisement;

        public AdvertisementService()
        {
            db = new reviewItContext();
            advertisementRepository = new Repository<Advertisement>(db);
            businessRepository = new Repository<BusinessProfile>(db);
            //<UserProfile>(db);
        }


        public void CreateAdvertisement(AdvertisementViewModel addVM,string path,string Businessname)
        {
            advertisement = new Advertisement
            {
                AdvertiseID = addVM.AdvertiseID,
                Title=addVM.Title,
                //BusinessId = addVM.BusinessId,
                BusinessName = Businessname,
                //Categories = addVM.Categories,
                About = addVM.About,
                Image = path,
                CreateDate = DateTime.Now,
                
            };

            advertisementRepository.Insert(advertisement);
            db.SaveChanges();

        }

        public void UpdateAdvertisement(AdvertisementViewModel addVM,string filename,string Businessname)
        {
            advertisement = new Advertisement
            {
                AdvertiseID = addVM.AdvertiseID,
                Title = addVM.Title,
               // BusinessId = advertisementVM.BusinessId,
                BusinessName = Businessname,
                Image = filename,
                About = addVM.About,
                CreateDate = DateTime.Now,
            };

            advertisementRepository.Update(advertisement);
            db.SaveChanges();

        }
        public void DeleteAdvertisement(int advertisementID)
        {
            advertisementRepository.Delete(new Advertisement { AdvertiseID = advertisementID });
            db.SaveChanges();
        }

        public IEnumerable<AdvertisementViewModel> GetAllAdvertisement(string BusinessName)
        {
            IEnumerable<AdvertisementViewModel> data = (from s in advertisementRepository.Get()
                                                          where s.BusinessName == BusinessName
                                                        join d in businessRepository.Get() on s.BusinessName equals d.BusinessName                                                   
                                                      
                                                          // join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                                          select new AdvertisementViewModel
                                                          {
                                                              AdvertiseID=s.AdvertiseID,
                                                              Title=s.Title,
                                                              BusinessName = BusinessName,
                                                              BusinessImage = d.ImageName,
                                                              About=s.About,
                                                              FilePath = s.Image,
                                                              CreatedDate = s.CreateDate,
                                                          }).AsEnumerable();
            return data;
        }


        public AdvertisementViewModel GetAdvertisementByID(int advertisementID)
        {
            AdvertisementViewModel data = (from s in advertisementRepository.Get()
                                             where s.AdvertiseID == advertisementID
                                           join d in businessRepository.Get() on s.BusinessName equals d.BusinessName
                                           select new AdvertisementViewModel
                                             {
                                                 AdvertiseID = s.AdvertiseID,
                                                 BusinessName = s.BusinessName,
                                                 BusinessImage = d.ImageName,
                                                 Title = s.Title,
                                                 //BusinessId = s.BusinessId,
                                                 About = s.About,
                                                 FilePath=s.Image,
                                                 CreatedDate = s.CreateDate,

                                             }).SingleOrDefault();
            return data;
        }




        public IEnumerable<AdvertisementViewModel> GetAllBusinessAdvertisement()
        {
            IEnumerable<AdvertisementViewModel> data = (from s in advertisementRepository.Get()
                                                        join d in businessRepository.Get() on s.BusinessName equals d.BusinessName                                                   
                                                        select new AdvertisementViewModel
                                                        {
                                                            AdvertiseID = s.AdvertiseID,
                                                            Title = s.Title,
                                                            BusinessName = s.BusinessName,
                                                            About = s.About,
                                                            BusinessImage=d.ImageName,
                                                            FilePath = s.Image,
                                                            CreatedDate = s.CreateDate,
                                                        }).AsEnumerable();
            return data;
        }
    }
}
