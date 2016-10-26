using CRM.Core.ViewModels;
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
    public class BusinessCategoryService
    {
         private reviewItContext db;
        private IRepository<BusinessCategory> businessCategoryRepository;
        //private BusinessProfile business;

        public BusinessCategoryService()
        {
            db = new reviewItContext();
            businessCategoryRepository = new Repository<BusinessCategory>(db);
            //<UserProfile>(db);
        }


        public IEnumerable<DropDownViewModel> GetAllBusinessCategory()
        {
            IEnumerable<DropDownViewModel> data = (from s in businessCategoryRepository.Get()
                                                          //where s.IsAccount == true
                                                          // join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                                           select new DropDownViewModel
                                                          {

                                                            Value = s.CategoryId,
                                                            Text = s.CategoryName,

                                                          }).AsEnumerable();
            return data;
        }


      
    }
}
