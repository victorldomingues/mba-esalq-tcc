using System.Diagnostics.CodeAnalysis;
using Api.Saldos.Aplicacao;
using Api.Saldos.Dominio.Portas;
using Api.Saldos.Extensoes;
using Api.Saldos.Infra.Dados;
using Api.Saldos.Modelos;
using Artefatos.Identidade;
using Artefatos.Identidade.Correlacoes;
using IdentityModel.AspNetCore.OAuth2Introspection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var configuracaoRedis = builder.Configuration.GetSection("ConnectionStrings")["Redis"];
var configuracaoPostgres = builder.Configuration.GetSection("ConnectionStrings")["Postgres"];

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AdicionarServiceosAplicacao();
builder.Services.AdicionarBancoDeDados(configuracaoPostgres!);
builder.Services.AdicionarCache(configuracaoRedis!);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(OAuth2IntrospectionDefaults.AuthenticationScheme)
    .AddOAuth2Introspection(
        opcoes => { opcoes.IntrospectionEndpoint = builder.Configuration["IntrospectionEndpoint"]; });
builder.Services.AddHttpClient(OAuth2IntrospectionDefaults.BackChannelHttpClientName)
    .AddHttpMessageHandler<CorrelationIdMessageHandler>();

builder.Services.AdicionarIdentidade();
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

//TODO: informar instrumentação do aspire no tcc
builder.Services.AddProblemDetails();
// builder.AddServiceDefaults();
builder.AddDefaultHealthChecksProduction();


var app = builder.Build();

//TODO: Colocar a configuração do dapper em um lugar mais apropriado
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/saldos",
        async (ISaldoServico saldoServico, IUsuarioLogado usuarioLogado) =>
        {
            var logger = app.Logger;
            await AplicarConfiguracoes();
            return Results.Ok(await saldoServico.Recuperar(usuarioLogado.Recuperar()!.Id));
            
            [ExcludeFromCodeCoverage]
            Task AplicarConfiguracoes()
            {
                var acrescimoLatencia = Environment.GetEnvironmentVariable(nameof(ConfiguracaoModelo.AcrescimoDeLatencia), EnvironmentVariableTarget.Process);
                var provocarErro = Environment.GetEnvironmentVariable(nameof(ConfiguracaoModelo.ProvocarErro),EnvironmentVariableTarget.Process);

                logger.LogInformation($"situação da configuração: -> ProvocarErro: {provocarErro}");
                logger.LogInformation($"situação da configuração: -> AcrescimoDeLatencia: {acrescimoLatencia}");
                
                if (string.Equals(acrescimoLatencia,"true", StringComparison.OrdinalIgnoreCase))
                {
                    var penaldiade = 500;
                    logger.LogInformation($"latencia penalisada em {penaldiade}ms");
                    Thread.Sleep(penaldiade);
                }
                
                if (string.Equals(provocarErro,"true", StringComparison.OrdinalIgnoreCase))
                {
                    logger.LogError($"Erro intencional provacado pela configuração -> ProvocarErro: {provocarErro}");
                    throw new Exception("Erro interno no servidor");
                }

                return Task.CompletedTask;
            }
        })
    .WithName("Saldos")
    .WithOpenApi()
    .RequireAuthorization();

app.MapPost("/configuracoes", (ConfiguracaoModelo configuracaoModelo) =>
    {
        Environment.SetEnvironmentVariable(nameof(ConfiguracaoModelo.AcrescimoDeLatencia),
            (configuracaoModelo?.AcrescimoDeLatencia == true).ToString(), EnvironmentVariableTarget.Process);

        Environment.SetEnvironmentVariable(nameof(ConfiguracaoModelo.ProvocarErro),
            (configuracaoModelo?.ProvocarErro == true).ToString(),EnvironmentVariableTarget.Process);
    })
    .WithName("Configuracoes")
    .WithOpenApi()
    .RequireAuthorization();


app.UseCors();

app.UseCorrelationId();

app.MapDefaultEndpointsProduduction();

app.Run();

public partial class Program
{
}