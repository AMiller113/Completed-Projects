Create Database TherapyCenter
GO

USE TherapyCenter
GO

CREATE TABLE Customers
(
CustomerID int IDENTITY Primary Key,
FirstName varchar(50) NOT NULL,
LastName varchar(50) NOT NULL,
DateOfCreation Date Default GETDATE()
)
GO

CREATE TABLE ProvidedServices
(
ServiceName varchar(30),
ServiceProvider varchar(30),
ServiceCost decimal(10,2),
Primary Key(ServiceName,ServiceProvider)
)
GO

Create Table Merchandise
(
ItemID INT Primary Key IDENTITY,
ItemName varchar(20) NOT NULL,
ItemCost decimal(10,2)
)
GO

Create Table Orders
(
OrderID int Primary Key IDENTITY,
CustomerID int FOREIGN KEY REFERENCES Customers(CustomerID),
ItemID int FOREIGN KEY REFERENCES Merchandise(ItemID),
OrderStatus varchar(15),
DateOrdered Date Default GETDATE(),
PaymentType varchar(30),
Constraint CHK_OrderStatus CHECK (OrderStatus IN ('Processing','Confirmed','Shipped','Delivered'))
)
GO

Create Table Bookings
(
BookingID int Primary Key IDENTITY,
TimeOfAppointment DateTime,
AppointmentStatus varchar(20),
CustomerID int FOREIGN KEY REFERENCES Customers(CustomerID),
ServiceName varchar(30),
ServiceProvider varchar(30),
Constraint FK_sName_sProvider FOREIGN KEY (ServiceName,ServiceProvider) REFERENCES ProvidedServices(ServiceName, ServiceProvider),
Constraint CHK_appointStatus CHECK(AppointmentStatus IN ('Booked','Canceled','Completed')) 
)
GO