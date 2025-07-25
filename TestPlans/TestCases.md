# ASMDB Application Test Cases

This document contains comprehensive test cases for the ASMDB WinForms application, covering all major user roles and flows: Admin, Employee (Salesman), Warehouse, Customer, Order, and Shipping management.

---

## 1. Login & Registration

| #  | Test Case Description | Steps | Expected Result |
|----|----------------------|-------|-----------------|
| 1  | Register as a new customer | Fill registration form with valid data | Account created, redirected to login |
| 2  | Register with missing fields | Leave one or more fields empty | Validation error shown |
| 3  | Register with mismatched passwords | Enter different passwords | Validation error shown |
| 4  | Login as admin with valid credentials | Enter correct admin username/password | Admin dashboard opens |
| 5  | Login as employee with valid credentials | Enter correct employee username/password | Employee dashboard opens |
| 6  | Login as warehouse staff | Enter correct warehouse username/password | Warehouse dashboard opens |
| 7  | Login as customer | Enter correct customer username/password | Customer dashboard opens |
| 8  | Login with invalid credentials | Enter wrong username/password | Error message shown |
| 9  | First-time employee login (inactive) | Login as new employee | Prompt for password reset |

---

## 2. Admin Management

| #  | Test Case Description | Steps | Expected Result |
|----|----------------------|-------|-----------------|
| 10 | Add a new customer | Admin > Manage Customers > Add | Customer appears in list |
| 11 | Update customer details | Admin > Manage Customers > Update | Changes saved and visible |
| 12 | Delete a customer | Admin > Manage Customers > Remove | Customer removed from list |
| 13 | Add a new employee | Admin > Manage Employees > Add | Employee appears in list |
| 14 | Update employee details | Admin > Manage Employees > Update | Changes saved and visible |
| 15 | Delete an employee | Admin > Manage Employees > Remove | Employee removed from list |

---

## 3. Employee (Salesman) Management

| #  | Test Case Description | Steps | Expected Result |
|----|----------------------|-------|-----------------|
| 16 | View all products | Employee dashboard loads | Product grid displays all products |
| 17 | Search for a product by name | Enter product name in search | Filtered results shown |
| 18 | Update product details | Select product > Update | Changes saved and visible |
| 19 | Hide/show a product | Select product > Hide | Product status toggled |
| 20 | Mark down a product | Select product > Mark Down | Markdown status toggled |
| 21 | Accept a pending order | Pending Orders > Accept | Order status updated, assigned to employee |

---

## 4. Warehouse Management

| #  | Test Case Description | Steps | Expected Result |
|----|----------------------|-------|-----------------|
| 22 | Add a new product | Add New Item | Product appears in list |
| 23 | Update product quantity | Update Item | Quantity updated |
| 24 | Remove a product | Remove Item | Product removed from list |
| 25 | Search for product by ID | Enter ID in search | Filtered results shown |

---

## 5. Customer Shopping & Orders

| #  | Test Case Description | Steps | Expected Result |
|----|----------------------|-------|-----------------|
| 26 | Browse product catalog | Customer dashboard loads | Product cards displayed |
| 27 | Search for a product | Enter search term | Filtered products shown |
| 28 | Add product to cart | Click on product > Add to cart | Cart count increases |
| 29 | Remove product from cart | Cart > Remove | Product removed from cart |
| 30 | Purchase products in cart | Cart > Buy Now | Order form opens |
| 31 | Select shipping type | Order form > Choose shipping | Shipping fee updates |
| 32 | Complete purchase | Select items > Purchase | Order created, confirmation shown |
| 33 | View past orders | Past Orders | List of previous orders shown |

---

## 6. Shipping & Order Fulfillment

| #  | Test Case Description | Steps | Expected Result |
|----|----------------------|-------|-----------------|
| 34 | View pending shipments (customer) | Shipping Info | Pending shipments listed |
| 35 | Mark order as shipped (employee) | Accept order > Mark as shipped | Order status updated |
| 36 | Mark order as delivered (customer) | Shipping Info > Mark as delivered | Order status updated |
| 37 | View shipping details | Shipping Info > Details | Shipping info dialog opens |

---

## 7. Profile Management

| #  | Test Case Description | Steps | Expected Result |
|----|----------------------|-------|-----------------|
| 38 | View customer profile | Customer dashboard > Profile | Profile dialog opens |
| 39 | Update customer profile | Edit profile fields > Save | Changes saved |
| 40 | Change profile image | Select new image > Save | Image updated |
| 41 | Delete customer account | Profile > Delete Account | Account removed, logout |

---

## 8. General & Error Handling

| #  | Test Case Description | Steps | Expected Result |
|----|----------------------|-------|-----------------|
| 42 | Attempt to delete customer with active orders | Admin > Remove customer | Error or warning shown |
| 43 | Attempt to purchase with empty cart | Cart > Buy Now | Warning shown |
| 44 | Attempt to update with invalid data | Enter invalid input | Validation error shown |
| 45 | Database connection failure | Disconnect DB, start app | Error dialog, retry/cancel |

---

*This test plan covers the main user flows and edge cases for all major roles and features in the ASMDB application. Add more cases as needed for new features or bug fixes.* 