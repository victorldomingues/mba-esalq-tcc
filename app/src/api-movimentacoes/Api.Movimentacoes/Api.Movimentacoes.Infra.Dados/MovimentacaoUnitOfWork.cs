using System.Text;
using Api.Movimentacoes.Dominio.MovimentacaoAgregado.Entidades;
using Api.Movimentacoes.Dominio.MovimentacaoAgregado.ObjetosValor;
using Api.Movimentacoes.Dominio.Portas;
using Api.Movimentacoes.Infra.Dados.Contextos;
using Artefatos.Notificacoes;

namespace Api.Movimentacoes.Infra.Dados;

public class MovimentacaoUnitOfWork : IMovimentacaoUnitOfWork
{
    private readonly IMovimentacaoRepositorio _movimentacaoRepositorio;
    private readonly ISaldoRepositorio _saldoRepositorio;
    private readonly PostgresDbContexto _contexto;
    private readonly INotificacaoContexto _notificacaoContexto;
    private readonly ILogger<MovimentacaoUnitOfWork> _logger;

    public MovimentacaoUnitOfWork(ILogger<MovimentacaoUnitOfWork> logger, PostgresDbContexto contexto, IMovimentacaoRepositorio movimentacaoRepositorio,
        ISaldoRepositorio saldoRepositorio,
        INotificacaoContexto notificacaoContexto)
    {
        _movimentacaoRepositorio = movimentacaoRepositorio;
        _saldoRepositorio = saldoRepositorio;
        _contexto = contexto;
        _notificacaoContexto = notificacaoContexto;
        _logger = logger;
    }

    public async Task MovimentarAsync(Movimentacao movimentacao)
    {
        await using var transaction = await _contexto.Database.BeginTransactionAsync();

        try
        {
            var saldo = await _saldoRepositorio.RecuperarAsync(movimentacao.IdUsuario);
            
            if (movimentacao.Tipo == TipoMovimentacao.Saque)
            {
                if (saldo == null || saldo.Valor <= 0 ||  saldo.Valor - movimentacao.Valor < 0)
                {
                    _notificacaoContexto.AdicionarNotificacao(new Notificacao("Saldo", "Saldo insuficiente"));
                    await transaction.RollbackAsync();
                    _logger.LogWarning($" Cliente: {movimentacao.IdUsuario} - Saldo insuficiente");
                    return;
                }
                
                saldo.AdicionarSaldo(movimentacao);
                await _movimentacaoRepositorio.Movimentar(movimentacao);
                await _saldoRepositorio.AtualizarSaldoAsync(saldo);
            }
            else
            {
                await _movimentacaoRepositorio.Movimentar(movimentacao);
                
                if (saldo == null)
                {
                    saldo = new Saldo(movimentacao);
                    await _saldoRepositorio.SalvarSaldoAsync(saldo);
                }
                else
                {
                    saldo.AdicionarSaldo(movimentacao);
                    await _saldoRepositorio.AtualizarSaldoAsync(saldo);
                }
            }
            
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            _notificacaoContexto.AdicionarNotificacao(new Notificacao("Transação", "Erro ao efetivar transação"));
            await transaction.RollbackAsync();
            using (_logger.BeginScope($"ERRO TRANSACAO CLIENTE {movimentacao.IdUsuario}")){
                _logger.LogError($" Exception: {e.Message}");
            }
        }
    }
    
}