namespace ASMDB.Models
{
    public class Products
    {
        public int Prod_ID { get; set; }
        public string Prod_Name { get; set; }
        public decimal Prod_Price { get; set; }
        public int Category_ID { get; set; }
        public int Prod_Quantity { get; set; }
    }
}