--CREATE DATABASE OrderManagementSystem

--USE OrderManagementSystem

--Products table will only contain details related to all the propducts
CREATE TABLE Products
(	
	ProductID INT IDENTITY(1,1) PRIMARY KEY,
	ProductName NVARCHAR(100),
	Weight INT,
	Height INT,
	SKU CHAR(8),
	Barcode CHAR(13),
	Available_Quantity INT
)

--Administrator table will have the details related to all the orders
CREATE TABLE Administrator
(
	AdminID INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(100),
	OrderID INT
)

--Buyer table contains records of buyers details and orders made by them
CREATE TABLE Buyer
(
	BuyerID INT IDENTITY(1,1) PRIMARY KEY,
	OrderID INT NULL, 
	Name NVARCHAR(50),
	Email NVARCHAR(100),
	Address NVARCHAR(200)
)

--Orders table contains records of all the product details ordered by the buyers
CREATE TABLE Orders
(
	OrderID INT IDENTITY(1,1) PRIMARY KEY,
	Product_ID INT,
	BuyerID INT ,
	Order_Status VARCHAR(11),	
)

--Inserting data into Products Table
INSERT INTO Products
VALUES
('Mobile',5,5,'12345678','23223112',5),
('Charger',3,3,'12765678','20103112',2),
('Tab',8,8,'39845678','01223112',4),
('Mac',12,12,'15495678','11003112',3),
('Ipad',10,10,'44345678','10103112',6);

--Inserting data into Buyer Table
INSERT INTO Buyer
VALUES
(1,'Harvey','harveyspecter@gmail.com','Street no.1 Berkleys Lane'),
(3,'Louis','louislane@gmail.com','Street no.3 Berkleys Lane'),
(2,'Donna','donnad@gmail.com','Street no.2 Berkleys Lane'),
(4,'Jessica','jessicapearson@gmail.com','Street no.4 Berkleys Lane');

--Inserting data into Orders
INSERT INTO Orders
VALUES
(2,1,'Placed'),
(5,1,'Placed'),
(3,2,'Approved'),
(4,3,'Cancelled'),
(5,4,'In Delivery');

--Creating a stored procedure for returning orders details for buyer
GO
CREATE PROCEDURE sp_getordersforBuyer @ID INT
AS
BEGIN
SELECT b.BuyerID, b.Name, b.Address, b.Email, o.OrderID, o.Order_Status FROM Buyer b LEFT JOIN Orders o ON b.BuyerID=o.BuyerID
WHERE b.BuyerID=@ID
END 

--Creating a stored procedure for returning all the orders
GO
CREATE PROCEDURE sp_getallOrders
AS
BEGIN
SELECT o.OrderID, o.BuyerID, p.ProductName, p.SKU, p.Barcode FROM Orders o LEFT JOIN Products p ON o.Product_ID=p.ProductID
END

EXEC sp_getordersforBuyer 1


--Creating a stored procedure to perform CRUD operations on orders
GO
CREATE PROCEDURE sp_Orders_CRUD
@OrderID INT,
@ProductID INT,
@Order_Status varchar(11),
@BuyerID INT,
@Option varchar(20)
AS
BEGIN
	if(@Option='Insert')
	BEGIN
	INSERT INTO Orders (Product_ID, BuyerID, Order_Status) VALUES (@ProductID,@BuyerID,@Order_Status)
	END
	if(@Option='Update')
	BEGIN
	UPDATE Orders SET Product_ID=@ProductID,BuyerID=@BuyerID,Order_Status=@Order_Status WHERE OrderID=@OrderID
	END
	if(@Option='Delete')
	BEGIN
	DELETE FROM Orders WHERE OrderID=@OrderID
	END
	if(@Option='GetOrder')
	BEGIN
	SELECT o.OrderID,o.Order_Status,b.BuyerID, b.Name,b.Address,p.ProductID,p.ProductName FROM Orders o 
	JOIN Products p ON o.Product_ID=p.ProductID 
	JOIN Buyer b ON o.BuyerID=b.BuyerID 
	WHERE o.OrderID=@OrderID
	END
	ELSE
	BEGIN
	SELECT o.OrderID,o.Order_Status,b.BuyerID, b.Name,b.Address,p.ProductID,p.ProductName  FROM Orders o
	JOIN Products p ON o.Product_ID=p.ProductID
	JOIN Buyer b ON o.BuyerID=b.BuyerID
	END
END	


