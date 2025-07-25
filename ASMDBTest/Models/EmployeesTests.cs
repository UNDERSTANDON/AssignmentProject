using ASMDB.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASMDBTest.Models
{
    [TestClass]
    public class EmployeesTests
    {
        [TestMethod]
        public void Can_Set_And_Get_Email()
        {
            var employee = new Employees { Email = "employee@example.com" };
            Assert.AreEqual("employee@example.com", employee.Email);
        }

        [TestMethod]
        public void Can_Set_And_Get_All_Properties()
        {
            var employee = new Employees
            {
                Employee_ID = 2,
                Employee_Name = "Jane Smith",
                Position = "Manager",
                Phone = "9876543210",
                Address = "456 Market St",
                Email = "jane@example.com",
                Role_ID = 1
            };
            Assert.AreEqual(2, employee.Employee_ID);
            Assert.AreEqual("Jane Smith", employee.Employee_Name);
            Assert.AreEqual("Manager", employee.Position);
            Assert.AreEqual("9876543210", employee.Phone);
            Assert.AreEqual("456 Market St", employee.Address);
            Assert.AreEqual("jane@example.com", employee.Email);
            Assert.AreEqual(1, employee.Role_ID);
        }
    }
} 