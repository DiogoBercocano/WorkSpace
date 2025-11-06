/* Criação do banco */
IF DB_ID('controle_vacinacao') IS NOT NULL
BEGIN
    ALTER DATABASE controle_vacinacao SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE controle_vacinacao;
END;
GO

CREATE DATABASE controle_vacinacao;
GO
USE controle_vacinacao;
GO

/* Tabelas */
CREATE TABLE dbo.unidades (
  id            INT IDENTITY(1,1) PRIMARY KEY,
  nome          VARCHAR(120) NOT NULL,
  cidade        VARCHAR(80)  NOT NULL,
  estoque_total INT NOT NULL CONSTRAINT DF_unidades_estoque_total DEFAULT (0),
  criado_em     DATETIME2(0) NOT NULL CONSTRAINT DF_unidades_criado_em DEFAULT (SYSDATETIME())
);
GO

CREATE TABLE dbo.vacinas (
  id             INT IDENTITY(1,1) PRIMARY KEY,
  nome           VARCHAR(100) NOT NULL,
  fabricante     VARCHAR(100) NOT NULL,
  intervalo_dias INT NOT NULL CONSTRAINT DF_vacinas_intervalo DEFAULT (0),
  criado_em      DATETIME2(0) NOT NULL CONSTRAINT DF_vacinas_criado_em DEFAULT (SYSDATETIME())
);
GO

CREATE TABLE dbo.pacientes (
  id          INT IDENTITY(1,1) PRIMARY KEY,
  nome        VARCHAR(120) NOT NULL,
  cpf         CHAR(11) NOT NULL UNIQUE,
  nascimento  DATE NOT NULL,
  criado_em   DATETIME2(0) NOT NULL CONSTRAINT DF_pacientes_criado_em DEFAULT (SYSDATETIME())
);
GO

CREATE TABLE dbo.aplicacoes (
  id              INT IDENTITY(1,1) PRIMARY KEY,
  paciente_id     INT NOT NULL,
  vacina_id       INT NOT NULL,
  unidade_id      INT NOT NULL,
  data_aplicacao  DATE NOT NULL,
  lote            VARCHAR(40) NOT NULL,
  dose_numero     INT NOT NULL CONSTRAINT DF_aplicacoes_dose DEFAULT (1),
  observacao      VARCHAR(255) NULL,
  criado_em       DATETIME2(0) NOT NULL CONSTRAINT DF_aplicacoes_criado_em DEFAULT (SYSDATETIME()),
  CONSTRAINT FK_apl_paciente FOREIGN KEY (paciente_id) REFERENCES dbo.pacientes(id),
  CONSTRAINT FK_apl_vacina   FOREIGN KEY (vacina_id)   REFERENCES dbo.vacinas(id),
  CONSTRAINT FK_apl_unidade  FOREIGN KEY (unidade_id)  REFERENCES dbo.unidades(id)
);
GO

CREATE TABLE dbo.agendamentos (
  id            INT IDENTITY(1,1) PRIMARY KEY,
  paciente_id   INT NOT NULL,
  vacina_id     INT NOT NULL,
  unidade_id    INT NOT NULL,
  data_agendada DATE NOT NULL,
  dose_numero   INT NOT NULL,
  criado_em     DATETIME2(0) NOT NULL CONSTRAINT DF_agendamentos_criado_em DEFAULT (SYSDATETIME()),
  CONSTRAINT FK_age_paciente FOREIGN KEY (paciente_id) REFERENCES dbo.pacientes(id),
  CONSTRAINT FK_age_vacina   FOREIGN KEY (vacina_id)   REFERENCES dbo.vacinas(id),
  CONSTRAINT FK_age_unidade  FOREIGN KEY (unidade_id)  REFERENCES dbo.unidades(id)
);
GO

CREATE TABLE dbo.aplicacoes_audit (
  id            INT IDENTITY(1,1) PRIMARY KEY,
  aplicacao_id  INT          NOT NULL,
  paciente_id   INT          NOT NULL,
  vacina_id     INT          NOT NULL,
  unidade_id    INT          NOT NULL,
  data_aplicacao DATE        NOT NULL,
  lote          VARCHAR(40)  NOT NULL,
  dose_numero   INT          NOT NULL,
  operacao      VARCHAR(10)  NOT NULL,
  audit_at      DATETIME2(0) NOT NULL DEFAULT (SYSDATETIME())
);
GO

/* Seeds */
INSERT INTO dbo.unidades (nome, cidade, estoque_total) VALUES
('UBS Central', 'Curitiba', 50),
('UPA Jardim',  'Curitiba', 25),
('Posto Sul',   'Ponta Grossa', 30);

INSERT INTO dbo.vacinas (nome, fabricante, intervalo_dias) VALUES
('Influenza', 'Butantan', 0),
('COVID-19',  'Pfizer',   21),
('Hepatite B','Fiocruz',  30);

INSERT INTO dbo.pacientes (nome, cpf, nascimento) VALUES
('Ana Silva',   '12345678901', '1990-05-10'),
('Bruno Souza', '98765432100', '1985-11-30'),
('Carla Lima',  '00011122233', '2010-02-14');

INSERT INTO dbo.aplicacoes (paciente_id, vacina_id, unidade_id, data_aplicacao, lote, dose_numero, observacao) VALUES
(1, 1, 1, '2025-10-10', 'INF-001', 1, 'Campanha anual'),
(2, 2, 1, '2025-10-20', 'COV-010', 1, 'Primeira dose'),
(2, 2, 1, '2025-10-20', 'COV-010', 1, 'Segunda dose'),
(3, 3, 2, '2025-09-25', 'HEP-100', 1, NULL);
GO
