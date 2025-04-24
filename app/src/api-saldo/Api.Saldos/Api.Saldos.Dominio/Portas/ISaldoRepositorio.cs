using Api.Saldos.Dominio.SaldoAgregado.ObjetosTransferencia;

namespace Api.Saldos.Dominio.Portas;

public interface ISaldoRepositorio
{
    Task<Saldo> Recuperar(Guid idUsuario);
}