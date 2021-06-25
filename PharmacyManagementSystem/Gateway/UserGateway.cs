using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Gateway
{
    public class UserGateway:Gateway
    {
        public List<User> GetAllUser()
        { 
             
            List<User> userList = new List<User>();
            SqlCommand com = new SqlCommand("pha_GetAllUser", Connection);
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

            userList = (from DataRow dr in dt.Rows

                         select new User()
                         {
                             UserId = Convert.ToInt32(dr["UserId"]),
                             UserName = Convert.ToString(dr["UserName"]),
                             Password = Convert.ToString(dr["Password"]),
                             RoleName = Convert.ToString(dr["RoleName"]),
                             IsActive = Convert.ToBoolean(dr["IsActive"])

                         }).ToList();


            return userList;
        }
    }
}