using Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;
using Api.Movimentacoes.Dominio.MovimentacaoAgregado.ObjetosValor;
using Shouldly;

namespace Api.Movimentacoes.Testes.TestesUnitarios.Core.MovimentacaoAgregado.Entidades;

public class MovimentacaoTeste
{
     [Fact(DisplayName = "Deve inicializar com todas as propriedades corretas")]
    public void DeveInicializarComAsPropriedadesCorretas()
    {
        
        var idUsuario = Guid.NewGuid();
        var tipo = TipoMovimentacao.Deposito;
        var forma = FormaMovimentacao.Pix;
        var valor = 100m;
        var destinatario = "Destinatario";
        var banco = "Banco";
        var agencia = "Agencia";
        var conta = "Conta";
        var dac = '1';

        
        var movimentacao = new Movimentacao(idUsuario, tipo, forma, valor, destinatario, banco, agencia, conta, dac);

        
        movimentacao.IdUsuario.ShouldBe(idUsuario);
        movimentacao.Tipo.ShouldBe(tipo);
        movimentacao.Forma.ShouldBe(forma);
        movimentacao.Valor.ShouldBe(valor);
        movimentacao.Destinatario.ShouldBe(destinatario);
        movimentacao.Banco.ShouldBe(banco);
        movimentacao.Agencia.ShouldBe(agencia);
        movimentacao.Conta.ShouldBe(conta);
        movimentacao.Dac.ShouldBe(dac);
        movimentacao.ValorEfetivarSaldo.ShouldBe(valor);
    }

    [Fact(DisplayName = "Deve inicializar somente com as propriedades obrigatórias")]
    public void DeveInicilizarComAsPropriedadesPadrao()
    {
        
        var idUsuario = Guid.NewGuid();
        var tipo = TipoMovimentacao.Deposito;
        var forma = FormaMovimentacao.Doc;
        var valor = 100m;
        var destinatario = "Destinatario";

        
        var movimentacao = new Movimentacao(idUsuario, tipo, forma, valor, destinatario);

        
        movimentacao.IdUsuario.ShouldBe(idUsuario);
        movimentacao.Tipo.ShouldBe(tipo);
        movimentacao.Forma.ShouldBe(forma);
        movimentacao.Valor.ShouldBe(valor);
        Assert.Equal(destinatario, movimentacao.Destinatario);
        movimentacao.Banco.ShouldBeNull();
        movimentacao.Agencia.ShouldBeNull();
        movimentacao.Conta.ShouldBeNull();
        movimentacao.Dac.ShouldBeNull();
        movimentacao.ValorEfetivarSaldo.ShouldBe(valor);
    }

    [Fact(DisplayName = "Deve instanciar tipo deposito com valor efetivar positivo")]
    public void DeveInstanciarTipoDepositoComValorEfetivarSaldoPositivo()
    {
        
        var idUsuario = Guid.NewGuid();
        var tipo = TipoMovimentacao.Deposito;
        var forma = FormaMovimentacao.Pix;
        var valor = 100m;
        var destinatario = "Destinatario";

        
        var movimentacao = new Movimentacao(idUsuario, tipo, forma, valor, destinatario);

        
        Assert.Equal(valor, movimentacao.ValorEfetivarSaldo);
    }

    [Fact(DisplayName = "Deve instanciar tipo saque com valor efetivar saldo negativo")]
    public void DeveInstanciarTipoSaqueComValorEfetivarSaldoNegativo()
    {
        
        var idUsuario = Guid.NewGuid();
        var tipo = TipoMovimentacao.Saque;
        var forma = FormaMovimentacao.Doc;
        var valor = 100m;
        var destinatario = "Destinatario";

        
        var movimentacao = new Movimentacao(idUsuario, tipo, forma, valor, destinatario);

        
        movimentacao.ValorEfetivarSaldo.ShouldBe(-valor);
    }
}