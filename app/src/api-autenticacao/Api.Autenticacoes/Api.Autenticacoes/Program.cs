using System.Security.Claims;
using Api.Autenticacoes.Aplicacao;
using Api.Autenticacoes.Aplicacao.Modelos;
using Api.Autenticacoes.Dados;
using Api.Autenticacoes.Dados.Contextos;
using Api.Autenticacoes.Dominio.Portas;
using Api.Autenticacoes.Modelos;
using Artefatos.Identidade.Correlacoes;
using Artefatos.Notificacoes;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

var chave = builder.Configuration["Seguranca:Chave"];
var stringConexaoRedis = builder.Configuration.GetConnectionString("Redis");
var stringConexaoPostgres = builder.Configuration.GetConnectionString("Postgres");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AdicionarBancoDeDados(stringConexaoPostgres!);
builder.Services.AdicionarCache(stringConexaoRedis!);
builder.Services.AdicionarNotificacoes();
builder.Services.AdicionarServicosAplicacao(new OpcoesDeSeguranca(chave!));
builder.Services.AddHttpContextAccessor();
builder.Services.TryAddScoped<CorrelationIdMessageHandler>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("localhost:4200", "localhost:80", "localhost")
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.EnrichNpgsqlDbContext<PostgresDbContexto>();
builder.AddRedisDistributedCache("Redis");

//TODO: informar instrumentação do aspire no tcc
builder.Services.AddProblemDetails();
builder.AddServiceDefaults();

var app = builder.Build();

//TODO: ver um melhor lugar para esse comando 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/login", async (LoginModelo loginModelo, IAutenticacaoServico autenticacaoServico,
        INotificacaoContexto notificacaoContexto) =>
    {
        var login = loginModelo.ParaLoginDto();
        var sessao = await autenticacaoServico.Logar(login);

        if (notificacaoContexto.ExisteNotificacao)
            return Results.Unauthorized();

        return Results.Ok(new { sessao!.AccessToken, IdSessao = sessao.Id });
    })
    .WithName("Login");

app.MapPost("/token_info", (INotificacaoContexto notificacaoContexto, HttpContext httpContext, IAutenticacaoServico autenticacaoServico) =>
    {
        app.Logger.LogInformation("Validação de token");
        
        if (httpContext.Request.Form.TryGetValue("token", out var tokens))
        {
            var token = tokens.First();
            var tokenValido = autenticacaoServico.ValidarToken(token!)!;
            if (tokenValido?.Identity?.IsAuthenticated == true && !notificacaoContexto.ExisteNotificacao)
            {
                var sub = RecuperarClaim(tokenValido, ClaimTypes.NameIdentifier);
                var email = RecuperarClaim(tokenValido, ClaimTypes.Email);
                return Results.Ok(new { active = true, sub, email });
            }
        }

        app.Logger.LogWarning("Unauthorized, não autorizado");

        return Results.Unauthorized();

        string RecuperarClaim(ClaimsPrincipal claimsPrincipal, string claimType)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == claimType)?.Value!;
        }
    })
    .WithName("TokenInfo");

app.UseHttpsRedirection();

app.UseCors();

app.UseCorrelationId();

//TODO: tratamento padrão para exceções inesperadas lançadas

// app.MapDefaultEndpoints();

app.Run();

public partial class Program
{
}