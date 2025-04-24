DROP TABLE IF EXISTS usuarios;

CREATE TABLE usuarios (
    id uuid PRIMARY KEY NOT NULL,
    nome VARCHAR(300) NOT NULL,
    cpf VARCHAR (11) NOT NULL,
    email VARCHAR(300) UNIQUE NOT NULL,
    senha VARCHAR(256) NOT NULL,
    situacao SMALLINT NOT NULL,
    criado_em TIMESTAMP NOT NULL,
    atualizado_em TIMESTAMP,
    deletado_em TIMESTAMP
);
