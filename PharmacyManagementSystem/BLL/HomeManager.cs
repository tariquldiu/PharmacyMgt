using PharmacyManagementSystem.Gateway;
using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.BLL
{
    public class HomeManager 
    {
        HomeGateway aHome = new HomeGateway();
        public List<Home> GetDetails(string startDate, string endDate)
        {
            return aHome.GetDetails(startDate, endDate);
        }
    }
}