using System.Runtime.CompilerServices;
using Api.Movimentacoes.Dominio.MovimentacaoAgregado.ObjetosValor;
using Api.Movimentacoes.Modelos;

namespace Api.Movimentacoes.Testes.Construtures;

public class MovimentacaoModeloConstrutor
{
    private MovimentacaoModelo _movimentacaoModelo;

    protected MovimentacaoModeloConstrutor()
    {
        Resetar();
    }

    public MovimentacaoModeloConstrutor Resetar()
    {
        _movimentacaoModelo = new MovimentacaoModelo()
        {
            Valor = 100m,
            Tipo = TipoMovimentacao.Deposito,
            Forma = FormaMovimentacao.Pix,
            Destinatario = "Destinatario Padrão",
            Banco = "Banco Padrão",
            Agencia = "Agencia Padrão",
            Conta = "Conta Padrão",
            Dac = "0"
        };
        return this;
    }

    public static MovimentacaoModeloConstrutor Construir()
    {
        return new MovimentacaoModeloConstrutor().Resetar();
    }

    public MovimentacaoModelo Instancia => _movimentacaoModelo;

    public MovimentacaoModeloConstrutor ComValor(decimal valor)
    {
        _movimentacaoModelo.Valor = valor;
        return this;
    }

    public MovimentacaoModeloConstrutor ComTipo(TipoMovimentacao tipo)
    {
        _movimentacaoModelo.Tipo = tipo;
        return this;
    }

    public MovimentacaoModeloConstrutor ComForma(FormaMovimentacao forma)
    {
        _movimentacaoModelo.Forma = forma;
        return this;
    }

    public MovimentacaoModeloConstrutor ComDestinatario(string destinatario)
    {
        _movimentacaoModelo.Destinatario = destinatario;
        return this;
    }

    public MovimentacaoModeloConstrutor ComBanco(string? banco)
    {
        _movimentacaoModelo.Banco = banco;
        return this;
    }

    public MovimentacaoModeloConstrutor ComAgencia(string? agencia)
    {
        _movimentacaoModelo.Agencia = agencia;
        return this;
    }

    public MovimentacaoModeloConstrutor ComConta(string? conta)
    {
        _movimentacaoModelo.Conta = conta;
        return this;
    }

    public MovimentacaoModeloConstrutor ComDac(string? dac)
    {
        _movimentacaoModelo.Dac = dac;
        return this;
    }

    public MovimentacaoModeloConstrutor Invalida()
    {
        this.ComAgencia(null)
            .ComConta(null)
            .ComBanco(null)
            .ComDac(null)
            .ComForma(FormaMovimentacao.Doc);
        return this;
    }
}