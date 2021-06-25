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
    public class CostsController : Controller
    {
        CostManager aCostManager = new CostManager();
        // GET: Costs
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
            var detailList = aCostManager.GetDetails(startDate, endDate);
            var costList = (from c in detailList
                            select new
                            { c.CostId, c.CostType, c.PurchaseId, c.CostDescription, c.CostDate, c.Amount, c.IsActive })
                                 .ToList();
            return Json(costList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllCost()
        {
            var costs = aCostManager.GetAllCost();
            var costList = (from c in costs
                               select new
                               {c.CostId, c.CostType, c.PurchaseId, c.CostDescription, c.CostDate, c.Amount, c.IsActive })
                                 .ToList();
            return Json(costList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveCost(List<Cost> CostList)
        {
            bool res = false;

            try
            {
                foreach (Cost cost in CostList)
                {
                    res = aCostManager.SaveCost(cost);
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