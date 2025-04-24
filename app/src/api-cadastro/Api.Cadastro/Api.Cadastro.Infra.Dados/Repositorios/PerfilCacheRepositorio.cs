using System.Text.Json;
using Api.Cadastro.Dominio.ClienteAgregado;
using Api.Cadastro.Dominio.Portas;
using StackExchange.Redis;

namespace Api.Cadastro.Data.Repositorios;

public class PerfilCacheRepositorio : IPerfilCacheRepositorio
{
    
    private readonly IConnectionMultiplexer _redis;

    public PerfilCacheRepositorio(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task<PerfilDto?> Recuperar(string chave)
    {
        var redis = _redis.GetDatabase();
        var json = await redis.StringGetAsync(chave);
        return json.HasValue ? JsonSerializer.Deserialize<PerfilDto>(json!) : null;
    }

    public async Task Cachear(string chave, PerfilDto perfilDto)
    {
        var agora = DateTime.Now;
        var redis = _redis.GetDatabase();
        var json = JsonSerializer.Serialize(perfilDto);
        await redis.StringSetAsync(chave, json, agora.AddMinutes(10) - agora);
    }
}