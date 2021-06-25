using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Models
{
    public class ProductType
    { 
        public int TypeId { get; set; } 
        public string TypeName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}