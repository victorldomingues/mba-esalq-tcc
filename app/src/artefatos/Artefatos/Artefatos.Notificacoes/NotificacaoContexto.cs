namespace Artefatos.Notificacoes;

public class NotificacaoContexto : INotificacaoContexto
{
    private readonly List<Notificacao> _notificacoes;

    public NotificacaoContexto()
    {
        _notificacoes = new List<Notificacao>();
    }
    
    public IEnumerable<Notificacao> Notificacoes => _notificacoes;
    
    public bool ExisteNotificacao => Notificacoes.Any();

    public void AdicionarNotificacao(Notificacao notificacao) => _notificacoes.Add(notificacao);
}