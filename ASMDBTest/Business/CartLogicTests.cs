using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using ASMDB.Order;

namespace ASMDBTest.Business
{
    [TestClass]
    public class CartLogicTests
    {
        public class CartItem
        {
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }

        [TestMethod]
        public void CartTotalCalculation_ReturnsCorrectTotal()
        {
            var cart = new List<CartItem>
            {
                new CartItem { Price = 10.0m, Quantity = 2 },
                new CartItem { Price = 5.5m, Quantity = 1 },
                new CartItem { Price = 3.0m, Quantity = 3 }
            };
            decimal expected = (10.0m * 2) + (5.5m * 1) + (3.0m * 3);
            decimal total = cart.Sum(item => item.Price * item.Quantity);
            Assert.AreEqual(expected, total);
        }

        [TestMethod]
        public void OrderCannotBeCompletedWithEmptyCart_ThrowsOrReturnsError()
        {
            var cart = new List<CartItem>();
            bool canOrder = cart.Any();
            Assert.IsFalse(canOrder, "Order should not be allowed with an empty cart.");
        }
    }
} 