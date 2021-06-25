using PharmacyManagementSystem.Gateway;
using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.BLL
{
    public class ProductTypeManager
    {
        ProductTypeGateway aType = new ProductTypeGateway();

        public List<ProductType> GetAllProductType() 
        {
            return aType.GetAllProductType();
        }
        public bool SaveProductType(ProductType type)
        {
            bool res = aType.SaveProductType(type);
            return res;
        }
    }
}