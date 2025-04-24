using Api.Movimentacoes.Infra.Dados.Contextos;
using Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;
using Api.Movimentacoes.Dominio.Portas;
using Microsoft.EntityFrameworkCore;

namespace Api.Movimentacoes.Infra.Dados.Repositorios;

public class MovimentacaoRepositorio : IMovimentacaoRepositorio
{
    private readonly PostgresDbContexto _contexto;

    public MovimentacaoRepositorio(PostgresDbContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task Movimentar(Movimentacao movimentacao)
    {
        await _contexto.Movimentacoes.AddAsync(movimentacao);
        await _contexto.SaveChangesAsync();
    }

    public  async Task<IEnumerable<Movimentacao>> Listar(Guid idUsuario)
    {
        return await _contexto.Movimentacoes.AsNoTracking().Where(x => x.IdUsuario == idUsuario)
            .OrderByDescending(x=>x.CriadoEm).ToListAsync();
    }
}