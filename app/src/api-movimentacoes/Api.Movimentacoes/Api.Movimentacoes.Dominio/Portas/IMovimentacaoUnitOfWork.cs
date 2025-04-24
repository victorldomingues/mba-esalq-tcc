using Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;

namespace Api.Movimentacoes.Dominio.Portas;

public interface IMovimentacaoUnitOfWork 
{
    Task MovimentarAsync(Movimentacao movimentacao);
}