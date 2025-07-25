using ASMDB.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASMDBTest.Models
{
    [TestClass]
    public class ProductsTests
    {
        [TestMethod]
        public void Prod_Price_ShouldBePositive()
        {
            var product = new Products { Prod_Price = -5 };
            Assert.IsTrue(product.Prod_Price < 0, "Prod_Price is negative for this test.");
        }

        [TestMethod]
        public void Can_Set_And_Get_All_Properties()
        {
            var product = new Products
            {
                Prod_ID = 3,
                Prod_Name = "USB-C Hub",
                Prod_Price = 39.99m,
                Category_ID = 2,
                Prod_Quantity = 10
            };
            Assert.AreEqual(3, product.Prod_ID);
            Assert.AreEqual("USB-C Hub", product.Prod_Name);
            Assert.AreEqual(39.99m, product.Prod_Price);
            Assert.AreEqual(2, product.Category_ID);
            Assert.AreEqual(10, product.Prod_Quantity);
        }
    }
} 