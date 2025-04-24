using Api.Saldos.Dominio.SaldoAgregado.ObjetosTransferencia;

namespace Api.Saldos.Dominio.Portas;

public interface ISaldoCacheRepositorio
{
    Task<Saldo?> Recuperar(Guid idUsuario);
    Task CachearSaldo(Saldo saldo, Guid idUsuario);
}