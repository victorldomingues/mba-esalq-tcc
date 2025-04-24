using Api.Movimentacoes.Dominio.MovimentacaoAgregado.ObjetosValor;
using Api.Movimentacoes.Modelos;
using Api.Movimentacoes.Testes.Construtures;
using Shouldly;

namespace Api.Movimentacoes.Testes.TestesUnitarios.Adaptadores.Primarios.Modelos;

public class MovimentacaoModeloTeste
{
    [Fact(DisplayName = "Deve criar Movimentacao corretamente a partir de MovimentacaoModelo")]
    public void DeveCriarMovimentacaoCorretamente()
    {
        var modelo = MovimentacaoModeloConstrutor.Construir().Instancia;
        var idUsuario = Guid.NewGuid();


        var movimentacao = modelo.ParaUsuario(idUsuario);


        movimentacao.IdUsuario.ShouldBe(idUsuario);
        movimentacao.Valor.ShouldBe(modelo.Valor);
        movimentacao.Tipo.ShouldBe(modelo.Tipo);
        movimentacao.Forma.ShouldBe(modelo.Forma);
        movimentacao.Destinatario.ShouldBe(modelo.Destinatario);
        movimentacao.Banco.ShouldBe(modelo.Banco);
        movimentacao.Agencia.ShouldBe(modelo.Agencia);
        movimentacao.Conta.ShouldBe(modelo.Conta);
        movimentacao.Dac.ShouldBe(modelo.Dac.ToCharArray().FirstOrDefault());
    }

    [Theory(DisplayName = "Deve criar Movimentacao corretamente com diferentes valores")]
    [InlineData(100, TipoMovimentacao.Deposito, FormaMovimentacao.Pix, "Destinatario A", "Banco A", "Agencia A",
        "Conta A", "1")]
    [InlineData(200, TipoMovimentacao.Saque, FormaMovimentacao.Ted, "Destinatario B", "Banco B", "Agencia B", "Conta B",
        "2")]
    public void DeveCriarMovimentacaoComDiferentesValores(decimal valor, TipoMovimentacao tipo, FormaMovimentacao forma,
        string destinatario, string banco, string agencia, string conta, string dac)
    {
        var modelo = new MovimentacaoModelo
        {
            Valor = valor,
            Tipo = tipo,
            Forma = forma,
            Destinatario = destinatario,
            Banco = banco,
            Agencia = agencia,
            Conta = conta,
            Dac = dac
        };
        var idUsuario = Guid.NewGuid();


        var movimentacao = modelo.ParaUsuario(idUsuario);


        movimentacao.IdUsuario.ShouldBe(idUsuario);
        movimentacao.Valor.ShouldBe(modelo.Valor);
        movimentacao.Tipo.ShouldBe(modelo.Tipo);
        movimentacao.Forma.ShouldBe(modelo.Forma);
        movimentacao.Destinatario.ShouldBe(modelo.Destinatario);
        movimentacao.Banco.ShouldBe(modelo.Banco);
        movimentacao.Agencia.ShouldBe(modelo.Agencia);
        movimentacao.Conta.ShouldBe(modelo.Conta);
        movimentacao.Dac.ShouldBe(modelo.Dac.ToCharArray().FirstOrDefault());
    }
}