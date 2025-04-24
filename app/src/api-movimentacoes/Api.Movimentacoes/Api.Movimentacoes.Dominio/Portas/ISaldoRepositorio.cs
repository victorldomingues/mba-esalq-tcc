using Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;

namespace Api.Movimentacoes.Dominio.Portas;

public interface ISaldoRepositorio
{
    Task<Saldo?> RecuperarAsync(Guid idUsuario);
    Task AtualizarSaldoAsync(Saldo saldo);
    Task SalvarSaldoAsync(Saldo saldo);
}