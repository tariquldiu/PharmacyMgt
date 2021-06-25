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
    public class PurchasesController : Controller
    {

        WarehouseManager aWarehouseManager = new WarehouseManager();
        ProductManager aProductManager = new ProductManager();
        PurchaseManager aPurchaseManager = new PurchaseManager();
        SaleManager aSaleManager = new SaleManager();
        CostManager aCostManager = new CostManager();
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
            var detailList = aPurchaseManager.GetDetails(startDate, endDate);
            var purchasesList = (from p in detailList
                                 select new
                                 { p.PurchaseId, p.ProductId, p.ProductName, p.WarehouseName, p.WarehouseId, p.GroupName, p.BrandName, p.TypeName, p.UnitPrice, p.Quantity, p.TotalQuantity, p.Measurement, p.Description, p.PurchaseDate, p.ManufacturingDate, p.ExpireDate, p.IsStock })
                                 .ToList();
            return Json(purchasesList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPurchaseByCostEntry() 
        {
            var purchases = aPurchaseManager.GetPurchaseByCostEntry();
            var purchasesList = (from p in purchases
                                 select new
                                 { p.PurchaseId, p.ProductId,p.ProductName, p.WarehouseName, p.WarehouseId,p.GroupName, p.BrandName,p.TypeName, p.UnitPrice, p.Quantity, p.Measurement, p.Description, p.PurchaseDate, p.ManufacturingDate, p.ExpireDate, p.IsStock })
                                 .ToList();
            return Json(purchasesList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllPurchase()
        {
            var purchases = aPurchaseManager.GetAllPurchase();
            var purchasesList = (from p in purchases
                                 select new
                                 { p.PurchaseId, p.ProductId, p.ProductName, p.WarehouseName, p.WarehouseId, p.GroupName, p.BrandName, p.TypeName, p.UnitPrice, p.Quantity, p.TotalQuantity, p.Measurement, p.Description, p.PurchaseDate, p.ManufacturingDate, p.ExpireDate, p.IsStock })
                                 .ToList();
            return Json(purchasesList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPurchaseInfoByCostType()
        {
            var purchaseInfos = aPurchaseManager.GetPurchaseInfoByCostType();
            var purchaseInfoList = (from p in purchaseInfos
                                    select new
                                    { p.PurchaseId, p.ProductId, p.ProductName, p.UnitPrice, p.Quantity, p.Measurement, p.Description, p.PurchaseDate, p.ManufacturingDate, p.ExpireDate, p.IsStock })
                                 .ToList();
            return Json(purchaseInfoList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllProduct()
        {
            var products = aProductManager.GetAllProduct();
            var productList = (from p in products
                               select new
                               { p.ProductId, p.ProductName, p.TypeId, p.GroupId, p.BrandId, p.CategoryId, p.IsActive })
                                 .ToList();
            return Json(productList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllWarehouse()
        {
            var warehouses = aWarehouseManager.GetAllWarehouse();
            var warehousesList = (from w in warehouses
                                  select new
                                  { w.WarehouseId, w.WarehouseName, w.WarehouseAddress })
                                 .ToList();
            return Json(warehousesList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SavePurchase(List<Purchase> PurchaseList)
        {
            bool res = false;

            try
            {
                foreach (Purchase purchase in PurchaseList)
                {
                    if (purchase.IsStock == false) { purchase.IsStock = true; }
                    if (purchase.Description == null) { purchase.Description = ""; }
                    if (purchase.ManufacturingDate == null) { purchase.ManufacturingDate = DateTime.Now; }
                    res = aPurchaseManager.SavePurchase(purchase);
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