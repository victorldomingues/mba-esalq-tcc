namespace Artefatos.Notificacoes;

public interface INotificacaoContexto
{
    public IEnumerable<Notificacao> Notificacoes { get; }

    public bool ExisteNotificacao { get; }
    public void AdicionarNotificacao(Notificacao notificacao);
    
}