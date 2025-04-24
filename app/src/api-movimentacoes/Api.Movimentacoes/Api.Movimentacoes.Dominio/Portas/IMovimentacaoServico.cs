using Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;

namespace Api.Movimentacoes.Dominio.Portas;

public interface IMovimentacaoServico
{
    public Task Movimentar(Movimentacao movimentacao);
    public Task<IEnumerable<Movimentacao>> Listar(Guid idUsuario);
}