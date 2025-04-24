using Api.Cadastro.Data.Contextos;
using Api.Cadastro.Data.Repositorios;
using Api.Cadastro.Dominio.Portas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;

namespace Api.Cadastro.Data;

public static class InfraDadoExtensao
{
    public static IServiceCollection AdicionarBancoDeDados(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");
        services.AddDbContext<PostgresDbContext>( options=> {
            options.UsePostgres(connectionString);
        });
        services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
        services.AddScoped<IPerfilRepositorio, PerfilRepositorio>();
        return services;
    }
    
    public static IServiceCollection AdicionarCache(this IServiceCollection services, string stringConexao)
    {
       
        services.TryAddSingleton<IConnectionMultiplexer>(_ =>
        {
            var redis = ConnectionMultiplexer.Connect(stringConexao);
            return redis;
        });
        services.AddScoped<IPerfilCacheRepositorio, PerfilCacheRepositorio>();

        return services;
    }
    
    private static DbContextOptionsBuilder UsePostgres(this DbContextOptionsBuilder options, string connectionString)
    {
        return options.UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention();
    }
}