using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Gateway
{
    public class GroupGateway: Gateway
    {
        public List<Group> GetAllGroup() 
        {
             
            List<Group> groupList = new List<Group>();
            SqlCommand com = new SqlCommand("pha_GetAllGroup", Connection);
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

            groupList = (from DataRow dr in dt.Rows

                         select new Group()
                         {
                             GroupId = Convert.ToInt32(dr["GroupId"]),
                             GroupName = Convert.ToString(dr["GroupName"]),
                             Description = Convert.ToString(dr["Description"])

                         }).ToList();


            return groupList; 
        }
        public bool SaveGroup(Group group) 
        {
            SqlCommand com = new SqlCommand("pha_SaveGroup", Connection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@GroupId", group.GroupId);
            com.Parameters.AddWithValue("@GroupName", group.GroupName);
            com.Parameters.AddWithValue("@Description", group.Description);
            com.Parameters.AddWithValue("@IsActive", group.IsActive);

            int i;
            try
            {
                Connection.Open();
                i = com.ExecuteNonQuery();
            }
            finally
            {
                Connection.Close();
            }
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }


        }
    }
}