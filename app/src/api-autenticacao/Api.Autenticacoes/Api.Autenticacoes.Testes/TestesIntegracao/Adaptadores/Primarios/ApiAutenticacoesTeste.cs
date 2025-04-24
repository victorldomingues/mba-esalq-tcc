using System.Net.Http.Json;
using System.Text.Json;
using Api.Autenticacoes.Dominio.AutenticacaoAgregado.Entidades;
using Api.Autenticacoes.Dominio.AutenticacaoAgregado.ObjetosDeValor;
using Artefatos.Seguranca;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Shouldly;

namespace Api.Autenticacoes.Testes.TestesIntegracao.Adaptadores.Primarios;

public class ApiAutenticacoesTeste: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    private static readonly JsonSerializerOptions JsonOptions = new()
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public ApiAutenticacoesTeste(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact(DisplayName = "Deve realizar login com credenciais válidas")]
    public async Task DeveRealizarLoginComCredenciaisValidas()
    {
        var usuario = new Usuario()
        {
            Id = Guid.NewGuid(),
            Email = "email@email.com",
            Senha = Criptografia.CriptografarSHA256("senha123"),
            Situacao = Situacao.Ativo,
            Cpf = "12345678910",
        };
        var clienteHttp = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.UsarBancoEmMemoria();
                    services.ConfigurarUsuarioPadrao(usuario);
                });
            })
            .CreateClient();

        var loginModelo = new { Cpf =usuario.Cpf, Senha = "senha123" };

        var resposta = await clienteHttp.PostAsJsonAsync("/login", loginModelo, JsonOptions);

        resposta.IsSuccessStatusCode.ShouldBeTrue();
        var conteudo = await resposta.Content.ReadFromJsonAsync<SessaoSucesso>();
        conteudo.ShouldNotBeNull();
        conteudo!.AccessToken.ShouldNotBeNullOrEmpty();
    }

    [Fact(DisplayName = "Deve retornar não autorizado ao realizar login com credenciais inválidas")]
    public async Task DeveRetornarNaoAutorizadoAoRealizarLoginComCredenciaisInvalidas()
    {
        var clienteHttp = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.UsarBancoEmMemoria();
                });
            })
            .CreateClient();

        var loginModelo = new { Cpf = "12345678910", Senha = "senhaErrada" };

        var resposta = await clienteHttp.PostAsJsonAsync("/login", loginModelo, JsonOptions);

        resposta.StatusCode.ShouldBe(System.Net.HttpStatusCode.Unauthorized);
    }

    [Fact(DisplayName = "Deve validar token e retornar informações")]
    public async Task DeveValidarTokenERetornarInformacoes()
    {
        var usuario = new Usuario()
        {
            Id = Guid.NewGuid(),
            Email = "email@email.com",
            Senha = Criptografia.CriptografarSHA256("senha123"),
            Situacao = Situacao.Ativo,
            Cpf = "12345678910",
        };
        var clienteHttp = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.UsarBancoEmMemoria();
                    services.ConfigurarUsuarioPadrao(usuario);
                });
            })
            .CreateClient();

        var loginModelo = new { Cpf = usuario.Cpf, Senha = "senha123" };
        var loginResposta = await clienteHttp.PostAsJsonAsync("/login", loginModelo, JsonOptions);
        var loginConteudo = await loginResposta.Content.ReadFromJsonAsync<SessaoSucesso>();
        var token = loginConteudo!.AccessToken;

        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("token", token)
        });

        var resposta = await clienteHttp.PostAsync("/token_info", formContent);

        resposta.IsSuccessStatusCode.ShouldBeTrue();
        var conteudo = await resposta.Content.ReadFromJsonAsync<AutorizacaoSucesso>();
        conteudo.ShouldNotBeNull();
        conteudo!.Active.ShouldBeTrue();
        conteudo.Sub.ShouldNotBeNullOrEmpty();
        conteudo.Email.ShouldNotBeNullOrEmpty();
    }
}

public class SessaoSucesso
{
    public string AccessToken { get; set; }
}

public class AutorizacaoSucesso
{
    public bool Active { get; set; }
    public string Sub { get; set; }
    public string Email { get; set; }
}
