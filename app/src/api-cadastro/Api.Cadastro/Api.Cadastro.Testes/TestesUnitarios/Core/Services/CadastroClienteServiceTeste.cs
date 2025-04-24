using Api.Cadastro.Aplicacao.Servicos;
using Api.Cadastro.Data.Repositorios;
using Api.Cadastro.Dominio.ClienteAgregado;
using Api.Cadastro.Dominio.ClienteAgregado.Entidades;
using Api.Cadastro.Modelos;
using Api.Cadastro.Testes.Construtures;
using Artefatos.Notificacoes;
using Moq;
using Shouldly;

namespace Api.Cadastro.Testes.TestesUnitarios.Core.Services;

public class CadastroClienteServiceTeste
{
    [Fact(DisplayName = "Deve cadastrar usuario")]
    public async Task DeveCadastrarUsuario()
    {
        var usuario = UsuarioBuilder.Construir().Instancia;
        var repositorioConstrutor = UsuarioRepositorioConstrutor.Construir();
        var notificacaoContexto = new NotificacaoContexto();
        var service = new CadastroClienteServico(repositorioConstrutor.Instancia, notificacaoContexto);
        
        await service.Cadastrar(usuario);
        
        notificacaoContexto.ExisteNotificacao.ShouldBeFalse();
        repositorioConstrutor.Mock.Verify(x=> x.Cadastrar(It.IsAny<Usuario>()), Times.Once);
        repositorioConstrutor.Mock.Verify(x=> x.Recuperar(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
    
    [Fact(DisplayName = "Deve retornar notificacao caso usuario cadastrado")]
    public async Task DeveRetornarNotificacaoCadastrado()
    {
        var usuario = UsuarioBuilder.Construir().Instancia;
        var repositorioConstrutor = UsuarioRepositorioConstrutor.Construir().UsuarioExistente(usuario);
        var notificacaoContexto = new NotificacaoContexto();
        var service = new CadastroClienteServico(repositorioConstrutor.Instancia, notificacaoContexto);
        
        await service.Cadastrar(usuario);
        
        notificacaoContexto.ExisteNotificacao.ShouldBeTrue();
        repositorioConstrutor.Mock.Verify(x=> x.Cadastrar(It.IsAny<Usuario>()), Times.Never);
        repositorioConstrutor.Mock.Verify(x=> x.Recuperar(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
    
    [Fact(DisplayName = "Deve recuperar perfil usuario e cachear")]
    public async Task DeveRecuperarPerfilUsuario()
    {
        var repositorioConstrutor = PerfilRepositorioConstrutor.Construir();
        var repositorioCacheConstrutor = PerfilCacheRepositorioConstrutor.Construir();
        var notificacaoContexto = new NotificacaoContexto();
        var service = new PerfilServico(repositorioConstrutor.Instancia, repositorioCacheConstrutor.Instancia);
        
        var perfil = await service.Recuperar(Guid.NewGuid());
        
        notificacaoContexto.ExisteNotificacao.ShouldBeFalse();
        repositorioConstrutor.Mock.Verify(x=> x.Recuperar(It.IsAny<Guid>()), Times.Once);
        repositorioCacheConstrutor.Mock.Verify(x=> x.Cachear(It.IsAny<string>(), It.IsAny<PerfilDto>()), Times.Once);
    }
    [Fact(DisplayName = "Deve recuperar perfil do cache")]
    public async Task DeveRecuperarPerfilCache()
    {
        var repositorioConstrutor = PerfilRepositorioConstrutor.Construir().Resetar();
        var repositorioCacheConstrutor = PerfilCacheRepositorioConstrutor.Construir().PerfilExistente();
        var notificacaoContexto = new NotificacaoContexto();
        var service = new PerfilServico(repositorioConstrutor.Instancia, repositorioCacheConstrutor.Instancia);
        
        var perfil = await service.Recuperar(Guid.NewGuid());
        
        notificacaoContexto.ExisteNotificacao.ShouldBeFalse();
        repositorioConstrutor.Mock.Verify(x=> x.Recuperar(It.IsAny<Guid>()), Times.Never);
        repositorioCacheConstrutor.Mock.Verify(x=> x.Recuperar(It.IsAny<string>()), Times.Once);
        repositorioCacheConstrutor.Mock.Verify(x=> x.Cachear(It.IsAny<string>(), It.IsAny<PerfilDto>()), Times.Never);
    }
    
}