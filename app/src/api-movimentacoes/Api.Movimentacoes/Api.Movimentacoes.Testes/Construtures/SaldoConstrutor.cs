using Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;
using Api.Movimentacoes.Dominio.MovimentacaoAgregado.ObjetosValor;

namespace Api.Movimentacoes.Testes.Construtures;

public class SaldoConstrutor
{
    private Saldo _saldo;

    protected SaldoConstrutor()
    {
        Resetar();
    }

    public SaldoConstrutor Resetar()
    {
        var movimentacao = new Movimentacao(
            Guid.NewGuid(),
            TipoMovimentacao.Deposito,
            FormaMovimentacao.Pix,
            100m,
            "Destinatario Padrão"
        );

        _saldo = new Saldo(movimentacao);
        return this;
    }

    public static SaldoConstrutor Construir()
    {
        return new SaldoConstrutor().Resetar();
    }

    public Saldo Instancia => _saldo;

    public SaldoConstrutor ComIdUsuario(Guid idUsuario)
    {
        _saldo.IdUsuario = idUsuario;
        return this;
    }
    
    public SaldoConstrutor AdicionarMovimentacao(Movimentacao movimentacao)
    {
        _saldo.AdicionarSaldo(movimentacao);
        return this;
    }
}