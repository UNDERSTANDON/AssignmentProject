namespace ASMDB.Models
{
    public class PastOrderView
    {
        public int Order_ID { get; set; }
        public string ProductName { get; set; }
        public decimal Cost { get; set; }
        public decimal ShippingFee { get; set; }
        public string OrderDate { get; set; }
    }
}