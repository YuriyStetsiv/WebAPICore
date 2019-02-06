USE master;
GO

IF EXISTS(SELECT * from sys.databases WHERE name='Lab3DB')
	BEGIN
		DROP DATABASE [Lab3DB];
	END

CREATE DATABASE [Lab3DB]
GO

USE [Lab3DB];
GO

CREATE TABLE [Sages](
[Id] [int] NOT NULL IDENTITY Primary key,
[Name] [nvarchar](30) NOT NULL,
[Age] [int] NOT NULL,
[Photo] [binary](50),
)
GO

CREATE TABLE[Books](
[Id] [int] NOT NULL IDENTITY PRIMARY KEY,
[Name] [nvarchar](50) NOT NULL,
[Price][int] NOT NULL,
[Description] [nvarchar](500)
)

CREATE TABLE [SagesBooks](
[BookId] [int] NOT NULL,
[SageId] [int] NOT NULL,
Primary key(BookId,SageId)
)
GO


CREATE TABLE [Orders](
[Id] [int] NOT NULL IDENTITY Primary key,
[UserId] [nvarchar](128) NOT NULL,
[Location] [nvarchar](50),
[Status] [int] NOT NULL ,
[isCompleted] [bit] NOT NULL
)
GO

CREATE TABLE [BooksOrders](
[OrderId] [int] NOT NULL,
[BookId] [int] NOT NULL,
Primary key(OrderId,BookId)
)
GO

CREATE TABLE [UserCart](
[UserId] [nvarchar](128) NOT NULL,
[BookId] [int] NOT NULL ,
Primary key(UserId,BookId)
)
GO


ALTER TABLE [SagesBooks]
ADD CONSTRAINT  FK_ToSagesBooksFromSages FOREIGN KEY ("SageId")
REFERENCES Sages ("Id");
GO

ALTER TABLE [SagesBooks]
ADD CONSTRAINT  FK_ToSagesBooksFromBooks FOREIGN KEY ("BookId")
REFERENCES Books ("Id");
GO

ALTER TABLE [BooksOrders]
ADD CONSTRAINT  FK_ToBooksOrdersFromOrders FOREIGN KEY ("OrderId")
REFERENCES Orders ("Id");
GO

ALTER TABLE [BooksOrders]
ADD CONSTRAINT  FK_ToBooksOrdersFromBooks FOREIGN KEY ("BookId")
REFERENCES Books ("Id");
GO

ALTER TABLE [UserCart]
ADD CONSTRAINT  FK_ToSageCartFromBooks FOREIGN KEY ("BookId")
REFERENCES Books ("Id");
GO