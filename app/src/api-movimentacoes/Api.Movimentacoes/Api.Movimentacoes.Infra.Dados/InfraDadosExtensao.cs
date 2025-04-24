using System.Diagnostics.CodeAnalysis;
using Api.Movimentacoes.Infra.Dados.Contextos;
using Api.Movimentacoes.Infra.Dados.Repositorios;
using Api.Movimentacoes.Dominio.Portas;
using Microsoft.EntityFrameworkCore;

namespace Api.Movimentacoes.Infra.Dados;

public static class InfraDadosExtensao
{
    public static IServiceCollection AdicionarBancoDeDados(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");
        
        services.AddDbContext<PostgresDbContexto>( options=> {
            options.UsePostgres(connectionString);
        });
        
        services.AddScoped<IMovimentacaoRepositorio, MovimentacaoRepositorio>();
        services.AddScoped<ISaldoRepositorio, SaldoRepositorio>();
        services.AddScoped<IMovimentacaoUnitOfWork, MovimentacaoUnitOfWork>();
        
        return services;
    }
    
    [ExcludeFromCodeCoverage]
    public static DbContextOptionsBuilder UsePostgres(this DbContextOptionsBuilder options, string connectionString)
    {
        return options.UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention();
    }
}