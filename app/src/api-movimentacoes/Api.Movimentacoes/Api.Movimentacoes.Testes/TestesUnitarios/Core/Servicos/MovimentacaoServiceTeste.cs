using Api.Movimentacoes.Aplicacao.Servicos;
using Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;
using Api.Movimentacoes.Testes.Construtures;
using Artefatos.Notificacoes;
using Moq;
using Shouldly;

namespace Api.Movimentacoes.Testes.TestesUnitarios.Core.Servicos;

public class MovimentacaoServiceTeste
{
    [Fact(DisplayName = "Deve realizar movimentacao")]
    public async Task DeveRealizarMovimentacao()
    {
        var movimentacao = MovimentacaoConstrutor.Construir().Instancia;
        var notificacaoContexto = new NotificacaoContexto();
        var unitOfWorkBuilder = MovimentacaoUnitOfWorkConstrutor.Construir();
        var movimentacaoRepositorio =  MovimentacaoRepositorioConstrutor.Construir();
        var movimentacaoServico =  new MovimentacaoServico(unitOfWorkBuilder.Instancia, notificacaoContexto,movimentacaoRepositorio.Instancia);

        await movimentacaoServico.Movimentar(movimentacao);
        
        unitOfWorkBuilder.Mock.Verify(unitOfWork => unitOfWork.MovimentarAsync(It.IsAny<Movimentacao>()), Times.Once);
        notificacaoContexto.ExisteNotificacao.ShouldBeFalse();
    }
}