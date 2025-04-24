using Api.Movimentacoes.Aplicacao;
using Api.Movimentacoes.Dominio.Portas;
using Api.Movimentacoes.Infra.Dados;
using Api.Movimentacoes.Modelos;
using Api.Movimentacoes.Validadores;
using Artefatos.Identidade;
using Artefatos.Identidade.Correlacoes;
using Artefatos.Notificacoes;
using IdentityModel.AspNetCore.OAuth2Introspection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AdicionarBancoDeDados(builder.Configuration);
builder.Services.AdicionarServicosAplicacao();
builder.Services.AdicionarNotificacoes();
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
            policy.WithOrigins("localhost:4200","localhost:80", "localhost")
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


//TODO: informar instrumentação do aspire no tcc
builder.Services.AddProblemDetails();
builder.AddServiceDefaults();

var app = builder.Build();

//TODO: ver um melhor lugar para esse comando 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/movimentacoes", async (IMovimentacaoServico movimentacaoServico,
        INotificacaoContexto notificacaoContexto, MovimentacaoModelo movimentacaoModelo,
        IUsuarioLogado usuarioLogado) =>
    {
        var validador = new MovimentacaoModeloValidador();
        var resultadoValidacao = validador.Validate(movimentacaoModelo);

        if (resultadoValidacao.IsValid)
        {
            var usuario = usuarioLogado.Recuperar()!;
            var movimentacao = movimentacaoModelo.ParaUsuario(usuario.Id);
            await movimentacaoServico.Movimentar(movimentacao);

            if (notificacaoContexto.ExisteNotificacao)
                return Results.UnprocessableEntity(new { Erros = notificacaoContexto.Notificacoes });

            return Results.Ok(movimentacaoModelo);
        }

        return Results.BadRequest(new
        {
            Erros = resultadoValidacao.Errors
                .Select(x => new
                    { Codigo = x.ErrorMessage, Mensagem = x.ErrorCode, Propriedade = x.PropertyName })
        });
    })
    .WithName("Movimentacao")
    .WithOpenApi()
    .RequireAuthorization();

app.MapGet("/movimentacoes", async (IMovimentacaoServico movimentacaoServico, IUsuarioLogado usuarioLogado) =>
    {
        var usuario = usuarioLogado.Recuperar()!;
        var movimentacoes = await movimentacaoServico.Listar(usuario.Id);
        return Results.Ok(movimentacoes);
    })
    .WithName("Listar Movimentacoes")
    .WithOpenApi()
    .RequireAuthorization();

app.UseCors();

app.UseCorrelationId();

// app.MapDefaultEndpoints();

app.Run();


public partial class Program
{
}