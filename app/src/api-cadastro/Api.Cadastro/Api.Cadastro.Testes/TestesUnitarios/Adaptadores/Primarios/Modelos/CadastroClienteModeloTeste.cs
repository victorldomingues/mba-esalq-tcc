using System.Text.RegularExpressions;
using Api.Cadastro.Dominio.ClienteAgregado.ObjetosValor;
using Api.Cadastro.Testes.Construtures;
using Shouldly;

namespace Api.Cadastro.Testes.TestesUnitarios.Adaptadores.Primarios.Modelos;

public class CadastroClienteModeloTeste
{
    [Fact(DisplayName = "Deve converter modelo para entidade usuario")]
    public void DeveConverterModeloParaEntidadeUsuario()
    {
        var cadastroCliente = CadastroClienteModeloBuilder.Construir().Instancia;
        var usuario = cadastroCliente.ParaUsuario();
        var senha = new Senha(cadastroCliente.Senha);
        
        usuario.Cpf.Numero.ShouldBe(Regex.Replace(cadastroCliente.Cpf,@"[^\d]", ""));
        usuario.Nome.ShouldBe(cadastroCliente.Nome);
        usuario.Email.ShouldBe(cadastroCliente.Email);
        usuario.Senha.Encriptada.ShouldBe(senha.Encriptada);
        usuario.Situacao.ShouldBe(Situacao.Ativo);
        usuario.CriadoEm.ShouldNotBe(default);
    }
}