using System.Net.Http.Json;
using System.Text.Json;
using Api.Movimentacoes.Testes.Construtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Shouldly;

namespace Api.Movimentacoes.Testes.TestesIntegracao.Adaptadores.Primarios;

public class ApiMovimentacaoTeste : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    private static readonly JsonSerializerOptions JsonOptions = new()
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public ApiMovimentacaoTeste(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact(DisplayName = "Deve realizar uma movimentacao")]
    public async Task DeveRealizarMovimentacao()
    {
        var clienteHttp = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.UsarBancoEmMemoria();
                    services.UsarAutorizacaoTeste();
                });
            })
            .CreateClient();

        var modelo = MovimentacaoModeloConstrutor.Construir().Instancia;

        var resposta = await clienteHttp.PostAsJsonAsync("/movimentacoes", modelo,
            JsonOptions);

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
                    services.UsarAutorizacaoTeste();
                });
            })
            .CreateClient();

        var modelo = MovimentacaoModeloConstrutor.Construir().Invalida().Instancia;
        var resposta = await clienteHttp.PostAsJsonAsync("/movimentacoes", modelo, JsonOptions);
        var respostaErros = await resposta.Content.ReadFromJsonAsync<RespostaErro>();

        resposta.IsSuccessStatusCode.ShouldBeFalse();
        respostaErros?.Erros.ShouldNotBeNull();
        respostaErros?.Erros.ShouldNotBeEmpty();

        //TODO: rever erros de validação incluir cada campo conforma a validação doc e ted para banco agencia conta e dac
    }
}