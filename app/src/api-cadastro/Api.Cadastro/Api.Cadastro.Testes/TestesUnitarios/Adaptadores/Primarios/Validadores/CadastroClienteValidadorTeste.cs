using Api.Cadastro.Modelos;
using Api.Cadastro.Testes.Construtures;
using Api.Cadastro.Validadores;
using Shouldly;

namespace Api.Cadastro.Testes.TestesUnitarios.Adaptadores.Primarios.Validadores;

public class CadastroClienteValidadorTeste
{
    //TODO: incluir mais cenários de teste
    [Fact(DisplayName = "Deve validar cliente com sucesso ")]
    public void DeveValidarComSucesso()
    {
        var cadastroCliente = CadastroClienteModeloBuilder.Construir().Instancia;
        var validador = new CadastroClienteValidador();
        
        var resultadoValidacao = validador.Validate(cadastroCliente);  
        resultadoValidacao.IsValid.ShouldBeTrue();
    }
    
    /// TODO: incluir mais cenários
    [Fact(DisplayName = "Deve conter nome obrigatório")]
    public void DeveConterNomeObrigatorio()
    {
        var cadastroCliente = CadastroClienteModeloBuilder.Construir().Instancia;
        var validador = new CadastroClienteValidador();
        cadastroCliente.Nome = null!;
        
        var resultadoValidacao = validador.Validate(cadastroCliente);  
        resultadoValidacao.IsValid.ShouldBeFalse();
        resultadoValidacao.Errors.Select(x=>x.PropertyName)
            .ShouldContain(nameof(CadastroClienteModelo.Nome));
    }
    

}