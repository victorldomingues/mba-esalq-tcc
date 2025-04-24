using Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;
using Api.Movimentacoes.Dominio.Portas;
using Moq;

namespace Api.Movimentacoes.Testes.Construtures;

public class SaldoRepositorioConstrutor
{
    public Mock<ISaldoRepositorio> Mock { get; }
    public ISaldoRepositorio Instancia => Mock.Object;

    protected SaldoRepositorioConstrutor()
    {
        Mock = new Mock<ISaldoRepositorio>();
    }

    public static SaldoRepositorioConstrutor Construir()
    {
        return new SaldoRepositorioConstrutor().Resetar();
    }

    public SaldoRepositorioConstrutor Resetar()
    {
        Mock.Setup(repositorio => repositorio.RecuperarAsync(It.IsAny<Guid>()));
        Mock.Setup(repositorio => repositorio.AtualizarSaldoAsync(It.IsAny<Saldo>()));
        Mock.Setup(repositorio => repositorio.SalvarSaldoAsync(It.IsAny<Saldo>()));

        return this;
    }

    public SaldoRepositorioConstrutor SaldoExistente(Saldo? saldo = null)
    {
        saldo ??= SaldoConstrutor.Construir().Instancia;

        Mock.Setup(repositorio => repositorio.RecuperarAsync(It.IsAny<Guid>()))
            .ReturnsAsync(saldo);

        return this;
    }
}