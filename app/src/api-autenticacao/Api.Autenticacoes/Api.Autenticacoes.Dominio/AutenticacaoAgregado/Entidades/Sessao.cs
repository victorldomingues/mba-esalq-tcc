namespace Api.Autenticacoes.Dominio.AutenticacaoAgregado.Entidades;

public class Sessao
{
    public Sessao()
    {
    }

    public Sessao(Guid idUsuario, DateTime criadoEm, DateTime expiraEm, string accessToken)
    {
        Chave = $"sessao-{idUsuario}";
        AccessToken = accessToken;
        Id = Guid.NewGuid();
        IdUsuario = idUsuario;
        CriadoEm = criadoEm;
        ExpiraEm = expiraEm;
    }

    public Guid Id { get; set; }
    public Guid IdUsuario { get; set; }
    public string Chave { get; set; }
    public string AccessToken { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime ExpiraEm { get; set; }
}