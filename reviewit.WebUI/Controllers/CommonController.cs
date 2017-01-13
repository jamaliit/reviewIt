using CRM.Core.Services;
using reviewIt.Core.Services;
using reviewIt.Core.ViewModels;
using reviewIt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace reviewIt.WebUI.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
       // private reviewItContext context;
        public SelectList GetAllBusinessCategory()
        {
            BusinessCategoryService businessCategoryService = new BusinessCategoryService();
            
            return new SelectList(businessCategoryService.GetAllBusinessCategory(),"Value","Text");
            //return View();
        }

        public SelectList GetAllBusiness()
        {
            BusinessProfileService businessService = new BusinessProfileService();

            return new SelectList(businessService.GetAllBusinessforDropDown(), "Value", "Text");
            //return View();
        }

        public string GetBusinessName(int id)
        {
            BusinessProfileService businessService = new BusinessProfileService();
           BusinessProfileViewModel businessName= businessService.getBusinessName(id);
            return businessName.BusinessName ;
        }

        internal string GetBusinessCategoryName(int id)
        {
            BusinessCategoryService businessCategoryService = new BusinessCategoryService();
            BusinessCategoryViewModel businessName = businessCategoryService.getBusinessCategoryName(id);
            return businessName.CategoryName;
        }
    }
}