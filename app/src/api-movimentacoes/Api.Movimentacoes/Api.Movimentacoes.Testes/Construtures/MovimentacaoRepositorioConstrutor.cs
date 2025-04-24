using Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;
using Api.Movimentacoes.Dominio.MovimentacaoAgregado.ObjetosValor;
using Api.Movimentacoes.Dominio.Portas;
using Moq;

namespace Api.Movimentacoes.Testes.Construtures;

public class MovimentacaoRepositorioConstrutor
{
    public Mock<IMovimentacaoRepositorio> Mock { get; }
    public IMovimentacaoRepositorio Instancia => Mock.Object;

    protected MovimentacaoRepositorioConstrutor()
    {
        Mock = new Mock<IMovimentacaoRepositorio>();
    }

    public static MovimentacaoRepositorioConstrutor Construir()
    {
        return new MovimentacaoRepositorioConstrutor().Resetar();
    }

    public MovimentacaoRepositorioConstrutor Resetar()
    {
        Mock.Setup(repositorio => repositorio.Movimentar(It.IsAny<Movimentacao>()))
            .Returns(Task.CompletedTask);

        return this;
    }

    public MovimentacaoRepositorioConstrutor MovimentacaoExistente(Movimentacao? movimentacao = null)
    {
        movimentacao ??= MovimentacaoConstrutor.Construir().Instancia;

        Mock.Setup(repositorio => repositorio.Movimentar(It.IsAny<Movimentacao>()))
            .Returns(Task.FromResult(movimentacao));

        return this;
    }
}