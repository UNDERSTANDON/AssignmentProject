# ASMDB Unit Test Cases (Pure Unit Tests)

This document lists **pure unit test cases** for the ASMDB project. These tests are intended to be automated in the `ASMDBTest` project using MSTest, and should not require a database or external dependencies.

---

## 1. Model Validation Tests

| #  | Description | Target | Expected Result |
|----|-------------|--------|----------------|
| 1  | Customer name should not be empty | Customers.Cus_Name | Validation fails if empty |
| 2  | All Customers properties can be set and retrieved | Customers | Properties match assigned values |
| 3  | Employee email can be set and retrieved | Employees.Email | Value is set and retrieved correctly |
| 4  | All Employees properties can be set and retrieved | Employees | Properties match assigned values |
| 5  | Product price must be positive | Products.Prod_Price | Validation fails if <= 0 |
| 6  | All Products properties can be set and retrieved | Products | Properties match assigned values |
| 7  | Order quantity must be >= 1 | Orders.Quantity | Validation fails if < 1 |
| 8  | All Orders properties can be set and retrieved | Orders | Properties match assigned values |
| 9  | User account must have either Employee_ID or Cus_ID | UserAccounts | Validation fails if both null |
| 10 | All UserAccounts properties can be set and retrieved | UserAccounts | Properties match assigned values |
| 11 | Shipping cost must be non-negative | Shipping.Shipping_Cost | Validation fails if < 0 |
| 12 | IsCompleted returns true for Delivered | Shipping.IsCompleted | Returns true if status is Delivered |
| 13 | All Shipping properties can be set and retrieved | Shipping | Properties match assigned values |
| 14 | All ProductImage properties can be set and retrieved | ProductImage | Properties match assigned values |
| 15 | All ProductsImage properties can be set and retrieved | ProductsImage | Properties match assigned values |

---

## 2. Business Logic & Utility Tests

| #  | Description | Target | Expected Result |
|----|-------------|--------|----------------|
| 16 | Password hash is consistent | DAL_Login.HashPassword | Same input returns same hash |
| 17 | Password hash is unique for different input | DAL_Login.HashPassword | Different input returns different hash |
| 18 | Cart total calculation (simple logic in test) | List<CartItem> | Returns correct total for items |
| 19 | Order cannot be completed with empty cart (simple logic in test) | List<CartItem> | Throws or returns error if cart is empty |
| 20 | Employee role assignment (property) | Employees.Role_ID | Property is set and retrieved correctly |
| 21 | Product markdown toggles status (property/logic) | Product.Markdown | Status toggles as expected |

---

*These unit test cases are intended for automation in the ASMDBTest project. They do not require a database or external dependencies. Some business logic tests use simple logic in the test file if no dedicated class exists. Expand or refine as needed for new features or bug fixes.* 