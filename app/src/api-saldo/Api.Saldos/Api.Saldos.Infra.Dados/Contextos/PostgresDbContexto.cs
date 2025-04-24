using Npgsql;

namespace Api.Saldos.Infra.Dados.Contextos;

public class PostgresDbContexto : IPostgresDbContext
{
    private string _stringConexao { get; }
    
    public PostgresDbContexto(string stringConexao)
    {
        _stringConexao = stringConexao;
    }
    public NpgsqlConnection AbrirConexao()
    {
        return new NpgsqlConnection(_stringConexao);
    }
}