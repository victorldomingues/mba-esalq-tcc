using Api.Movimentacoes.Dominio.MovimentacaoAgregado.ObjetosValor;
using Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;

namespace Api.Movimentacoes.Dominio.Portas;

public interface IMovimentacaoRepositorio
{
    public Task Movimentar(Movimentacao movimentacao);
    public Task<IEnumerable<Movimentacao>> Listar(Guid idUsuario);
}