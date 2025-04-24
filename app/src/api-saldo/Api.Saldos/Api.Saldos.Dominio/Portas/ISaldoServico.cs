using Api.Saldos.Dominio.SaldoAgregado.ObjetosTransferencia;

namespace Api.Saldos.Dominio.Portas;

public interface ISaldoServico
{
    Task<Saldo> Recuperar(Guid idUsuario);
}