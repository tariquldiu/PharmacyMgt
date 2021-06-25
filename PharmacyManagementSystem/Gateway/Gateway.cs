using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace PharmacyManagementSystem.Gateway
{
    public class Gateway
    {

        private string connectionString = WebConfigurationManager.ConnectionStrings["PharmacyCon"].ConnectionString;
        public SqlConnection Connection { get; set; }
        public Gateway() 
        {
            Connection = new SqlConnection(connectionString);
        }
    }
}