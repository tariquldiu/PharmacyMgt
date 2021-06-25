using PharmacyManagementSystem.Gateway;
using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.BLL
{
    public class CategoryManager
    {
        CategoryGateway aCategory = new CategoryGateway();
        public List<Category> GetAllCategory()
        { 
            return aCategory.GetAllCategory();
        }
        public bool SaveCategory(Category category)
        {
            bool res = aCategory.SaveCategory(category);
            return res;
        }
    }
}