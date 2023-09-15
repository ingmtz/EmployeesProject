CREATE DATABASE Employees;

USE Employees;

CREATE TABLE Employees.dbo.Employees (
	Id int IDENTITY(1,1) NOT NULL,
	Name nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	LastName nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK_Employees PRIMARY KEY (Id)
);