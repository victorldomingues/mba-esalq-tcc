using Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;
using Api.Movimentacoes.Dominio.Portas;
using Moq;

namespace Api.Movimentacoes.Testes.Construtures;

public class MovimentacaoUnitOfWorkConstrutor
{
    public Mock<IMovimentacaoUnitOfWork> Mock { get; }
    public IMovimentacaoUnitOfWork Instancia => Mock.Object;

    protected MovimentacaoUnitOfWorkConstrutor()
    {
        Mock = new Mock<IMovimentacaoUnitOfWork>();
    }

    public static MovimentacaoUnitOfWorkConstrutor Construir()
    {
        return new MovimentacaoUnitOfWorkConstrutor().Resetar();
    }

    public MovimentacaoUnitOfWorkConstrutor Resetar()
    {
        Mock.Setup(unitOfWork => unitOfWork.MovimentarAsync(It.IsAny<Movimentacao>()))
            .Returns(Task.CompletedTask);

        return this;
    }

    public MovimentacaoUnitOfWorkConstrutor MovimentacaoExistente(Movimentacao? movimentacao = null)
    {
        movimentacao ??= MovimentacaoConstrutor.Construir().Instancia;

        Mock.Setup(unitOfWork => unitOfWork.MovimentarAsync(It.IsAny<Movimentacao>()))
            .Returns(Task.FromResult(movimentacao));

        return this;
    }
}