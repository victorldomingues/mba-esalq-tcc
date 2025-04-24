using System.Net.Http.Json;
using System.Text.Json;
using Api.Cadastro.Data.Contextos;
using Api.Cadastro.Testes.Construtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using StackExchange.Redis;

namespace Api.Cadastro.Testes.TestesIntegracao.Adaptadores.Primarios;

public class ApiCadastroTeste : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    private static JsonSerializerOptions _jsonOptions = new JsonSerializerOptions()
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public ApiCadastroTeste(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact(DisplayName = "Deve cadastrar um cliente")]
    public async Task DeveCadastrarCliente()
    {
        var clienteHttp = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.UsarBancoEmMemoria();
                });
            })
            .CreateClient();

        var modelo = CadastroClienteModeloBuilder.Construir().Instancia;

        var resposta = await clienteHttp.PostAsJsonAsync("/cadastros", modelo, _jsonOptions);
        
        resposta.IsSuccessStatusCode.ShouldBeTrue();
    }
    
    //TODO: Incluir mais cenários de validação
    [Fact(DisplayName = "Deve retornar validar dados incorretos")]
    public async Task DeveValidarDadosIncorrestos()
    {
        
        var clienteHttp = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.UsarBancoEmMemoria();
                });
            })
            .CreateClient();

        var modelo = CadastroClienteModeloBuilder.Construir().Instancia;
        modelo.Cpf = "";
        modelo.Senha = "";
        var resposta = await clienteHttp.PostAsJsonAsync("/cadastros", modelo, _jsonOptions);
        var respostaErros = await resposta.Content.ReadFromJsonAsync<RespostaErro>();
        
        resposta.IsSuccessStatusCode.ShouldBeFalse();
        respostaErros.Erros.ShouldNotBeNull();
        respostaErros.Erros.ShouldNotBeEmpty();
        respostaErros.Erros?.Select(x=> x.Propriedade).ShouldContain("Cpf");
        respostaErros.Erros?.Select(x=> x.Propriedade).ShouldContain("ConfirmaSenha");

    }
}

internal static class BancoEmMemoriaExtensao
{
    public static IServiceCollection UsarBancoEmMemoria(this IServiceCollection services)
    {
        services.Remove(services.Single(service => service.ServiceType == typeof(DbContextOptions<PostgresDbContext>)));
        services.AddDbContext<PostgresDbContext>(options =>
            options.UseInMemoryDatabase("memorydb"));
        services.AddSingleton<IConnectionMultiplexer>(Mock.Of<IConnectionMultiplexer>());
        return services;
    }
}

internal class RespostaErro
{
        public IEnumerable<Erro> Erros { get; set; }
}

internal class Erro
{
    public string Codigo { get; set; }
    public string Mensagem {get; set; }
    public string? Propriedade { get; set; }
}
