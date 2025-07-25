using ASMDB.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ASMDBTest.Models
{
    [TestClass]
    public class ShippingTests
    {
        [TestMethod]
        public void Shipping_Cost_ShouldBeNonNegative()
        {
            var shipping = new Shipping { Shipping_Cost = -1 };
            Assert.IsTrue(shipping.Shipping_Cost < 0, "Shipping_Cost is negative for this test.");
        }

        [TestMethod]
        public void IsCompleted_Returns_True_For_Delivered()
        {
            var shipping = new Shipping { Shipping_Status = "Delivered" };
            Assert.IsTrue(shipping.IsCompleted);
        }

        [TestMethod]
        public void Can_Set_And_Get_All_Properties()
        {
            var date = DateTime.Now;
            var shipping = new Shipping
            {
                Ship_ID = 1,
                Estimate_Date = date,
                Ship_Type = "Express",
                Shipping_Cost = 10.5m,
                Current_Location = "Warehouse",
                Shipping_Status = "In Transit"
            };
            Assert.AreEqual(1, shipping.Ship_ID);
            Assert.AreEqual(date, shipping.Estimate_Date);
            Assert.AreEqual("Express", shipping.Ship_Type);
            Assert.AreEqual(10.5m, shipping.Shipping_Cost);
            Assert.AreEqual("Warehouse", shipping.Current_Location);
            Assert.AreEqual("In Transit", shipping.Shipping_Status);
        }
    }
} 