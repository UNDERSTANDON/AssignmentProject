using ASMDB.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASMDBTest.Business
{
    [TestClass]
    public class EmployeeRoleLogicTests
    {
        [TestMethod]
        public void EmployeeRoleAssignment_PropertySetAndGet()
        {
            var employee = new Employees { Role_ID = 2 };
            Assert.AreEqual(2, employee.Role_ID);
        }
    }
} 