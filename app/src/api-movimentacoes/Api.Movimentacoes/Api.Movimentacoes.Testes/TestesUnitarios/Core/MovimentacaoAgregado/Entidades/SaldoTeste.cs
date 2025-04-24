using Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;
using Api.Movimentacoes.Dominio.MovimentacaoAgregado.ObjetosValor;
using Shouldly;

namespace Api.Movimentacoes.Testes.TestesUnitarios.Core.MovimentacaoAgregado.Entidades;

public class SaldoTeste
{
    [Fact(DisplayName = "Deve criar Saldo com valores corretos")]
    public void DeveCriarSaldoComValoresCorretos()
    {
        var movimentacao = new Movimentacao(
            Guid.NewGuid(),
            TipoMovimentacao.Deposito,
            FormaMovimentacao.Pix,
            100m,
            "Destinatario"
        );


        var saldo = new Saldo(movimentacao);


        saldo.IdUsuario.ShouldBe(movimentacao.IdUsuario);
        saldo.Valor.ShouldBe(movimentacao.Valor);
        saldo.Situacao.ShouldBe(Situacao.Ativo);
    }

    [Fact(DisplayName = "Deve adicionar saldo corretamente")]
    public void DeveAdicionarSaldoCorretamente()
    {
        var movimentacaoInicial = new Movimentacao(
            Guid.NewGuid(),
            TipoMovimentacao.Deposito,
            FormaMovimentacao.Pix,
            100m,
            "Destinatario"
        );
        var saldo = new Saldo(movimentacaoInicial);

        var movimentacaoAdicional = new Movimentacao(
            Guid.NewGuid(),
            TipoMovimentacao.Deposito,
            FormaMovimentacao.Pix,
            50m,
            "Destinatario"
        );


        saldo.AdicionarSaldo(movimentacaoAdicional);


        saldo.Valor.ShouldBe(150m);
    }

    [Fact(DisplayName = "Deve subtrair saldo corretamente")]
    public void DeveSubtrairSaldoCorretamente()
    {
        var movimentacaoInicial = new Movimentacao(
            Guid.NewGuid(),
            TipoMovimentacao.Deposito,
            FormaMovimentacao.Pix,
            100m,
            "Destinatario"
        );
        var saldo = new Saldo(movimentacaoInicial);

        var movimentacaoSubtracao = new Movimentacao(
            Guid.NewGuid(),
            TipoMovimentacao.Saque,
            FormaMovimentacao.Pix,
            50m,
            "Destinatario"
        );


        saldo.AdicionarSaldo(movimentacaoSubtracao);


        saldo.Valor.ShouldBe(50m);
    }
}