using System.Text;
using Api.Cadastro.Dominio.ClienteAgregado.ObjetosValor;
using Shouldly;

namespace Api.Cadastro.Testes.TestesUnitarios.Core.ClienteAgregado.ObjetosValor;

public class SenhaTeste
{
    [Fact(DisplayName = "Deve encriptar senha")]
    public void DeveEncriptarSenha()
    {
        var senha = new Senha("senha123");

        senha.Encriptada.ShouldNotBeNull();
        senha.Encriptada.ShouldNotBeEmpty();
        senha.Encriptada.Length.ShouldBe(64);
    }
}