using System.Text.Json;
using Api.Saldos.Dominio.Portas;
using Api.Saldos.Dominio.SaldoAgregado.ObjetosTransferencia;
using StackExchange.Redis;

namespace Api.Saldos.Infra.Dados.Repositorios;

public class SaldoCacheRepositorio : ISaldoCacheRepositorio
{
    
    private readonly IConnectionMultiplexer _redis;

    public SaldoCacheRepositorio(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task<Saldo?> Recuperar(Guid idUsuario)
    {
        var chave = ComporChave(idUsuario);
        var redis = _redis.GetDatabase();
        var json = await redis.StringGetAsync(chave);
        return json.HasValue ? JsonSerializer.Deserialize<Saldo>(json!) : null;
    }

    public async Task CachearSaldo(Saldo saldo, Guid idUsuario)
    {
        var chave = ComporChave(idUsuario);
        var redis = _redis.GetDatabase();
        var json = JsonSerializer.Serialize(saldo);
        await redis.StringSetAsync(chave, json, TimeSpan.FromMinutes(1));
    }

    public string ComporChave(Guid idUsuario) => $"saldo-{idUsuario}";
}