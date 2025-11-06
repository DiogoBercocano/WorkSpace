-- 1
SELECT 
    a.id,
    p.nome AS paciente,
    v.nome AS vacina,
    u.nome AS unidade,
    a.data_aplicacao,
    a.dose_numero
FROM aplicacoes a
INNER JOIN pacientes p ON a.paciente_id = p.id
INNER JOIN vacinas v ON a.vacina_id = v.id
INNER JOIN unidades u ON a.unidade_id = u.id
WHERE a.data_aplicacao >= DATEADD(DAY, -30, CAST(GETDATE() AS DATE))
ORDER BY a.data_aplicacao DESC;
GO

-- 2
CREATE OR ALTER PROCEDURE sp_total_aplicacoes_paciente
    @p_paciente_id INT
AS
BEGIN
    SELECT COUNT(*) AS TOTAL_APLICACOES
    FROM aplicacoes
    WHERE paciente_id = @p_paciente_id;
END;
GO

EXEC sp_total_aplicacoes_paciente @p_paciente_id = 2;
GO

-- 3
CREATE OR ALTER PROCEDURE sp_listar_aplicacoes_paciente
    @p_paciente_id INT
AS
BEGIN
    SELECT 
        a.id,
        p.nome AS paciente,
        v.nome AS vacina,
        u.nome AS unidade,
        a.data_aplicacao,
        a.dose_numero
    FROM aplicacoes a
    INNER JOIN pacientes p ON a.paciente_id = p.id
    INNER JOIN vacinas v ON a.vacina_id = v.id
    INNER JOIN unidades u ON a.unidade_id = u.id
    WHERE a.paciente_id = @p_paciente_id
    ORDER BY a.data_aplicacao DESC;
END;
GO

EXEC sp_listar_aplicacoes_paciente @p_paciente_id = 1;
GO

-- 4
CREATE OR ALTER PROCEDURE sp_registrar_aplicacao
    @p_paciente_id INT,
    @p_vacina_id INT,
    @p_unidade_id INT,
    @p_data_aplicacao DATE,
    @p_lote NVARCHAR(50),
    @p_dose_numero INT,
    @p_observacao NVARCHAR(255)
AS
BEGIN
    INSERT INTO aplicacoes
        (paciente_id, vacina_id, unidade_id, data_aplicacao, lote, dose_numero, observacao)
    VALUES
        (@p_paciente_id, @p_vacina_id, @p_unidade_id, @p_data_aplicacao, @p_lote, @p_dose_numero, @p_observacao);
END;
GO

EXEC sp_registrar_aplicacao 
    @p_paciente_id = 1,
    @p_vacina_id = 2,
    @p_unidade_id = 1,
    @p_data_aplicacao = '2025-11-05',
    @p_lote = 'L1234',
    @p_dose_numero = 1,
    @p_observacao = 'Aplicação teste';
GO

-- 5
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='aplicacoes_audit' AND xtype='U')
BEGIN
    CREATE TABLE aplicacoes_audit (
        id INT IDENTITY PRIMARY KEY,
        aplicacao_id INT,
        paciente_id INT,
        vacina_id INT,
        unidade_id INT,
        data_aplicacao DATE,
        lote NVARCHAR(50),
        dose_numero INT,
        operacao NVARCHAR(10),
        audit_at DATETIME DEFAULT GETDATE()
    );
END;
GO

CREATE OR ALTER TRIGGER trg_ai_aplicacoes_auditoria
ON aplicacoes
AFTER INSERT
AS
BEGIN
    INSERT INTO aplicacoes_audit (
        aplicacao_id,
        paciente_id,
        vacina_id,
        unidade_id,
        data_aplicacao,
        lote,
        dose_numero,
        operacao,
        audit_at
    )
    SELECT 
        i.id,
        i.paciente_id,
        i.vacina_id,
        i.unidade_id,
        i.data_aplicacao,
        i.lote,
        i.dose_numero,
        'INSERT',
        GETDATE()
    FROM inserted i;
END;
GO

INSERT INTO aplicacoes (paciente_id, vacina_id, unidade_id, data_aplicacao, lote, dose_numero, observacao)
VALUES (2, 1, 1, '2025-11-05', 'L5555', 2, 'Aplicação teste trigger');
GO

SELECT * FROM aplicacoes_audit ORDER BY audit_at DESC;
GO

-- 6
PRINT '1) inserted';
PRINT '2) CREATE PROCEDURE';
PRINT '3) UNIQUE (cpf)';
PRINT '4) SELECT TOP 5 id, paciente_id, data_aplicacao FROM aplicacoes ORDER BY data_aplicacao DESC;';
GO
