using ASMDB.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ASMDBTest.Models
{
    [TestClass]
    public class UserAccountsTests
    {
        [TestMethod]
        public void Should_Have_EmployeeID_Or_CusID()
        {
            var user = new UserAccounts { Employee_ID = null, Cus_ID = null };
            Assert.IsNull(user.Employee_ID);
            Assert.IsNull(user.Cus_ID);
        }

        [TestMethod]
        public void Can_Set_And_Get_All_Properties()
        {
            var now = DateTime.Now;
            var user = new UserAccounts
            {
                UserAccount_ID = 10,
                Username = "testuser",
                PasswordHash = "hash123",
                Employee_ID = 1,
                Cus_ID = null,
                CreatedAt = now,
                IsActive = true
            };
            Assert.AreEqual(10, user.UserAccount_ID);
            Assert.AreEqual("testuser", user.Username);
            Assert.AreEqual("hash123", user.PasswordHash);
            Assert.AreEqual(1, user.Employee_ID);
            Assert.IsNull(user.Cus_ID);
            Assert.AreEqual(now, user.CreatedAt);
            Assert.IsTrue(user.IsActive);
        }
    }
} 