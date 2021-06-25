using PharmacyManagementSystem.BLL;
using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacyManagementSystem.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        ProductTypeManager aTypeManager = new ProductTypeManager();
        BrandManager aBrandManager = new BrandManager();
        CategoryManager aCategoryManager = new CategoryManager();
        GroupManager aGroupManager = new GroupManager();
        ProductManager aProductManager = new ProductManager();
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Create()
        {
            return View();
        }
        public JsonResult GetDetails(string startDate, string endDate)
        {
            var detailList = aProductManager.GetDetails(startDate, endDate);
            var productList = (from p in detailList
                               select new
                               { p.ProductId, p.ProductName, p.TypeId, p.TypeName, p.GroupId, p.GroupName, p.BrandId, p.BrandName, p.CategoryId, p.CategoryName, p.AddedDate, p.IsActive })
                                 .ToList();
            return Json(productList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllProduct()
        {
            var products = aProductManager.GetAllProduct();
            var productList = (from p in products
                               select new
                               { p.ProductId, p.ProductName, p.TypeId,p.TypeName, p.GroupId,p.GroupName, p.BrandId, p.BrandName, p.CategoryId,p.CategoryName,p.AddedDate, p.IsActive })
                                 .ToList();
            return Json(productList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllProductType()
        {
            var productTypes = aTypeManager.GetAllProductType();
            var productTypeList = (from pt in productTypes
                                   select new
                                   { pt.TypeId, pt.TypeName })
                                 .ToList();
            return Json(productTypeList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllBrand()
        {
            var brands = aBrandManager.GetAllBrand();
            var brandList = (from b in brands
                             select new
                             { b.BrandId, b.BrandName })
                                 .ToList();
            return Json(brandList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllCategory()
        {
            var categories = aCategoryManager.GetAllCategory();
            var categorieList = (from c in categories
                                 select new
                                 { c.CategoryId, c.CategoryName })
                                 .ToList();
            return Json(categorieList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllGroup()
        {
            var groups = aGroupManager.GetAllGroup();
            var groupList = (from g in groups
                             select new
                             { g.GroupId, g.GroupName, g.Description })
                                 .ToList();
            return Json(groupList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveGroup(Group group)
        {
            bool res = false;

            if (group.Description == null) { group.Description = ""; }
            if (group.IsActive == false) { group.IsActive = true; }

            try
            {
                res = aGroupManager.SaveGroup(group);
                return Json(res);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }

        }
        public JsonResult SaveBrand(Brand brand)
        {
            bool res = false;

            if (brand.Description == null) { brand.Description = ""; }
            if (brand.IsActive == false) { brand.IsActive = true; }

            try
            {
                res = aBrandManager.SaveBrand(brand);
                return Json(res);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }

        }
        public JsonResult SaveType(ProductType type)
        {
            bool res = false;

            if (type.Description == null) { type.Description = ""; }
            if (type.IsActive == false) { type.IsActive = true; }

            try
            {
                res = aTypeManager.SaveProductType(type);
                return Json(res);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }

        }
        public JsonResult SaveCategory(Category category)
        {
            bool res = false;

            if (category.Description == null) { category.Description = ""; }
            if (category.IsActive == false) { category.IsActive = true; }

            try
            {
                res = aCategoryManager.SaveCategory(category);
                return Json(res);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }

        }
        public JsonResult SaveProduct(List<Product> ProductList)
        {
            bool res = false;

            try
            {
                foreach (Product product in ProductList)
                {
                    res = aProductManager.SaveProduct(product);
                }

                return Json(res);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }

        }

    }
}