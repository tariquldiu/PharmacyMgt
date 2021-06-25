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
    public class HomeController : Controller
    {
        HomeManager aHomeManager = new HomeManager();
        public ActionResult Index()
        {
            return View();
        }
          
        public JsonResult GetDetails(string startDate, string endDate)
        {
            var detailList = aHomeManager.GetDetails(startDate, endDate);
            return Json(detailList, JsonRequestBehavior.AllowGet);
        }
    }
}