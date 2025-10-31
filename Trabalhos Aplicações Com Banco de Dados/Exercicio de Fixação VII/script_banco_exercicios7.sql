IF DB_ID('Exercicios7') IS NULL CREATE DATABASE Exercicios7;
GO
USE Exercicios7;
GO

CREATE TABLE Contas (
  Id INT PRIMARY KEY IDENTITY,
  Nome NVARCHAR(15) UNIQUE,
  Saldo DECIMAL(10,2)
);

INSERT INTO Contas (Nome, Saldo) VALUES
('Ana', 1000),
('Bruno', 500),
('Carla', 2000);