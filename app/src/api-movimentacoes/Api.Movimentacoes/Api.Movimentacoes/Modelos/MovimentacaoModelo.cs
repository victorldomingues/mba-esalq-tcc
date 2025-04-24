using Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;
using Api.Movimentacoes.Dominio.MovimentacaoAgregado.ObjetosValor;

namespace Api.Movimentacoes.Modelos;

public record MovimentacaoModelo
{
    public decimal Valor { get; set; }
    public TipoMovimentacao Tipo { get; set; }
    public FormaMovimentacao Forma { get; set; }
    
    public string? Destinatario { get; set; }
    public string? Banco { get; set; }
    public string? Agencia { get; set; }
    public string? Conta { get; set; }
    public string? Dac { get; set; }
    
    public Movimentacao ParaUsuario(Guid idUsuario)
    {
        return new Movimentacao(idUsuario, Tipo, Forma, Valor,Destinatario, Banco, Agencia, Conta, Dac?.ToCharArray()?.FirstOrDefault());
    }
}