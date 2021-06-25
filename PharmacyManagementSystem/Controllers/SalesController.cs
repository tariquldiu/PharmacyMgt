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
    public class SalesController : Controller
    {
        PurchaseManager aPurchaseManager = new PurchaseManager();
        SaleManager aSaleManager = new SaleManager();
        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();  
        }
        public ActionResult Vouchar()
        {
            return View(); 
        }
        public JsonResult GetDetails(string startDate, string endDate)
        {
            var detailList = aSaleManager.GetDetails(startDate, endDate);
            var saleList = (from s in detailList
                            select new
                            { s.SaleId, s.ProductId, s.ProductName, s.PurchaseId, s.BillNo, s.UnitPrice, s.Quantity, s.Amount, s.Discount, s.DiscountType, s.SaleFor, s.SaleDate, s.CustomerName, s.CustomerContact, s.IsActive })
                                 .ToList();
            return Json(saleList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPurchaseForSaleEdit(int purchaseId, int saleId)
        {
            var saleInfos = aSaleManager.GetPurchaseForSaleEdit(purchaseId, saleId);
            var saleInfoList = (from s in saleInfos
                                    select new
                                    { s.SaleId, s.ProductId, s.ProductName, s.PurchaseId, s.BillNo, s.UnitPrice, s.Quantity, s.Amount, s.Discount, s.DiscountType, s.SaleFor, s.SaleDate, s.CustomerName, s.CustomerContact, s.IsActive })
                                .ToList();
            return Json(saleInfoList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLastSale()
        {
            var saleList = aSaleManager.GetLastSale();
            return Json(saleList,JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteSale(int saleId)
        {
            var isSaleDeleted = aSaleManager.DeleteSale(saleId); 
            return Json(isSaleDeleted, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBillNo(string saleDate)
        {
            var billNo = aSaleManager.GetBillNo(saleDate);
            return Json(billNo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllSale()
        {
            var sales = aSaleManager.GetAllSale();
            var saleList = (from s in sales
                            select new
                                    {s.SaleId, s.ProductId,s.ProductName, s.PurchaseId,s.BillNo, s.UnitPrice, s.Quantity,s.Amount, s.Discount,s.DiscountType, s.SaleFor, s.SaleDate, s.CustomerName, s.CustomerContact, s.IsActive })
                                 .ToList();
            return Json(saleList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPurchasedProduct()
        {
            var purchaseInfos = aPurchaseManager.GetPurchaseInfoByCostType();
            var purchaseInfoList = (from p in purchaseInfos
                                    select new
                                    { p.PurchaseId, p.ProductId, p.ProductName, p.UnitPrice, p.Quantity, p.Measurement, p.Description, p.PurchaseDate, p.ManufacturingDate, p.ExpireDate, p.IsStock })
                                 .ToList();
            return Json(purchaseInfoList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveSale(List<Sale> saleList)
        {
            bool res = false;

            try
            {   
                foreach(Sale sale in saleList)
                {
                    if (sale.SaleFor == null) { sale.SaleFor = ""; }
                    if (sale.CustomerName == null) { sale.CustomerName = ""; }
                    if (sale.CustomerContact == null) { sale.CustomerContact = ""; }
                    res = aSaleManager.SaveSale(sale);
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