using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PharmacyManagementSystem.Gateway
{
    public class WarehouseGateway: Gateway
    {
        public List<Warehouse> GetAllWarehouse() 
        {
             
            List<Warehouse> warehouseList = new List<Warehouse>();
            SqlCommand com = new SqlCommand("pha_GetAllWarehouse", Connection);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            try
            {
                Connection.Open();
                da.Fill(dt);
            }
            finally
            {
                Connection.Close();
            }

            warehouseList = (from DataRow dr in dt.Rows

                         select new Warehouse() 
                         {
                             WarehouseId = Convert.ToInt32(dr["WarehouseId"]),
                             WarehouseName = Convert.ToString(dr["WarehouseName"]),
                             WarehouseAddress = Convert.ToString(dr["WarehouseAddress"])

                         }).ToList();


            return warehouseList;
        }
    }
}