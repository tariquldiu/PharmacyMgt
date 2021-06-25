using PharmacyManagementSystem.Gateway;
using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.BLL
{
    public class GroupManager
    {
        GroupGateway aGroup = new GroupGateway();
        public List<Group> GetAllGroup()
        { 
            return aGroup.GetAllGroup();
        }
        public bool SaveGroup(Group group) 
        {
            bool res=aGroup.SaveGroup(group);
            return res;
        }
    }
}