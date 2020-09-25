--Use WebApiDemoDB

--Create table Product(
--ProductId int IDENTITY(1,1) Primary Key, 
--ProductName varchar(225),
--Price decimal,
--AvailableQty int
--)


--Drop table Customer

--Create Table Customer
--(
--	CustomerId int Identity Primary key,
--	FirstName varchar(255),
--	LastName varchar(255),
--	BirthDate Datetime,
--	EmailId varchar(255),
--	PhoneNumber Bigint
--)


--Drop table Orders

--Create Table Orders
--(
--	OrderId int Identity Primary Key,
--	ProdId int Foreign Key References dbo.Product(ProductId),
--	CustId int Foreign Key References dbo.Customer(CustomerId),
--	OrderQty int
--)
