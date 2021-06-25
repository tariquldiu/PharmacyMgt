using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Models
{
    public class Home
    {
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 
        public decimal MedicineCost { get; set; }
        public decimal OthersCost { get; set; } 
        public decimal TotalSale { get; set; } 
        public decimal TotalProfit { get; set; }
    }
}