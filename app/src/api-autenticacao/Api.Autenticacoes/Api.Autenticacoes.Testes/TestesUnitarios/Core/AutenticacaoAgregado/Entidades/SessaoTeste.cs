using Api.Autenticacoes.Dominio.AutenticacaoAgregado.Entidades;
using Shouldly;

namespace Api.Autenticacoes.Testes.TestesUnitarios.Core.AutenticacaoAgregado.Entidades;

public class SessaoTeste
{
    [Fact(DisplayName = "Deve criar uma sessão com valores válidos")]
    public void DeveCriarSessaoComValoresValidos()
    {
        
        var idUsuario = Guid.NewGuid();
        var criadoEm = DateTime.UtcNow;
        var expiraEm = criadoEm.AddHours(1);
        var token = "token-de-teste";

        
        var sessao = new Sessao(idUsuario, criadoEm, expiraEm, token);

        
        sessao.ShouldNotBeNull();
        sessao.Chave.ShouldBe($"sessao-{idUsuario}");
        sessao.IdUsuario.ShouldBe(idUsuario);
        sessao.AccessToken.ShouldBe(token);
        sessao.CriadoEm.ShouldBe(criadoEm);
        sessao.ExpiraEm.ShouldBe(expiraEm);
    }

    [Fact(DisplayName = "Deve inicializar uma sessão com construtor vazio")]
    public void DeveInicializarSessaoComConstrutorVazio()
    {
        
        var sessao = new Sessao();

        
        sessao.ShouldNotBeNull();
        sessao.Chave.ShouldBeNull();
        sessao.AccessToken.ShouldBeNull();
        sessao.Id.ShouldBe(Guid.Empty);
        sessao.IdUsuario.ShouldBe(Guid.Empty);
        sessao.CriadoEm.ShouldBe(default(DateTime));
        sessao.ExpiraEm.ShouldBe(default(DateTime));
    }
}