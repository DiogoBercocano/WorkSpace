/* ==============================================================
   EXEMPLO BANCO OFICINA
   Banco: OficinaDB
   Tabelas: Clientes, Veiculos, Funcionarios, Servicos,
            OrdensServico, ItensOrdem, Pagamentos
   ============================================================== */

------------------------------------------------------------
-- 0) CRIAR/RECRIAR BANCO
------------------------------------------------------------
IF DB_ID('OficinaDB') IS NOT NULL
BEGIN
  ALTER DATABASE OficinaDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
  DROP DATABASE OficinaDB;
END
GO
CREATE DATABASE OficinaDB;
GO
USE OficinaDB;
GO

------------------------------------------------------------
-- 1) SCHEMA (7 TABELAS)
------------------------------------------------------------

-- 1.1 Clientes
CREATE TABLE Clientes (
  ClienteId  INT IDENTITY(1,1) PRIMARY KEY,
  Nome       NVARCHAR(100) NOT NULL,
  Telefone   NVARCHAR(20)
);

-- 1.2 Veiculos
CREATE TABLE Veiculos (
  VeiculoId  INT IDENTITY(1,1) PRIMARY KEY,
  ClienteId  INT NOT NULL,
  Placa      NVARCHAR(10) NOT NULL,
  Modelo     NVARCHAR(100),
  CONSTRAINT FK_Veiculos_Clientes
    FOREIGN KEY (ClienteId) REFERENCES Clientes(ClienteId)
);

-- 1.3 Funcionarios
CREATE TABLE Funcionarios (
  FuncionarioId INT IDENTITY(1,1) PRIMARY KEY,
  Nome          NVARCHAR(100),
  Cargo         NVARCHAR(50)
);

-- 1.4 Servicos
CREATE TABLE Servicos (
  ServicoId INT IDENTITY(1,1) PRIMARY KEY,
  Descricao NVARCHAR(200),
  Preco     DECIMAL(10,2)
);

-- 1.5 Ordens de Serviço
CREATE TABLE OrdensServico (
  OrdemId      INT IDENTITY(1,1) PRIMARY KEY,
  ClienteId    INT NOT NULL,
  VeiculoId    INT NOT NULL,
  DataAbertura DATE NOT NULL DEFAULT GETDATE(),
  Status       NVARCHAR(20) DEFAULT 'ABERTA',
  CONSTRAINT FK_Ordens_Clientes FOREIGN KEY (ClienteId) REFERENCES Clientes(ClienteId),
  CONSTRAINT FK_Ordens_Veiculos FOREIGN KEY (VeiculoId) REFERENCES Veiculos(VeiculoId)
);

-- 1.6 Itens da Ordem
CREATE TABLE ItensOrdem (
  ItemId    INT IDENTITY(1,1) PRIMARY KEY,
  OrdemId   INT NOT NULL,
  ServicoId INT NOT NULL,
  Quantidade INT DEFAULT 1,
  CONSTRAINT FK_ItensOrdem_Ordens FOREIGN KEY (OrdemId) REFERENCES OrdensServico(OrdemId),
  CONSTRAINT FK_ItensOrdem_Servicos FOREIGN KEY (ServicoId) REFERENCES Servicos(ServicoId)
);

-- 1.7 Pagamentos
CREATE TABLE Pagamentos (
  PagamentoId   INT IDENTITY(1,1) PRIMARY KEY,
  OrdemId       INT NOT NULL,
  Valor         DECIMAL(10,2),
  DataPagamento DATE,
  CONSTRAINT FK_Pagamentos_Ordens FOREIGN KEY (OrdemId) REFERENCES OrdensServico(OrdemId)
);

------------------------------------------------------------
-- 2) DADOS-BASE
------------------------------------------------------------
-- Clientes
INSERT INTO Clientes (Nome, Telefone) VALUES
('João Silva',  '9999-1111'),
('Maria Souza', '9888-2222'),
('Pedro Lima',  '9777-3333');

-- Veículos (cada um pertence a um cliente)
INSERT INTO Veiculos (ClienteId, Placa, Modelo) VALUES
(1, 'ABC-1234', 'Gol'),
(2, 'XYZ-5678', 'Civic'),
(3, 'JKL-9012', 'Onix');

-- Funcionários (não usados nos exercícios, mas compõem as 7 tabelas)
INSERT INTO Funcionarios (Nome, Cargo) VALUES
('Carla Menezes', 'Atendente'),
('Bruno Rocha',   'Mecânico');

-- Serviços (preços fixos para gabaritos de SELECT)
INSERT INTO Servicos (Descricao, Preco) VALUES
('Troca de óleo',               150.00),
('Alinhamento e balanceamento', 120.00),
('Pintura parcial',             800.00);

-- Ordem de serviço base (ABERTA) para gabarito de JOIN
INSERT INTO OrdensServico (ClienteId, VeiculoId, Status)
VALUES (1, 1, 'ABERTA');  -- OrdemId = 1 (esperado)

-- Itens da ordem base (para existir relação com serviços)
INSERT INTO ItensOrdem (OrdemId, ServicoId, Quantidade) VALUES
(1, 1, 1),   -- Troca de óleo
(1, 2, 1);   -- Alinhamento e balanceamento