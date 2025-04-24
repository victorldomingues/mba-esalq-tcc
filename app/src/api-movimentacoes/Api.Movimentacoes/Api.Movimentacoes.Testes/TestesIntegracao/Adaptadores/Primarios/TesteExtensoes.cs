using Api.Movimentacoes.Infra.Dados.Contextos;
using Artefatos.Identidade;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Api.Movimentacoes.Testes.TestesIntegracao.Adaptadores.Primarios;

internal static class TesteExtensoes
{
    public static IServiceCollection UsarBancoEmMemoria(this IServiceCollection services)
    {
        services.Remove(services.Single(service => service.ServiceType == typeof(DbContextOptions<PostgresDbContexto>)));
        services.AddDbContext<PostgresDbContexto>(options =>
            options.UseInMemoryDatabase("memorydb")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
        );
        return services;
    }

    public static IServiceCollection UsarAutorizacaoTeste(this IServiceCollection services)
    {
        services.AddAuthentication(defaultScheme: "TestScheme")
            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                "TestScheme", options => { });
        
        var mockUsuarioLogado = new Mock<IUsuarioLogado>();
        
        mockUsuarioLogado.Setup(x => x.Recuperar()).Returns(new UsuarioLogadoDto()
            { EstaLogado = true, Email = "teste@teste.com", Id = Guid.NewGuid() });
        
        services.AddSingleton(mockUsuarioLogado.Object);
        
        return services;
    }
}