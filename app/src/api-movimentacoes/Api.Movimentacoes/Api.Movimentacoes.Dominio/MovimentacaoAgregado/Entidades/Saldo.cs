using Api.Movimentacoes.Dominio.MovimentacaoAgregado.ObjetosValor;

namespace Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;

public class Saldo : Entidade
{
    protected Saldo()
    {
    }

    public Saldo(Movimentacao movimentacao)
    {
        IdUsuario = movimentacao.IdUsuario;
        Valor = movimentacao.Valor;
        CriadoEm = DateTime.Now;
        AtualizadoEm = CriadoEm;
        Situacao = Situacao.Ativo;
    }

    public Guid IdUsuario { get; set; }

    public decimal Valor { get; protected set; }

    public void AdicionarSaldo(Movimentacao movimentacao)
    {
        Valor += movimentacao.ValorEfetivarSaldo;
        AtualizadoEm = DateTime.Now;
    }
}