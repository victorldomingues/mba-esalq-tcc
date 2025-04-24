using Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;
using Api.Movimentacoes.Dominio.Portas;
using Api.Movimentacoes.Infra.Dados.Contextos;
using Microsoft.EntityFrameworkCore;

namespace Api.Movimentacoes.Infra.Dados.Repositorios;

public class SaldoRepositorio : ISaldoRepositorio
{
    private readonly PostgresDbContexto _contexto;

    public SaldoRepositorio(PostgresDbContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<Saldo?> RecuperarAsync(Guid idUsuario)
    {
        return  await _contexto.Saldos.FirstOrDefaultAsync(x=>x.IdUsuario == idUsuario);
    }

    public async Task AtualizarSaldoAsync(Saldo saldo)
    {
        _contexto.Entry(saldo).State = EntityState.Modified;
        await _contexto.SaveChangesAsync();
    }
    public  async Task SalvarSaldoAsync(Saldo saldo)
    {
        await _contexto.Saldos.AddAsync(saldo);
        await _contexto.SaveChangesAsync();
    }
}