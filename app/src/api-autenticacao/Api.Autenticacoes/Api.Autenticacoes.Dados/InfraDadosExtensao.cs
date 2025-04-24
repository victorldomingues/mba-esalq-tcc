using Api.Autenticacoes.Dados.Contextos;
using Api.Autenticacoes.Dados.Repositorios;
using Api.Autenticacoes.Dominio.AutenticacaoAgregado.Entidades;
using Api.Autenticacoes.Dominio.Portas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;

namespace Api.Autenticacoes.Dados;

public static class InfraDadosExtensao
{
    public static IServiceCollection AdicionarBancoDeDados(this IServiceCollection services, string stringConexao)
    {

        services.AddDbContext<PostgresDbContexto>( options=> {
            options.UsePostgres(stringConexao);
        });
        services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
        
        return services;
    }

    public static IServiceCollection AdicionarCache(this IServiceCollection services, string stringConexao)
    {
        
        services.TryAddSingleton<IConnectionMultiplexer>(_ =>
        {
            var redis = ConnectionMultiplexer.Connect(stringConexao);
            return redis;
        });
        services.AddSingleton<ISessaoCacheRepositorio, SessaoCacheRepositorio>();
        return services;
    }
    
    private static DbContextOptionsBuilder UsePostgres(this DbContextOptionsBuilder options, string connectionString)
    {
        return options.UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention();
    }
}