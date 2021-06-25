using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; } 
        public int ProductId { get; set; }
        public string ProductName { get; set; } 
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string BrandName { get; set; }
        public string GroupName { get; set; } 
        public string TypeName { get; set; } 
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int TotalQuantity { get; set; }
        public string Measurement { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsStock { get; set; }
        public bool IsActive { get; set; }

    }
}