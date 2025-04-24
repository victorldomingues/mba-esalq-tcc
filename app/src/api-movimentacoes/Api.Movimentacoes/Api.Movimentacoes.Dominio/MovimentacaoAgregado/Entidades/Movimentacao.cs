using Api.Movimentacoes.Dominio.MovimentacaoAgregado.ObjetosValor;

namespace Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;

public class Movimentacao : Entidade
{
    protected Movimentacao(){}
    
    public Movimentacao(Guid idUsuario,  TipoMovimentacao tipo, FormaMovimentacao forma, decimal valor, 
        string destinatario, string? banco = null, string? agencia = null, string? conta = null, char? dac = null)
    {
        IdUsuario = idUsuario;
        Valor = valor;
        Tipo = tipo;
        Forma = forma;
        Destinatario = destinatario;
        Id = Guid.NewGuid();
        CriadoEm = DateTime.Now;
        Situacao = Situacao.Ativo;
        Banco = banco;
        Agencia = agencia;
        Conta = conta; 
        Dac = dac;
        ValorEfetivarSaldo = (Tipo == TipoMovimentacao.Deposito ? Valor : Valor * -1);
    }

    public Guid Id { get; set; }
    public Guid IdUsuario { get; set; }
    public decimal Valor { get; set; }
    public TipoMovimentacao Tipo { get; set; }
    public FormaMovimentacao Forma { get; set; }
    
    public string Destinatario { get; set; }
    public string? Banco { get; set; }
    public string? Agencia { get; set; }
    public string? Conta { get; set; }
    public char? Dac { get; set; }

    public decimal ValorEfetivarSaldo { get; protected set; } 
    
}