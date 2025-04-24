using Api.Saldos.Dominio.Portas;
using Api.Saldos.Infra.Dados.Contextos;
using Api.Saldos.Infra.Dados.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;

namespace Api.Saldos.Infra.Dados;

public static class InfraDadosExtensao
{
    public static IServiceCollection AdicionarBancoDeDados(this IServiceCollection services, string stringConexao)
    {
        services.AddSingleton<IPostgresDbContext, PostgresDbContexto>(_ => new PostgresDbContexto(stringConexao));
        services.AddScoped<ISaldoRepositorio, SaldoRepositorio>();

        return services;
    }

    public static IServiceCollection AdicionarCache(this IServiceCollection services, string stringConexao)
    {
        services.TryAddSingleton<IConnectionMultiplexer>(_ =>
        {
            var redis = ConnectionMultiplexer.Connect(stringConexao);
            return redis;
        });

        services.AddSingleton<ISaldoCacheRepositorio, SaldoCacheRepositorio>();
        return services;
    }
}