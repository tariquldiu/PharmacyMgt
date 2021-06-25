using PharmacyManagementSystem.Gateway;
using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.BLL
{
    public class UserManager
    {
        UserGateway aUser = new UserGateway();
        public List<User> GetAllUser()
        {
            return aUser.GetAllUser();
        }

    }
}