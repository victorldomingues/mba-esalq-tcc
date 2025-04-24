namespace Api.Movimentacoes.Testes.TestesIntegracao.Adaptadores.Primarios;

internal class RespostaErro
{
    public IEnumerable<Erro> Erros { get; set; }
}

internal class Erro
{
    public string Codigo { get; set; }
    public string Mensagem { get; set; }
    public string? Propriedade { get; set; }
}