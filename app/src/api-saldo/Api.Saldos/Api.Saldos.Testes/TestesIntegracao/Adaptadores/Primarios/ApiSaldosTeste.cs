using System.Net.Http.Json;
using System.Text.Json;
using Api.Saldos.Dominio.Portas;
using Api.Saldos.Dominio.SaldoAgregado.ObjetosTransferencia;
using Api.Saldos.Infra.Dados.Contextos;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;

namespace Api.Saldos.Testes.TestesIntegracao.Adaptadores.Primarios;

public class ApiSaldosTeste : IClassFixture<WebApplicationFactory<Program>>
{
    
    private readonly WebApplicationFactory<Program> _factory;

    private static readonly JsonSerializerOptions JsonOptions = new()
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public ApiSaldosTeste(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact(DisplayName = "Deve recuperar saldo via http get")]
    public async Task DeveRecuperarSaldoViaHttpGet()
    {
        var clienteHttp = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.UsarBancoMocado();
                    services.UsarAutorizacaoTeste();
                });
            })
            .CreateClient();
        
        var resposta = await clienteHttp.GetAsync("/saldos");
        var saldo = resposta.Content.ReadFromJsonAsync<Saldo>(JsonOptions);
        resposta.IsSuccessStatusCode.ShouldBeTrue();
    }
    
    [Fact(DisplayName = "Deve recuperar saldo via http get sem cache")]
    public async Task DeveRecuperarSaldoViaHttpGetSemCache()
    {
        var clienteHttp = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.UsarBancoMocado(false);
                    services.UsarAutorizacaoTeste();
                });
            })
            .CreateClient();
        
        var resposta = await clienteHttp.GetAsync("/saldos");
        var saldo = resposta.Content.ReadFromJsonAsync<Saldo>(JsonOptions);
        resposta.IsSuccessStatusCode.ShouldBeTrue();
    }
}
