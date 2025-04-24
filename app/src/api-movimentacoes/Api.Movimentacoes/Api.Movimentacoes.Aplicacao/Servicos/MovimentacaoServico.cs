using Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;
using Api.Movimentacoes.Dominio.Portas;
using Artefatos.Notificacoes;

namespace Api.Movimentacoes.Aplicacao.Servicos;

public class MovimentacaoServico : IMovimentacaoServico
{
    private readonly IMovimentacaoUnitOfWork _movimentacaoUow;
    private readonly INotificacaoContexto _notificacaoContexto;
    private readonly IMovimentacaoRepositorio _repositorioMovimentacao;

    public MovimentacaoServico(IMovimentacaoUnitOfWork _movimentacaoUow,
        INotificacaoContexto notificacaoContexto, IMovimentacaoRepositorio repositorioMovimentacao)
    {
        this._movimentacaoUow = _movimentacaoUow;
        _notificacaoContexto = notificacaoContexto;
        _repositorioMovimentacao = repositorioMovimentacao;
    }

    public async Task Movimentar(Movimentacao movimentacao)
    {
        if (movimentacao.Valor <= 0)
        {
            _notificacaoContexto.AdicionarNotificacao(new Notificacao("Valor da transacao", "Valor da transacao invalido"));
            return;
        }
        //TODO: implementar cache
        await _movimentacaoUow.MovimentarAsync(movimentacao);
    }

    public async Task<IEnumerable<Movimentacao>> Listar(Guid idUsuario)
    {   var movimentacoes = await _repositorioMovimentacao.Listar(idUsuario);
        //TODO: implementar cache.
        return movimentacoes;
    }
}