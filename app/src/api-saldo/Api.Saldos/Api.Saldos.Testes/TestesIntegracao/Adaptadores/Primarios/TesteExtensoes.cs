using Api.Saldos.Dominio.Portas;
using Api.Saldos.Dominio.SaldoAgregado.ObjetosTransferencia;
using Api.Saldos.Infra.Dados.Contextos;
using Artefatos.Identidade;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StackExchange.Redis;

namespace Api.Saldos.Testes.TestesIntegracao.Adaptadores.Primarios;

public static class TesteExtensoes
{
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
    
    public static IServiceCollection UsarBancoMocado(this IServiceCollection services, bool habilitaCache = true)
    {
        services.AddSingleton(Mock.Of<IConnectionMultiplexer>());
        var mockDbContext = new Mock<IPostgresDbContext>();
        var mockSaldoRepositorio = new Mock<ISaldoRepositorio>();
        var mockSaldoCacheRespositorio = new Mock<ISaldoCacheRepositorio>();
        var saldo = new Saldo() { AtualizadoEm = DateTime.Now, Valor = 130m };
        
        if(habilitaCache)
            mockSaldoCacheRespositorio.Setup(mockDbContext=> mockDbContext
                    .Recuperar(It.IsAny<Guid>()))
                .ReturnsAsync(saldo);
        
        mockSaldoRepositorio.Setup(mockDbContext=> mockDbContext
                .Recuperar(It.IsAny<Guid>()))
            .ReturnsAsync(saldo);
        
        services.Remove(services.Single(service => service.ServiceType == typeof(IPostgresDbContext)));
        services.Remove(services.Single(service => service.ServiceType == typeof(ISaldoRepositorio)));
        services.Remove(services.Single(service => service.ServiceType == typeof(ISaldoCacheRepositorio)));
        services.AddSingleton<IPostgresDbContext>(_ => mockDbContext.Object );
        services.AddScoped<ISaldoRepositorio>(_ => mockSaldoRepositorio.Object);
        services.AddScoped<ISaldoCacheRepositorio>(_ => mockSaldoCacheRespositorio.Object);
        
        return services;
    }
}