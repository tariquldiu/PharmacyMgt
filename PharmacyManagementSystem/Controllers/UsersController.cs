using PharmacyManagementSystem.BLL;
using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PharmacyManagementSystem.Controllers
{
    public class UsersController : Controller
    {
        UserManager aUserManager = new UserManager();
       [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
       [HttpPost]
        public JsonResult Login(User u)
        {
            string lowerUsername = u.UserName.ToLower();
            string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(u.Password, "SHA1");
           try
            {
                u.Password = pass;
                var users = aUserManager.GetAllUser();
                var user = users.FirstOrDefault(x => x.UserName == lowerUsername && x.Password == u.Password && x.IsActive == true);
                if (user == null)
                {
                    var message = 0;
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(u.UserName, false);
                    var message = 1;
                    return Json(message, JsonRequestBehavior.AllowGet);
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
      
    }
}