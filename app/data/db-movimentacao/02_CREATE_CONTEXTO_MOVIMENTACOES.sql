DROP TABLE IF EXISTS movimentacoes;
DROP TABLE IF EXISTS saldos;

CREATE TABLE movimentacoes (
    id uuid PRIMARY KEY NOT NULL,
    id_usuario uuid NOT NULL,
    valor NUMERIC(10,2) NOT NULL,
    valor_efetivar_saldo NUMERIC(10,2) NOT NULL,
    tipo SMALLINT NOT NULL,
    forma SMALLINT NOT NULL,
    destinatario TEXT,
    banco VARCHAR(10) NULL,
    agencia VARCHAR(10) NULL,
    conta VARCHAR (10) NULL,
    dac CHAR(1) NULL,
    situacao SMALLINT NOT NULL,
    criado_em TIMESTAMP NOT NULL,
    atualizado_em TIMESTAMP,
    deletado_em TIMESTAMP
);

CREATE TABLE saldos (
    id_usuario uuid PRIMARY KEY NOT NULL,
    valor NUMERIC(10,2) NOT NULL,
    situacao SMALLINT NOT NULL,
    criado_em TIMESTAMP NOT NULL,
    atualizado_em TIMESTAMP,
    deletado_em TIMESTAMP
);
