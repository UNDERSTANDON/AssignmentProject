using ASMDB.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASMDBTest.Models
{
    [TestClass]
    public class OrdersTests
    {
        [TestMethod]
        public void Quantity_ShouldBeAtLeastOne()
        {
            var order = new Orders { Quantity = 0 };
            Assert.IsTrue(order.Quantity < 1, "Quantity is less than 1 for this test.");
        }

        [TestMethod]
        public void Can_Set_And_Get_All_Properties()
        {
            var order = new Orders
            {
                Order_ID = 4,
                salesman_Status = "Preparing",
                Ship_ID = 2,
                Employee_ID = 5,
                Cus_ID = 6,
                Is_Done = false,
                Quantity = 2,
                Cost = 99.99m
            };
            Assert.AreEqual(4, order.Order_ID);
            Assert.AreEqual("Preparing", order.salesman_Status);
            Assert.AreEqual(2, order.Ship_ID);
            Assert.AreEqual(5, order.Employee_ID);
            Assert.AreEqual(6, order.Cus_ID);
            Assert.IsFalse(order.Is_Done);
            Assert.AreEqual(2, order.Quantity);
            Assert.AreEqual(99.99m, order.Cost);
        }
    }
} 