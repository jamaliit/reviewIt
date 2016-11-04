using CRM.Core.ViewModels;
using reviewIt.Domain.Models;
using reviewIt.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core.Services
{
   public class AdvertisementService
    {
        private reviewItContext db;
        private IRepository<Advertisement> advertisementRepository;
        private Advertisement advertisement;

        public AdvertisementService()
        {
            db = new reviewItContext();
            advertisementRepository = new Repository<Advertisement>(db);
            //<UserProfile>(db);
        }


        public void CreateAdvertisement(AdvertisementViewModel addVM)
        {
            advertisement = new Advertisement
            {
                AdvertiseID = addVM.AdvertiseID,
                Title=addVM.Title,
                BusinessId = addVM.BusinessId,
                //BusinessName = addVM.BusinessName,
                //Categories = addVM.Categories,
                About = addVM.About,
                
            };

            advertisementRepository.Insert(advertisement);
            db.SaveChanges();

        }

        public void UpdateAdvertisement(AdvertisementViewModel advertisementVM)
        {
            advertisement = new Advertisement
            {
                AdvertiseID= advertisement.AdvertiseID,
                Title= advertisement.Title,
                BusinessId = advertisementVM.BusinessId,
              //  BusinessName = advertisementVM.BusinessName,
               
                About = advertisementVM.About,
       
            };

            advertisementRepository.Update(advertisement);
            db.SaveChanges();

        }
        public void DeleteAdvertisement(int advertisementID)
        {
            advertisementRepository.Delete(new Advertisement { AdvertiseID = advertisementID });
            db.SaveChanges();
        }

        public IEnumerable<AdvertisementViewModel> GetAllAdvertisement()
        {
            IEnumerable<AdvertisementViewModel> data = (from s in advertisementRepository.Get()
                                                          //where s.IsAccount == true
                                                          // join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                                          select new AdvertisementViewModel
                                                          {
                                                              AdvertiseID=s.AdvertiseID,
                                                              Title=s.Title,
                                                              BusinessId = s.BusinessId,
                                                             About=s.About,

                                                          }).AsEnumerable();
            return data;
        }


        public AdvertisementViewModel GetAdvertisementByID(int advertisementID)
        {
            AdvertisementViewModel data = (from s in advertisementRepository.Get()
                                             where s.AdvertiseID == advertisementID
                                             //join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                             select new AdvertisementViewModel
                                             {
                                                 AdvertiseID = s.AdvertiseID,
                                                 Title = s.Title,
                                                 BusinessId = s.BusinessId,
                                                 About = s.About,

                                             }).SingleOrDefault();
            return data;
        }



    }
}
