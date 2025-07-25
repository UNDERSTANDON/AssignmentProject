-- 1.I want to get a list of all orders along with the customer's name and phone number:
go
select o.Order_ID, c.Cus_Name, c.Phone
from Orders o 
join Customer c on o.Cus_ID = c.Cus_ID


-- 2. I want to see all products that were included in orders.
go
select op.Order_ID, p.Prod_Name
from Order_Products op
join Products p on op.Prod_ID = p.Prod_ID


-- 3. I want to check which employee (salesman) handled each order.
go
select o.Order_ID, e.Employee_Name
from Orders o
join Employee e on o.Employee_ID = e.Employee_ID


-- 4. I want to get all products, including their category name.
go
select p.Prod_Name, c.Category_Name
from Products p
join Category c on p.Category_ID = c.Category_ID


-- 5. I want to show all customers who made an order that included a product in the "Wearable Tech" category. 
go
select c.Cus_Name
from Customer c
join Orders o on c.Cus_ID = o.Cus_ID
join Order_Products op on o.Order_ID = op.Order_ID
join Products p on op.Prod_ID = p.Prod_ID
join Category ca on p.Category_ID = ca.Category_ID
where ca.Category_Name = 'Wearable Tech'

-- 6. I want to get all orders that have a total price greater than $100.
go
select o.Order_ID, sum(p.Prod_Price) as Total_Price
from Orders o
join Order_Products op on o.Order_ID = op.Order_ID
join Products p on op.Prod_ID = p.Prod_ID
group by o.Order_ID
having sum(p.Prod_Price) > 100

-- 7. I want to get all employees who have sold more than 10 products (by number of products, not quantity).
go
select e.Employee_Name, count(op.Prod_ID) as Total_Products_Sold
from Employee e
join Orders o on e.Employee_ID = o.Employee_ID
join Order_Products op on o.Order_ID = op.Order_ID
group by e.Employee_Name
having count(op.Prod_ID) > 10

-- 8. I want to get all orders that were shipped by Express.
go
select o.Order_ID, s.Ship_Type
from Orders o
join Shipping s on o.Ship_ID = s.Ship_ID
where s.Ship_Type = 'Express'

-- 9. I want to get all customers who have not made any orders.
go
select c.Cus_Name
from Customer c
left join Orders o on c.Cus_ID = o.Cus_ID
where o.Order_ID is null

-- 10. I want to get the most popular product (the one ordered the most times).
go
select top 1 p.Prod_Name, count(*) as Total_Ordered
from Order_Products op
join Products p on op.Prod_ID = p.Prod_ID
group by p.Prod_Name
order by Total_Ordered desc
