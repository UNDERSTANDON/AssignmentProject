using System;

namespace ASMDB.Models
{
    public class Shipping
    {
        public int Ship_ID { get; set; }
        public DateTime Estimate_Date { get; set; }
        public string Ship_Type { get; set; }
        public decimal Shipping_Cost { get; set; }
        public string Current_Location { get; set; }
        public string Shipping_Status { get; set; }
        public bool IsCompleted => string.Equals(Shipping_Status, "Delivered", StringComparison.OrdinalIgnoreCase);
    }
}