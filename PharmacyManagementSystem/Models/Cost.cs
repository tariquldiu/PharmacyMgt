using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Models
{
    public class Cost
    {
        public int CostId { get; set; }
        public string CostType { get; set; }
        public int PurchaseId { get; set; } 
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; } 
        public int Quantity { get; set; }
        public string ProductName { get; set; }   
        public string CostDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CostDate { get; set; }
        public decimal Amount { get; set; } 
        public bool IsActive { get; set; }


    }
} 