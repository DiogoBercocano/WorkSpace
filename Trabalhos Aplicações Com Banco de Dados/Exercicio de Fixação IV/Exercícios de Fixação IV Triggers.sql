--1
CREATE TABLE CarrosLogs (
 LogID INT IDENTITY PRIMARY KEY,
 CarroID INT NOT NULL,
 MarcaID INT NOT NULL,
 PrecoTabela DECIMAL(12,2) NOT NULL,
 Ano INT NOT NULL,
 Cor NVARCHAR(30),
 Modelo NVARCHAR(100),
 Ativo BIT NOT NULL,
 DataAlteracao DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);
	

-- 1.2
CREATE TRIGGER trg_Carro_Modelo_Upper
ON Carros
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Carros
    SET Modelo = UPPER(Modelo)
    WHERE CarroID IN (SELECT CarroID FROM inserted);
END;


-- 1.3
CREATE TRIGGER trg_Log_Alteracao_Carro
ON Carros
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO CarrosLogs (
        CarroID, MarcaID, PrecoTabela, Ano, Cor, Modelo, Ativo
    )
    SELECT
        i.CarroID,
        i.MarcaID,
        i.PrecoTabela,
        i.Ano,
        i.Cor,
        i.Modelo,
        i.Ativo
    FROM inserted i;
END;

