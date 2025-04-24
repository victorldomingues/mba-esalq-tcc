using System.ComponentModel.DataAnnotations;
using Api.Autenticacoes.Dados.Contextos;
using Api.Autenticacoes.Dominio.AutenticacaoAgregado.Entidades;
using Api.Autenticacoes.Dominio.Portas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StackExchange.Redis;

namespace Api.Autenticacoes.Testes.TestesIntegracao.Adaptadores.Primarios;

public static class TesteExtensoes
{
    public static IServiceCollection UsarBancoEmMemoria(this IServiceCollection services)
    {
        services.Remove(services.Single(service => service.ServiceType == typeof(DbContextOptions<PostgresDbContexto>)));
        services.AddDbContext<PostgresDbContexto>(options =>
            options.UseInMemoryDatabase("memorydb")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
        );
        services.Remove(services.Single(service => service.ServiceType == typeof(ISessaoCacheRepositorio)));
        services.AddSingleton(Mock.Of<ISessaoCacheRepositorio>());
        services.AddSingleton(Mock.Of<IConnectionMultiplexer>());
        return services;
    }
    
    public static IServiceCollection ConfigurarUsuarioPadrao(this IServiceCollection services, Usuario usuario)
    {
        using (var provider = services.BuildServiceProvider())
        {
            var context = provider.GetService<PostgresDbContexto>();
            context.Usuarios.Add(usuario);
            context.SaveChanges();
        }

        return services;
    }
}