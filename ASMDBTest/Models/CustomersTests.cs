using ASMDB.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASMDBTest.Models
{
    [TestClass]
    public class CustomersTests
    {
        [TestMethod]
        public void Cus_Name_ShouldNotBeEmpty()
        {
            var customer = new Customers { Cus_Name = "" };
            Assert.IsTrue(string.IsNullOrEmpty(customer.Cus_Name), "Cus_Name should be empty for this test.");
        }

        [TestMethod]
        public void Can_Set_And_Get_All_Properties()
        {
            var customer = new Customers
            {
                Cus_ID = 1,
                Cus_Name = "John Doe",
                Phone = "1234567890",
                Address = "123 Main St",
                Email = "john@example.com"
            };
            Assert.AreEqual(1, customer.Cus_ID);
            Assert.AreEqual("John Doe", customer.Cus_Name);
            Assert.AreEqual("1234567890", customer.Phone);
            Assert.AreEqual("123 Main St", customer.Address);
            Assert.AreEqual("john@example.com", customer.Email);
        }
    }
} 