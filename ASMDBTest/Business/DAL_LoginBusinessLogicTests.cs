using ASMDB.Connection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASMDBTest.Business
{
    [TestClass]
    public class DAL_LoginBusinessLogicTests
    {
        [TestMethod]
        public void HashPassword_SameInput_ReturnsSameHash()
        {
            string password = "TestPassword123!";
            string hash1 = DAL_Login.HashPassword(password);
            string hash2 = DAL_Login.HashPassword(password);
            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void HashPassword_DifferentInput_ReturnsDifferentHash()
        {
            string password1 = "PasswordOne";
            string password2 = "PasswordTwo";
            string hash1 = DAL_Login.HashPassword(password1);
            string hash2 = DAL_Login.HashPassword(password2);
            Assert.AreNotEqual(hash1, hash2);
        }
    }
} 