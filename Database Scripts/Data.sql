Use WebApiDemoDB

Delete Product
Delete Customer

Insert into Product Values 
('Skirt', 500.00, 15, 'PCODE01'), 
('Tshirt', 250.00, 20, 'PCODE02'), 
('Jeans', 1000.00, 15, 'PCODE03'), 
('Frock', 800.00, 15, 'PCODE04')

Insert into Customer Values
('Vaani', 'Kankaria', '2019-8-12', 'vaani@abc.com', 4568972225, 'CUSTCODE01'),
('Ashish', 'Kankaria', '1987-1-13', 'ashish@abc.com', 7945632165, 'CUSTCODE02'),
('Manasi', 'Jain', '1988-1-5', 'manasi@abc.com', 4456972123, 'CUSTCODE03'),
('Ravi', 'Jain', '1958-1-12', 'ravi@abc.com', 3468975425, 'CUSTCODE04')


select * from Product --where ProductId = 1
select * from Customer-- where CustomerId = 1
select * from Orders-- where OrderId = 1

select Count(*) from Customer

Alter Table Product Add ProductCode varchar(255)
Alter Table Customer Add CustomerCode varchar(255)
Alter Table Orders Add OrderStatus varchar(255)

Select * from Customer where CustomerCode = 'CUSTCODE02'

Update Customer Set FirstName = 'Princy', PhoneNumber = 456987456 where CustomerCode = 'CUSTCODE06'

Insert into Orders Values 
(5, 5, 1, 'Delivered'),
(6, 5, 2, 'Delivered'),
(7, 5, 1, 'In Progress'),
(8, 5, 1, 'Cancelled')

select * from Customer as c
Inner Join Orders as o
ON c.CustomerId = o.CustId
Where c.CustomerId = 5


