using Api.Autenticacoes.Dominio.AutenticacaoAgregado.Entidades;
using Api.Autenticacoes.Dominio.AutenticacaoAgregado.ObjetosDeValor;
using Artefatos.Seguranca;
using Shouldly;

namespace Api.Autenticacoes.Testes.TestesUnitarios.Core.AutenticacaoAgregado.Entidades;

public class UsuarioTeste
{
    
    //TODO: criar cenarios de teste com teoria
    [Fact(DisplayName = "Deve validar senha correta")]
    public void DeveValidarSenhaCorreta()
    {
        var senhaOriginal = "senha123";
        var usuario = new Usuario
        {
            Senha = Criptografia.CriptografarSHA256(senhaOriginal)
        };

        var senhaValida = usuario.SenhaValida(senhaOriginal);

        senhaValida.ShouldBeTrue();
    }

    [Fact(DisplayName = "Deve invalidar senha incorreta")]
    public void DeveInvalidarSenhaIncorreta()
    {
        var senhaOriginal = "senha123";
        var senhaIncorreta = "senhaErrada";
        var usuario = new Usuario
        {
            Senha = Criptografia.CriptografarSHA256(senhaOriginal)
        };

        var senhaValida = usuario.SenhaValida(senhaIncorreta);

        senhaValida.ShouldBeFalse();
    }

    [Fact(DisplayName = "Deve inicializar usuário com valores padrão")]
    public void DeveInicializarUsuarioComValoresPadrao()
    {
        var usuario = new Usuario();

        usuario.ShouldNotBeNull();
        usuario.Id.ShouldBe(Guid.Empty);
        usuario.Email.ShouldBeNull();
        usuario.Cpf.ShouldBeNull();
        usuario.Senha.ShouldBeNull();
        usuario.Situacao.ShouldBe(default(Situacao));
    }
}