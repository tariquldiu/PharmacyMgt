using PharmacyManagementSystem.Gateway;
using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PharmacyManagementSystem.BLL
{

    public class ProductManager
    {
        ProductGateway aProduct = new ProductGateway();
         
        public List<Product> GetAllProduct()
        {
            return aProduct.GetAllProduct();
        }
        public bool SaveProduct(Product product)
        {
            bool res = aProduct.SaveProduct(product);
            return res;
        }

        public List<Product> GetDetails(string startDate, string endDate)
        {
            return aProduct.GetDetails(startDate, endDate);
        }
    }
}