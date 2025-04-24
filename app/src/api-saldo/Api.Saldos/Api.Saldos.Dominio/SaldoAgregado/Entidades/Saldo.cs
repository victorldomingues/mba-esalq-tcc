namespace Api.Saldos.Dominio.SaldoAgregado.ObjetosTransferencia;

public record Saldo
{
    public decimal Valor { get; set; }
    public DateTime AtualizadoEm { get; set; }
}