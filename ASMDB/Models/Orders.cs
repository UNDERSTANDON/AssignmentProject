namespace ASMDB.Models
{
    public class Orders
    {
        public int Order_ID { get; set; }
        public string salesman_Status { get; set; }
        public int Ship_ID { get; set; }
        public int Employee_ID { get; set; }
        public int Cus_ID { get; set; }
        public bool Is_Done { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public string Order_Date { get; set; }
    }
}