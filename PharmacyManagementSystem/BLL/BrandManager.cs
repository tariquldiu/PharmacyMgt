using PharmacyManagementSystem.Gateway;
using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.BLL
{
    public class BrandManager
    {
        BrandGateway aBrand = new BrandGateway();

        public List<Brand> GetAllBrand() 
        {
            return aBrand.GetAllBrand();
        }
        public bool SaveBrand(Brand brand)
        {
            bool res = aBrand.SaveBrand(brand);
            return res;
        }
    }
}