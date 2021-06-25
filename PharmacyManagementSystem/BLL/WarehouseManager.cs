using PharmacyManagementSystem.Gateway;
using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PharmacyManagementSystem.BLL
{
    public class WarehouseManager
    {
        WarehouseGateway aWarehouse = new WarehouseGateway();
        public List<Warehouse> GetAllWarehouse() 
        {
            return aWarehouse.GetAllWarehouse(); 
        }
    }
}