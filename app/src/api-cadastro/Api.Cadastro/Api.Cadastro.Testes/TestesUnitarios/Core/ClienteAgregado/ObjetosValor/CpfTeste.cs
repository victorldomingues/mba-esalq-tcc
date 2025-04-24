using System.Text.RegularExpressions;
using Api.Cadastro.Dominio.ClienteAgregado.ObjetosValor;
using Shouldly;

namespace Api.Cadastro.Testes.TestesUnitarios.Core.ClienteAgregado.ObjetosValor;

public class CpfTeste
{
    // TODO: incluir mais cenários de testes
    [Fact(DisplayName = "Cpf deve ter o CPF valido")]
    public void DeveValidarCpf()
    {
        var numero = "928.532.020-11";
        
        var cpf = new Cpf(numero);
        
        cpf.Valido.ShouldBeTrue();
    }
    
    // TODO: incluir mais cenários de testes
    [Fact(DisplayName = "Cpf deve ter o CPF invalido")]
    public void DeveInvalidarCpf()
    {
        var numero = "11111111111";
        
        var cpf = new Cpf(numero);
        
        cpf.Valido.ShouldBeFalse();
    }

    [Fact]
    public void DeveLimparCpf()
    {
        var numero = "928.532.020-11";
        
        var cpf = new Cpf(numero);
        
        cpf.Numero.ShouldBe(Regex.Replace(numero, @"[^\d]", ""));
    }
}