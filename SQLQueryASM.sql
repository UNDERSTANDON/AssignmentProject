Create Database ASM

go 
use ASM

go 
CREATE TABLE Role (
    Role_ID INT PRIMARY KEY,
    Authority VARCHAR(50)
);

go
CREATE TABLE Category (
    Category_ID INT PRIMARY KEY,
    Category_Name VARCHAR(50)
);

go
CREATE TABLE Products (
    Prod_ID INT PRIMARY KEY,
    Prod_Name VARCHAR(50),
    Prod_Price DECIMAL(10, 2),
    Prod_Quantity INT DEFAULT 0,
    Category_ID INT,
    FOREIGN KEY (Category_ID) REFERENCES Category(Category_ID)
);

go
CREATE TABLE Employee (
    Employee_ID INT PRIMARY KEY,
    Employee_Name VARCHAR(50),
    Position VARCHAR(50),
    Phone VARCHAR(15),
    Address VARCHAR(255),
    Email VARCHAR(100),
    Role_ID INT,
    FOREIGN KEY (Role_ID) REFERENCES Role(Role_ID)
);

go
CREATE TABLE Customer (
    Cus_ID INT PRIMARY KEY,
    Cus_Name VARCHAR(50),
    Phone VARCHAR(15),
    Address VARCHAR(255),
    Email VARCHAR(100)
);

go
CREATE TABLE ProductsImage (
    Image_ID INT PRIMARY KEY,
    Image_Path VARCHAR(255),
    Is_Existed BIT DEFAULT 0,
    Prod_ID INT,
    FOREIGN KEY (Prod_ID) REFERENCES Products(Prod_ID)
);

CREATE TABLE CustomersImage (
    Image_ID INT PRIMARY KEY,
    Image_Path VARCHAR(255),
    Is_Existed BIT DEFAULT 0,
    Cus_ID INT,
    FOREIGN KEY (Cus_ID) REFERENCES Customer(Cus_ID)
);

CREATE TABLE EmployeesImage (
    Image_ID INT PRIMARY KEY,
    Image_Path VARCHAR(255),
    Is_Existed BIT DEFAULT 0,
    Employee_ID INT,
    FOREIGN KEY (Employee_ID) REFERENCES Employee(Employee_ID)
);

go
CREATE TABLE Shipping(
    Ship_ID INT PRIMARY KEY,
    Estimate_Date DATE,
    Ship_Type VARCHAR(15),
    Shipping_Cost DECIMAL(10, 2),
    Current_Location VARCHAR(100),
    Shipping_Status VARCHAR(20)
)

go
CREATE TABLE Orders (
    Order_ID INT PRIMARY KEY,
    salesman_Status VARCHAR(50),
    Quantity INT DEFAULT 1,
    Cost DECIMAL(10, 2),
    Is_Done BIT DEFAULT 0,
    Ship_ID INT,
    Employee_ID INT,
    Cus_ID INT,
    Order_Date DATE,
    FOREIGN KEY (Employee_ID) REFERENCES Employee(Employee_ID),
    FOREIGN KEY (Cus_ID) REFERENCES Customer(Cus_ID),
    FOREIGN KEY (Ship_ID) REFERENCES Shipping(Ship_ID)
);


go
CREATE TABLE Order_Products (
    Order_ID INT,
    Prod_ID INT,
    PRIMARY KEY (Order_ID, Prod_ID),
    FOREIGN KEY (Order_ID) REFERENCES Orders(Order_ID),
    FOREIGN KEY (Prod_ID) REFERENCES Products(Prod_ID)
);

go
CREATE TABLE UserAccount (
    UserAccount_ID INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Employee_ID INT NULL,
    Cus_ID INT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 0,
    FOREIGN KEY (Employee_ID) REFERENCES Employee(Employee_ID),
    FOREIGN KEY (Cus_ID) REFERENCES Customer(Cus_ID)
);

go
-- Role
INSERT INTO Role (Role_ID, Authority) 
VALUES (1, 'Admin');

INSERT INTO Role (Role_ID, Authority) 
VALUES (2, 'Sales');

INSERT INTO Role (Role_ID, Authority) 
VALUES (3, 'Warehouse');

go
-- Employee
INSERT INTO Employee (Employee_ID, Employee_Name, Position, Phone, Address, Email, Role_ID) 
VALUES (1, 'Alice Johnson', 'Admin', '123456789', '123 Main St', 'alice@example.com', 1);

INSERT INTO Employee (Employee_ID, Employee_Name, Position, Phone, Address, Email, Role_ID) 
VALUES (2, 'Bob Smith', 'Salesman', '234567890', '456 Market St', 'bob@example.com', 2);

INSERT INTO Employee (Employee_ID, Employee_Name, Position, Phone, Address, Email, Role_ID) 
VALUES (3, 'Carol White', 'Manager', '345678901', '789 Broadway', 'carol@example.com', 3);

INSERT INTO Employee (Employee_ID, Employee_Name, Position, Phone, Address, Email, Role_ID) 
VALUES (7, 'Ethan Clark', 'Salesman', '444555666', '12 Elm St', 'ethan@example.com', 2);

INSERT INTO Employee (Employee_ID, Employee_Name, Position, Phone, Address, Email, Role_ID) 
VALUES (8, 'Mia Scott', 'Salesman', '555666777', '34 Pine St', 'mia@example.com', 2);

INSERT INTO Employee (Employee_ID, Employee_Name, Position, Phone, Address, Email, Role_ID) 
VALUES (9, 'Lucas Adams', 'Warehouse', '666777888', '56 Oak St', 'lucas@example.com', 3);

INSERT INTO Employee (Employee_ID, Employee_Name, Position, Phone, Address, Email, Role_ID) 
VALUES (10, 'Charlotte Baker', 'Admin', '777888999', '78 Maple St', 'charlotte@example.com', 1);

-- Add new Employees
INSERT INTO Employee (Employee_ID, Employee_Name, Position, Phone, Address, Email, Role_ID) VALUES
(11, 'Grace Foster', 'Salesman', '555333444', '56 Willow Dr', 'grace.foster@example.com', 2);

INSERT INTO Employee (Employee_ID, Employee_Name, Position, Phone, Address, Email, Role_ID) VALUES
(12, 'Logan Reed', 'Warehouse', '555444555', '78 Oak Blvd', 'logan.reed@example.com', 3);

go
-- Add employees for Admin, Salesman, and Warehouse (Fast testing)
INSERT INTO Employee (Employee_ID, Employee_Name, Position, Phone, Address, Email, Role_ID)
VALUES (4, 'Test Admin', 'Admin', '111111111', '1 Admin St', 'admin@test.com', 1);

INSERT INTO Employee (Employee_ID, Employee_Name, Position, Phone, Address, Email, Role_ID) 
VALUES (5, 'Test Salesman', 'Salesman', '222222222', '2 Sales St', 'sales@test.com', 2); 

INSERT INTO Employee (Employee_ID, Employee_Name, Position, Phone, Address, Email, Role_ID) 
VALUES (6, 'Test Warehouse', 'Warehouse', '333333333', '3 Warehouse St', 'warehouse@test.com', 3);

go
-- Customer
INSERT INTO Customer (Cus_ID, Cus_Name, Phone, Address, Email)
VALUES (1, 'David Brown', '456789012', '321 Oak St', 'david@example.com');

INSERT INTO Customer (Cus_ID, Cus_Name, Phone, Address, Email)
VALUES (2, 'Emma Green', '567890123', '654 Pine St', 'emma@example.com');

INSERT INTO Customer (Cus_ID, Cus_Name, Phone, Address, Email) 
VALUES (4, 'Sophia Turner', '678901234', '987 Maple St', 'sophia@example.com');

INSERT INTO Customer (Cus_ID, Cus_Name, Phone, Address, Email)
VALUES (5, 'Liam King', '789012345', '654 Cedar St', 'liam@example.com');

INSERT INTO Customer (Cus_ID, Cus_Name, Phone, Address, Email)
VALUES (6, 'Olivia Lee', '890123456', '321 Birch St', 'olivia@example.com');

-- Add a customer with Cus_ID = 1 for username 'c' (Fast testing)
INSERT INTO Customer (Cus_ID, Cus_Name, Phone, Address, Email)
VALUES (3, 'Test Customer', '123456789', '123 Test St', 'customer@test.com');

-- Add more Customers 
INSERT INTO Customer (Cus_ID, Cus_Name, Phone, Address, Email) VALUES
(7, 'Noah Harris', '901234567', '159 Spruce St', 'noah@example.com');

INSERT INTO Customer (Cus_ID, Cus_Name, Phone, Address, Email) VALUES
(8, 'Ava Walker', '123098456', '753 Willow St', 'ava@example.com');

INSERT INTO Customer (Cus_ID, Cus_Name, Phone, Address, Email) VALUES
(9, 'Mason Young', '234567890', '852 Aspen St', 'mason@example.com');

INSERT INTO Customer (Cus_ID, Cus_Name, Phone, Address, Email) VALUES
(10, 'Isabella Hall', '345678901', '951 Poplar St', 'isabella@example.com');

-- Add a customer who has not made any orders
INSERT INTO Customer (Cus_ID, Cus_Name, Phone, Address, Email) VALUES
(11, 'Henry Brooks', '456123789', '111 Cypress St', 'henry@example.com');

-- Add new Customers
INSERT INTO Customer (Cus_ID, Cus_Name, Phone, Address, Email) VALUES
(12, 'Ella Carter', '555111222', '12 Magnolia St', 'ella.carter@example.com');

INSERT INTO Customer (Cus_ID, Cus_Name, Phone, Address, Email) VALUES
(13, 'Jack Evans', '555222333', '34 Cypress Ave', 'jack.evans@example.com');

go
-- UserAccount
-- Employee
INSERT INTO UserAccount (Username, PasswordHash, Employee_ID, IsActive) VALUES
('alice', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 1, 1);

INSERT INTO UserAccount (Username, PasswordHash, Employee_ID, IsActive) VALUES
('bob',   '8d969eef6ecad3c29a3a629280e686cff8ca7f8f3fdf8e6e6b6a6e7e6e6e6e6e6e', 2, 1);

-- Customer
INSERT INTO UserAccount (Username, PasswordHash, Cus_ID, IsActive) VALUES
('davidb', '2e2e7e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e', 1, 1);

INSERT INTO UserAccount (Username, PasswordHash, Cus_ID, IsActive) VALUES
('emmag', '7e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e6e', 2, 1);

-- Fast Testing
-- Customer
INSERT INTO UserAccount (Username, PasswordHash, Cus_ID, IsActive)
VALUES ('c', '9bf5bb3e4bd40e48b2a893d8928a13c0289f9a74404437ffc44070135c95f290', 3, 1); -- c123

-- Admin
INSERT INTO UserAccount (Username, PasswordHash, Employee_ID, IsActive)
VALUES ('a', '7c04837eb356565e28bb14e5a1dedb240a5ac2561f8ed318c54a279fb6a9665e', 4, 1); -- a123

-- Salesman
INSERT INTO UserAccount (Username, PasswordHash, Employee_ID, IsActive)
VALUES ('s', 'a9dcdc7159d1a9daae0ccc718f5dbc2bb1c61e38873abc8074b374473d4d00b9', 5, 1); -- s123

-- Warehouse
INSERT INTO UserAccount (Username, PasswordHash, Employee_ID, IsActive)
VALUES ('w', '69e3107c6c3ee5c621d8b1800cc01a22f33f837173725898add518b1d9d21cf5', 6, 1); -- w123

go
-- Category
INSERT INTO Category (Category_ID, Category_Name) VALUES
(1, 'Audio Devices'),
(2, 'Office Furniture'),
(3, 'Computer Accessories'),
(4, 'Home Lighting'),
(5, 'Wearable Tech');

go
-- Products
INSERT INTO Products (Prod_ID, Prod_Name, Prod_Price, Category_ID) VALUES
(1, 'Bluetooth Speaker', 49.99, 1);

INSERT INTO Products (Prod_ID, Prod_Name, Prod_Price, Category_ID) VALUES
(2, 'Ergonomic Office Chair', 129.99, 2);

INSERT INTO Products (Prod_ID, Prod_Name, Prod_Price, Category_ID) VALUES
(3, 'USB-C Hub', 39.99, 3);

INSERT INTO Products (Prod_ID, Prod_Name, Prod_Price, Category_ID) VALUES
(4, 'Smart Desk Lamp', 69.99, 4);

INSERT INTO Products (Prod_ID, Prod_Name, Prod_Price, Category_ID) VALUES
(5, 'Noise-Canceling Earbuds', 89.99, 5);

INSERT INTO Products (Prod_ID, Prod_Name, Prod_Price, Category_ID) VALUES
(6, 'Wireless Mouse', 25.99, 3);

INSERT INTO Products (Prod_ID, Prod_Name, Prod_Price, Category_ID) VALUES
(7, 'LED Desk Light', 34.99, 4);

INSERT INTO Products (Prod_ID, Prod_Name, Prod_Price, Category_ID) VALUES
(8, 'Smartwatch', 199.99, 5);

INSERT INTO Products (Prod_ID, Prod_Name, Prod_Price, Category_ID) VALUES
(9, 'Conference Speakerphone', 149.99, 1);

INSERT INTO Products (Prod_ID, Prod_Name, Prod_Price, Category_ID) VALUES
(10, 'Standing Desk', 299.99, 2);

go
-- Shipping
INSERT INTO Shipping (Ship_ID, Estimate_Date, Ship_Type, Shipping_Cost, Current_Location, Shipping_Status) VALUES
(1, '2025-06-01', 'Standard', 10.00, 'Warehouse', 'Pending');

INSERT INTO Shipping (Ship_ID, Estimate_Date, Ship_Type, Shipping_Cost, Current_Location, Shipping_Status) VALUES
(2, '2025-06-02', 'Express', 20.00, 'In Transit', 'Shipped');

INSERT INTO Shipping (Ship_ID, Estimate_Date, Ship_Type, Shipping_Cost, Current_Location, Shipping_Status) VALUES
(3, '2025-06-03', 'Standard', 12.00, 'Customer Address', 'Delivered');

INSERT INTO Shipping (Ship_ID, Estimate_Date, Ship_Type, Shipping_Cost, Current_Location, Shipping_Status) VALUES
(4, '2025-06-04', 'Express', 18.00, 'Sorting Center', 'In Transit');

INSERT INTO Shipping (Ship_ID, Estimate_Date, Ship_Type, Shipping_Cost, Current_Location, Shipping_Status) VALUES
(5, '2025-06-05', 'Standard', 8.00, 'Warehouse', 'Pending');

-- Add more Shipping
INSERT INTO Shipping (Ship_ID, Estimate_Date, Ship_Type, Shipping_Cost, Current_Location, Shipping_Status) VALUES
(6, '2024-06-06', 'Express', 15.00, 'Customer Address', 'Delivered');

INSERT INTO Shipping (Ship_ID, Estimate_Date, Ship_Type, Shipping_Cost, Current_Location, Shipping_Status) VALUES
(7, '2024-06-07', 'Standard', 9.00, 'Warehouse', 'Pending');

INSERT INTO Shipping (Ship_ID, Estimate_Date, Ship_Type, Shipping_Cost, Current_Location, Shipping_Status) VALUES
(8, '2024-06-08', 'Express', 22.00, 'In Transit', 'Shipped');

INSERT INTO Shipping (Ship_ID, Estimate_Date, Ship_Type, Shipping_Cost, Current_Location, Shipping_Status) VALUES
(9, '2024-06-09', 'Standard', 11.00, 'Sorting Center', 'In Transit');

INSERT INTO Shipping (Ship_ID, Estimate_Date, Ship_Type, Shipping_Cost, Current_Location, Shipping_Status) VALUES
(10, '2024-06-10', 'Express', 20.00, 'Customer Address', 'Delivered');

-- Add new Shipping
INSERT INTO Shipping (Ship_ID, Estimate_Date, Ship_Type, Shipping_Cost, Current_Location, Shipping_Status) VALUES
(11, '2024-06-11', 'Standard', 13.00, 'Warehouse', 'Pending');

INSERT INTO Shipping (Ship_ID, Estimate_Date, Ship_Type, Shipping_Cost, Current_Location, Shipping_Status) VALUES
(12, '2024-06-12', 'Express', 21.00, 'In Transit', 'Shipped');

go
-- Orders
-- Only use Cus_IDs, Ship_IDs, Employee_IDs, and Order_IDs that exist in their respective tables
INSERT INTO Orders (Order_ID, salesman_Status, Quantity, Cost, Is_Done, Ship_ID, Employee_ID, Cus_ID, Order_Date) VALUES
(1, 'Completed', 2, 99.98, 1, 1, 2, 1, '2025-06-01');

INSERT INTO Orders (Order_ID, salesman_Status, Quantity, Cost, Is_Done, Ship_ID, Employee_ID, Cus_ID, Order_Date) VALUES
(2, 'Pending', 1, 129.99, 0, 2, 5, 2, '2025-06-02');

INSERT INTO Orders (Order_ID, salesman_Status, Quantity, Cost, Is_Done, Ship_ID, Employee_ID, Cus_ID, Order_Date) VALUES
(3, 'Completed', 3, 299.97, 1, 3, 2, 3, '2025-06-03');

INSERT INTO Orders (Order_ID, salesman_Status, Quantity, Cost, Is_Done, Ship_ID, Employee_ID, Cus_ID, Order_Date) VALUES
(4, 'Completed', 1, 25.99, 1, 4, 7, 4, '2025-06-04');

INSERT INTO Orders (Order_ID, salesman_Status, Quantity, Cost, Is_Done, Ship_ID, Employee_ID, Cus_ID, Order_Date) VALUES
(5, 'Pending', 2, 299.99, 0, 5, 8, 5, '2025-06-05');

INSERT INTO Orders (Order_ID, salesman_Status, Quantity, Cost, Is_Done, Ship_ID, Employee_ID, Cus_ID, Order_Date) VALUES
(6, 'Completed', 1, 199.99, 1, 1, 2, 6, '2025-06-06');

INSERT INTO Orders (Order_ID, salesman_Status, Quantity, Cost, Is_Done, Ship_ID, Employee_ID, Cus_ID, Order_Date) VALUES
(7, 'Completed', 1, 49.99, 1, 7, 3, 7, '2025-06-07');

INSERT INTO Orders (Order_ID, salesman_Status, Quantity, Cost, Is_Done, Ship_ID, Employee_ID, Cus_ID, Order_Date) VALUES
(8, 'Pending', 3, 89.99, 0, 8, 4, 8, '2025-06-08');

INSERT INTO Orders (Order_ID, salesman_Status, Quantity, Cost, Is_Done, Ship_ID, Employee_ID, Cus_ID, Order_Date) VALUES
(9, 'Completed', 2, 129.99, 1, 9, 5, 9, '2025-06-09');

INSERT INTO Orders (Order_ID, salesman_Status, Quantity, Cost, Is_Done, Ship_ID, Employee_ID, Cus_ID, Order_Date) VALUES
(10, 'Completed', 1, 39.99, 1, 10, 6, 10, '2025-06-10');

INSERT INTO Orders (Order_ID, salesman_Status, Quantity, Cost, Is_Done, Ship_ID, Employee_ID, Cus_ID, Order_Date) VALUES
(11, 'Completed', 2, 59.98, 1, 11, 11, 12, '2025-06-11');

INSERT INTO Orders (Order_ID, salesman_Status, Quantity, Cost, Is_Done, Ship_ID, Employee_ID, Cus_ID, Order_Date) VALUES
(12, 'Pending', 1, 129.99, 0, 12, 8, 13, '2025-06-12');

go
-- Order_Products
INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(1, 1);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(1, 3);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(2, 2);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(3, 8);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(3, 5);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(4, 6);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(5, 10);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(5, 7);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(6, 8);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(6, 9);

-- Add more Order_Products
INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(4, 2);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(7, 1);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(8, 3);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(9, 4);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(10, 5);

-- Add more Order_Products for Employee_ID 2 (who handles Order_IDs 1, 3, 6)
-- We'll add more products to Order_ID 6 (Employee_ID 2) to ensure >10 sales
INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(6, 1);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(6, 2);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(6, 3);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(6, 4);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(6, 5);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(6, 6);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(6, 7);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(6, 10);

go
-- Add new Order_Products (unique pairs)
INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(11, 2);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(11, 6);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(12, 4);

INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES
(12, 9);

go
-- Product Image 
INSERT INTO ProductsImage (Image_ID, Image_Path, Is_Existed, Prod_ID) VALUES
(1, 'C:/ASMDB/Product_Image/Speaker.jpg', 1, 1);

INSERT INTO ProductsImage (Image_ID, Image_Path, Is_Existed, Prod_ID) VALUES
(2, 'C:/ASMDB/Product_Image/Chair.jpg', 1, 2);

INSERT INTO ProductsImage (Image_ID, Image_Path, Is_Existed, Prod_ID) VALUES
(3, 'C:/ASMDB/Product_Image/UGREEN-USB-C-Hub.png', 1, 3);

INSERT INTO ProductsImage (Image_ID, Image_Path, Is_Existed, Prod_ID) VALUES
(4, 'C:/ASMDB/Product_Image/opple_Smart_Deck_Lamp.jpg', 1, 4);

INSERT INTO ProductsImage (Image_ID, Image_Path, Is_Existed, Prod_ID) VALUES
(5, 'C:/ASMDB/Product_Image/Earbuds.jpg', 1, 5);

INSERT INTO ProductsImage (Image_ID, Image_Path, Is_Existed, Prod_ID) VALUES
(6, 'C:/ASMDB/Product_Image/Conference Speakphone.jpg', 1, 6);

INSERT INTO ProductsImage (Image_ID, Image_Path, Is_Existed, Prod_ID) VALUES
(7, 'C:/ASMDB/Product_Image/Smart watch.jpg', 1, 7);

INSERT INTO ProductsImage (Image_ID, Image_Path, Is_Existed, Prod_ID) VALUES
(8, 'C:/ASMDB/Product_Image/LED Desk Lamp.jpg', 1, 8);

INSERT INTO ProductsImage (Image_ID, Image_Path, Is_Existed, Prod_ID) VALUES
(9, 'C:/ASMDB/Product_Image/Wireless mouse.jpg', 1, 9);

INSERT INTO ProductsImage (Image_ID, Image_Path, Is_Existed, Prod_ID) VALUES
(10, 'C:/ASMDB/Product_Image/Standing Desk.jpg', 1, 10);