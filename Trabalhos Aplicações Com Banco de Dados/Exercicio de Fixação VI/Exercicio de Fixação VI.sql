USE OficinaDB;


--1
INSERT INTO Clientes (Nome, Telefone) VALUES
('Lucas Andrade', '9666-4444'),
('Fernanda Costa', '9555-3333'),
('Rafael Oliveira', '9444-2222');

INSERT INTO Veiculos (ClienteId, Placa, Modelo) VALUES
(4, 'DEF-1111', 'Corolla'),
(5, 'GHI-2222', 'Fiesta'),
(6, 'JKM-3333', 'HB20');

INSERT INTO Servicos (Descricao, Preco) VALUES
('Troca de pneus', 400.00),
('Revisão completa', 900.00),
('Lavagem detalhada', 180.00);


--2
UPDATE Clientes
SET Telefone = '9000-0000'
WHERE Nome = 'Maria Souza';


--3
DELETE FROM Veiculos
WHERE Placa = 'GHI-2222'; 


--4
SELECT * FROM Clientes;


--5
SELECT 
  c.ClienteId,
  c.Nome,
  v.Placa,
  o.Status
FROM OrdensServico o
JOIN Clientes c ON o.ClienteId = c.ClienteId
JOIN Veiculos v ON o.VeiculoId = v.VeiculoId
WHERE o.Status = 'ABERTA';


--6
DECLARE @QtdServicos INT;
SET @QtdServicos = (SELECT COUNT(*) FROM Servicos);
SELECT @QtdServicos AS QtdTotalServicos;


-- 7
IF OBJECT_ID('sp_abrir_ordem') IS NOT NULL
    DROP PROCEDURE sp_abrir_ordem;


CREATE OR ALTER PROCEDURE sp_abrir_ordem
    @ClienteId INT,
    @VeiculoId INT
AS
BEGIN
    INSERT INTO OrdensServico (ClienteId, VeiculoId, Status)
    VALUES (@ClienteId, @VeiculoId, 'ABERTA');
END;


EXEC sp_abrir_ordem 2, 2;


-- 8
IF OBJECT_ID('trg_pagamento_atualiza_ordem') IS NOT NULL
    DROP TRIGGER trg_pagamento_atualiza_ordem;


CREATE OR ALTER TRIGGER trg_pagamento_atualiza_ordem
ON Pagamentos
AFTER INSERT
AS
BEGIN
    UPDATE OrdensServico
    SET Status = 'PAGA'
    WHERE OrdemId IN (SELECT OrdemId FROM inserted);
END;


--9
-- Estrutura básica de um cursor

declare @variavel int,
declare @variavel2 nvarchar(100);

--Declarar o cursor
declare nome_cursor cursor for
select ClienteId from 
Clientes

-- Abrir o cursor
open nome_cursor

-- Ler a linha da interação
fetch next from nome_cursor into @variavel

-- Estrutura de repetição loop
while @@FETCH_STATUS = 0
begin
	-- Ação exibir o valor
	print (variavel);
	--print concat(@variavel, '' variavel2);

	-- Próxima linha
	fetch next from nome_cursor into @variavel
end

-- Fechar o cursor
close nome_cursor
-- Desalocar o cursor
deallocate nome_cursor