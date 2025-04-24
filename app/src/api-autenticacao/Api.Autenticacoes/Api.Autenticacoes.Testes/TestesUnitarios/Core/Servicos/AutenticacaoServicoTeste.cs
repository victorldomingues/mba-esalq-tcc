using Api.Autenticacoes.Aplicacao.Modelos;
using Api.Autenticacoes.Aplicacao.Servicos;
using Api.Autenticacoes.Dominio.AutenticacaoAgregado.Entidades;
using Api.Autenticacoes.Dominio.AutenticacaoAgregado.ObjetosTransferencia;
using Api.Autenticacoes.Dominio.Portas;
using Artefatos.Notificacoes;
using Artefatos.Seguranca;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace Api.Autenticacoes.Testes.TestesUnitarios.Core.Servicos;

public class AutenticacaoServicoTeste
{
    [Fact(DisplayName = "Deve retornar sessão válida ao logar com credenciais corretas")]
    public async Task DeveRetornarSessaoValidaAoLogarComCredenciaisCorretas()
    {
        var mockUsuarioRepositorio = new Mock<IUsuarioRepositorio>();
        var mockSessaoCacheRepositorio = new Mock<ISessaoCacheRepositorio>();
        var mockNotificacaoContexto = new Mock<INotificacaoContexto>();
        var opcoesDeSeguranca = new OpcoesDeSeguranca(Guid.NewGuid().ToString());
        var logger = Mock.Of<ILogger<AutenticacaoServico>>();
        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Email = "teste@teste.com",
            Senha = Criptografia.CriptografarSHA256("senha123")
        };

        mockUsuarioRepositorio
            .Setup(repo => repo.RecuperarUsuarioAsync(It.IsAny<LoginDto>()))
            .ReturnsAsync(usuario);

        var autenticacaoServico = new AutenticacaoServico(
            logger,
            mockUsuarioRepositorio.Object,
            mockNotificacaoContexto.Object,
            mockSessaoCacheRepositorio.Object,
            opcoesDeSeguranca
        );

        var loginDto = new LoginDto { Cpf = "cpf", Senha = "senha123" };

        var sessao = await autenticacaoServico.Logar(loginDto);

        sessao.ShouldNotBeNull();
        sessao.IdUsuario.ShouldBe(usuario.Id);
        sessao.AccessToken.ShouldNotBeNullOrEmpty();

        mockSessaoCacheRepositorio.Verify(repo => repo.CachearSessaoAsync(It.IsAny<Sessao>()), Times.Once);
    }

    [Fact(DisplayName = "Deve retornar nulo e adicionar notificação ao logar com credenciais incorretas")]
    public async Task DeveRetornarNuloEAdicionarNotificacaoAoLogarComCredenciaisIncorretas()
    {
        var mockUsuarioRepositorio = new Mock<IUsuarioRepositorio>();
        var mockSessaoCacheRepositorio = new Mock<ISessaoCacheRepositorio>();
        var mockNotificacaoContexto = new Mock<INotificacaoContexto>();
        var opcoesDeSeguranca = new OpcoesDeSeguranca(Guid.NewGuid().ToString());
        var logger = Mock.Of<ILogger<AutenticacaoServico>>();

        mockUsuarioRepositorio
            .Setup(repo => repo.RecuperarUsuarioAsync(It.IsAny<LoginDto>()))
            .ReturnsAsync((Usuario?)null);

        var autenticacaoServico = new AutenticacaoServico(
            logger,
            mockUsuarioRepositorio.Object,
            mockNotificacaoContexto.Object,
            mockSessaoCacheRepositorio.Object,
            opcoesDeSeguranca
        );

        var loginDto = new LoginDto { Cpf = "", Senha = "senhaErrada" };

        var sessao = await autenticacaoServico.Logar(loginDto);

        sessao.ShouldBeNull();

        mockNotificacaoContexto.Verify(ctx =>
            ctx.AdicionarNotificacao(It.Is<Notificacao>(n =>
                n.Codigo == "AUTENTICACAO_ERRO_001" &&
                n.Mensagem.Contains("Ocorreu um proplema"))), Times.Once);
    }
}