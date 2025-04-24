using Api.Movimentacoes.Dominio.MovimentacaoAgregado.ObjetosValor;
using Api.Movimentacoes.Modelos;
using Api.Movimentacoes.Testes.Construtures;
using Api.Movimentacoes.Validadores;
using FluentValidation.TestHelper;
using Shouldly;

namespace Api.Movimentacoes.Testes.TestesUnitarios.Adaptadores.Primarios.Validadores;

public class MovimentacaoModeloValidadorTeste
{
    private readonly MovimentacaoModeloValidador _validador;

    public MovimentacaoModeloValidadorTeste()
    {
        _validador = new MovimentacaoModeloValidador();
    }

    [Fact(DisplayName = "Deve validar corretamente quando todos os campos são válidos")]
    public void DeveValidarCorretamente()
    {
        
        var modelo = new MovimentacaoModelo
        {
            Valor = 100m,
            Tipo = TipoMovimentacao.Deposito,
            Forma = FormaMovimentacao.Pix,
            Destinatario = "Destinatario Teste",
            Banco = "Banco Teste",
            Agencia = "Agencia Teste",
            Conta = "Conta Teste",
            Dac = "1"
        };

        
        var resultado = _validador.TestValidate(modelo);

        
        resultado.ShouldNotHaveAnyValidationErrors();
    }

    [Theory(DisplayName = "Deve falhar na validação quando Valor é inválido")]
    [InlineData(0)]
    [InlineData(-1)]
    public void DeveFalharQuandoValorInvalido(decimal valor)
    {
        
        var modelo = new MovimentacaoModelo
        {
            Valor = valor,
            Tipo = TipoMovimentacao.Deposito,
            Forma = FormaMovimentacao.Pix,
            Destinatario = "Destinatario Teste"
        };

        
        var resultado = _validador.TestValidate(modelo);

        
        resultado.ShouldHaveValidationErrorFor(m => m.Valor)
            .WithErrorCode("ERRO_MOVIMENTACAO_0001");
    }

    [Fact(DisplayName = "Deve falhar na validação quando Destinatario é nulo ou vazio")]
    public void DeveFalharQuandoDestinatarioInvalido()
    {
        
        var modelo = new MovimentacaoModelo
        {
            Valor = 100m,
            Tipo = TipoMovimentacao.Deposito,
            Forma = FormaMovimentacao.Pix,
            Destinatario = ""
        };

        
        var resultado = _validador.TestValidate(modelo);

        
        resultado.ShouldHaveValidationErrorFor(m => m.Destinatario)
            .WithErrorCode("ERRO_MOVIMENTACAO_0002");
    }

    [Fact(DisplayName = "Deve falhar na validação quando Tipo é inválido")]
    public void DeveFalharQuandoTipoInvalido()
    {
        
        var modelo = new MovimentacaoModelo
        {
            Valor = 100m,
            Tipo = default,
            Forma = FormaMovimentacao.Pix,
            Destinatario = "Destinatario Teste"
        };

        
        var resultado = _validador.TestValidate(modelo);

        
        resultado.ShouldHaveValidationErrorFor(m => m.Tipo)
            .WithErrorCode("ERRO_MOVIMENTACAO_0003");
    }

    [Fact(DisplayName = "Deve falhar na validação quando Forma é inválida")]
    public void DeveFalharQuandoFormaInvalida()
    {
        
        var modelo = new MovimentacaoModelo
        {
            Valor = 100m,
            Tipo = TipoMovimentacao.Deposito,
            Forma = default,
            Destinatario = "Destinatario Teste"
        };

        
        var resultado = _validador.TestValidate(modelo);

        
        resultado.ShouldHaveValidationErrorFor(m => m.Forma)
            .WithErrorCode("ERRO_MOVIMENTACAO_0004");
    }

    [Theory(DisplayName = "Deve falhar na validação quando dados bancários são inválidos para DOC ou TED")]
    [InlineData(FormaMovimentacao.Doc)]
    [InlineData(FormaMovimentacao.Ted)]
    public void DeveFalharQuandoDadosBancariosInvalidos(FormaMovimentacao forma)
    {
        
        var modelo = new MovimentacaoModelo
        {
            Valor = 100m,
            Tipo = TipoMovimentacao.Deposito,
            Forma = forma,
            Destinatario = "Destinatario Teste",
            Banco = "",
            Agencia = "",
            Conta = "",
            Dac = null
        };

        
        var resultado = _validador.TestValidate(modelo);

        
        resultado.ShouldHaveValidationErrorFor(m => m.Forma)
            .WithErrorCode("ERRO_MOVIMENTACAO_0002");
    }
}