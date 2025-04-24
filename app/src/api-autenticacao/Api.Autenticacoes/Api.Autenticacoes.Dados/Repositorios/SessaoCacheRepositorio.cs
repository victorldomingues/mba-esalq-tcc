using System.Text.Json;
using Api.Autenticacoes.Dominio.AutenticacaoAgregado.Entidades;
using Api.Autenticacoes.Dominio.Portas;
using StackExchange.Redis;

namespace Api.Autenticacoes.Dados.Repositorios;

public class SessaoCacheRepositorio : ISessaoCacheRepositorio
{
    private readonly IConnectionMultiplexer _redis;

    public SessaoCacheRepositorio(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }
    
    public async Task CachearSessaoAsync(Sessao sessao)
    {
        var redis = _redis.GetDatabase();
        var json = JsonSerializer.Serialize(sessao);
        await redis.StringSetAsync(sessao.Chave, json,  sessao.ExpiraEm - sessao.CriadoEm);
    }

    public async Task<Sessao?> RecuperarSessaoAsync(string chave)
    {
        var redis = _redis.GetDatabase();
        var json = await redis.StringGetAsync(chave);
        return json.HasValue ? JsonSerializer.Deserialize<Sessao>(json!) : null;
    }
}