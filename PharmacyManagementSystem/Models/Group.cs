using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Models
{
    public class Group
    {
        public int GroupId { get; set; } 
        public string GroupName { get; set; } 
        public string Description { get; set; }
        public bool IsActive { get; set; } 

    }
}