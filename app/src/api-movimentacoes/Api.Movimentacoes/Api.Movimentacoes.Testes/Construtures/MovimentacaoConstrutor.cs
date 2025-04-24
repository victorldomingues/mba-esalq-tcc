using Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;
using Api.Movimentacoes.Dominio.MovimentacaoAgregado.ObjetosValor;

namespace Api.Movimentacoes.Testes.Construtures;

public class MovimentacaoConstrutor
{
    private Movimentacao _movimentacao;

    protected MovimentacaoConstrutor()
    {
        Resetar();
    }

    public MovimentacaoConstrutor Resetar()
    {
        _movimentacao = new Movimentacao(
            Guid.NewGuid(),
            TipoMovimentacao.Deposito,
            FormaMovimentacao.Pix,
            100m,
            "Destinatario Padrão",
            "Banco Padrão",
            "Agencia Padrão",
            "Conta Padrão",
            '0'
        );
        return this;
    }

    public static MovimentacaoConstrutor Construir()
    {
        return new MovimentacaoConstrutor().Resetar();
    }

    public Movimentacao Instancia => _movimentacao;

    public MovimentacaoConstrutor ComIdUsuario(Guid idUsuario)
    {
        _movimentacao.IdUsuario = idUsuario;
        return this;
    }

    public MovimentacaoConstrutor ComTipo(TipoMovimentacao tipo)
    {
        _movimentacao.Tipo = tipo;
        return this;
    }

    public MovimentacaoConstrutor ComForma(FormaMovimentacao forma)
    {
        _movimentacao.Forma = forma;
        return this;
    }

    public MovimentacaoConstrutor ComValor(decimal valor)
    {
        _movimentacao.Valor = valor;
        return this;
    }

    public MovimentacaoConstrutor ComDestinatario(string destinatario)
    {
        _movimentacao.Destinatario = destinatario;
        return this;
    }

    public MovimentacaoConstrutor ComBanco(string? banco)
    {
        _movimentacao.Banco = banco;
        return this;
    }

    public MovimentacaoConstrutor ComAgencia(string? agencia)
    {
        _movimentacao.Agencia = agencia;
        return this;
    }

    public MovimentacaoConstrutor ComConta(string? conta)
    {
        _movimentacao.Conta = conta;
        return this;
    }

    public MovimentacaoConstrutor ComDac(char? dac)
    {
        _movimentacao.Dac = dac;
        return this;
    }

    public MovimentacaoConstrutor ComFormaTransferencia(FormaMovimentacao forma)
    {
        _movimentacao.Forma = forma;
        return this;
    }
}