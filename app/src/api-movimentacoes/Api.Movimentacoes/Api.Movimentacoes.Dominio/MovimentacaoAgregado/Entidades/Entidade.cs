using Api.Movimentacoes.Dominio.MovimentacaoAgregado.ObjetosValor;

namespace Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;

public abstract class Entidade
{
    public DateTime CriadoEm { get; set; }
    public DateTime? AtualizadoEm { get; set; }
    public DateTime? DeletadoEm { get; set; }
    public Situacao Situacao { get; set; }
}