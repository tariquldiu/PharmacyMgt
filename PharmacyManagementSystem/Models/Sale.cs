using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public string BillNo { get; set; }
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } 
        public int Quantity { get; set; } 
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }  
        public decimal Discount { get; set; } 
        public string DiscountType { get; set; }
        public string SaleFor { get; set; } 
        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime SaleDate { get; set; }
        public bool IsActive { get; set; }


    }
}