-- Create the database
CREATE DATABASE STGenetics;
GO

-- Use the STGenetics database
USE STGenetics;
GO

-- Create the Animal table
CREATE TABLE Animal (
  AnimalId INT PRIMARY KEY,
  Name VARCHAR(50),
  Breed VARCHAR(50),
  BirthDate DATE,
  Sex VARCHAR(6),
  Price DECIMAL(10,2),
  Status VARCHAR(10)
);
GO

-- Create the Order table

CREATE TABLE [Order]
(
    OrderId INT PRIMARY KEY IDENTITY,
    TotalPurchaseAmount DECIMAL(18, 2) NOT NULL,
    TotalQuantity INT NOT NULL
   
)
GO


CREATE TABLE OrderDetail
(
    OrderId INT NOT NULL,
    AnimalId INT NOT NULL,
    CONSTRAINT PK_OrderDetail PRIMARY KEY (OrderId, AnimalId)
)
GO


 