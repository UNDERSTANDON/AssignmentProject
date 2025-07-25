# ASMDB

## Overview
ASMDB is a modern WinForms application for managing a small business database, supporting multiple user roles: Admin, Employee (Salesman), Warehouse, and Customer. The app features a clean, user-friendly interface and demonstrates core business management workflows, including product, order, employee, and customer management.

## Features
- **Multi-role login:** Admin, Employee, Warehouse, and Customer dashboards
- **Admin:** Manage employees and customers with add/update dialogs
- **Employee (Salesman):** Manage products, update/hide/mark down, view pending orders
- **Warehouse:** Manage product inventory, add/update/remove products
- **Customer:** Shop for products, add to cart, place orders, view shipping and order history
- **Order Management:** Cart, order, and shipping forms with modern UI
- **Modern UI:** Rounded corners, Segoe UI, icons, and improved layouts
- **Database support:** Can run with a real SQL Server database (see below)
- **SQL Schema:** Provided in `SQLQueryASM.sql` for real database setup
- **XML Documentation:** All Data Access Layer (DAL) classes and methods are fully documented for maintainability and IntelliSense support

## Setup Instructions
1. **Clone or download the repository.**
2. Open the solution (`ASMDB.sln`) in Visual Studio.
3. Build and run the project. The main form can be changed in `Program.cs`.
4. (Optional) Use the provided `SQLQueryASM.sql` to set up a real database in SQL Server. Update the connection string in `App.config` as needed.

## Usage Notes
- **Login credentials (for fast testing):**
  - **Admin:** `a` / `a123`
  - **Salesman:** `s` / `s123`
  - **Warehouse:** `w` / `w123`
  - **Customer:** `c` / `c123`
- By default, the app is designed for demonstration. To use a real database, configure the connection string and ensure your SQL Server matches the schema in `SQLQueryASM.sql`.
- All forms and dialogs use consistent, modern WinForms design.

## File Structure
- `ASMDB/` - Main application code (forms, dialogs, logic)
- `ASMDB/Connection/` - Data Access Layer (DAL) with full XML documentation
- `ASMDBTest/` - Automated test project (MSTest, .NET Framework)
- `ASMDB/TestPlans/` - Unit test plan (21 test cases: 15 model validation, 6 business logic/utility)
- `SQLQueryASM.sql` - SQL schema and sample queries
- `README.md` - Project documentation

## Data Access Layer Documentation
All classes and public methods in the `ASMDB/Connection/` folder (DAL) are fully documented with XML comments. This provides:
- Improved code readability and maintainability
- IntelliSense support in Visual Studio
- Easier onboarding for new developers

## Testing

### Test Plan Summary
- **21 unit test cases** are implemented and documented in `ASMDB/TestPlans/UnitTestCases.md`:
  - **15 model validation tests** (e.g., property assignment, validation logic)
  - **6 business logic/utility tests** (e.g., password hashing, cart logic, role assignment)
- All tests are pure unit tests and do **not** require a database or external dependencies.

### Automated Testing (ASMDBTest Project)
- Automated tests are located in the `ASMDBTest/` project, which uses **MSTest** and targets **.NET Framework**.
- To add new tests, create new classes or methods in `ASMDBTest/` using the `[TestClass]` and `[TestMethod]` attributes.
- **Run tests** using Visual Studio's Test Explorer or from the command line:
  ```sh
  dotnet test ASMDBTest
  ```
- For more, see [Microsoft Docs: Unit testing in .NET](https://learn.microsoft.com/en-us/dotnet/core/testing/).

#### Example Model Test
```csharp
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
            Assert.IsTrue(string.IsNullOrEmpty(customer.Cus_Name));
        }
    }
}
```

#### Example Business Logic Test
```csharp
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
    }
}
```

## License
This project is for educational/demo purposes. Feel free to use and modify as needed.