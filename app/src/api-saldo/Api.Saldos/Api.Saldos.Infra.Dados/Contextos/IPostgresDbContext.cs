using Npgsql;

namespace Api.Saldos.Infra.Dados.Contextos;

public interface IPostgresDbContext
{
    NpgsqlConnection AbrirConexao();
}