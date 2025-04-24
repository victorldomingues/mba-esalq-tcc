using Api.Saldos.Dominio.Portas;
using Api.Saldos.Dominio.SaldoAgregado.ObjetosTransferencia;
using Api.Saldos.Infra.Dados.Contextos;
using Dapper;

namespace Api.Saldos.Infra.Dados.Repositorios;

public class SaldoRepositorio : ISaldoRepositorio
{
    private readonly IPostgresDbContext _context;

    public SaldoRepositorio(IPostgresDbContext context)
    {
        _context = context;
    }

    public  async Task<Saldo> Recuperar(Guid idUsuario)
    {
        await using var conexao = _context.AbrirConexao();
        var saldo =  await conexao.QuerySingleAsync<Saldo>(
            "SELECT valor, atualizado_em FROM saldos WHERE id_usuario = @id", 
            new  {id = idUsuario});
        return saldo;
    }
}