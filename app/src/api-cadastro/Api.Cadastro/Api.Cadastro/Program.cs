using Api.Cadastro.Aplicacao;
using Api.Cadastro.Data;
using Api.Cadastro.Dominio.Portas;
using Api.Cadastro.Modelos;
using Api.Cadastro.Validadores;
using Artefatos.Identidade;
using Artefatos.Identidade.Correlacoes;
using Artefatos.Notificacoes;
using IdentityModel.AspNetCore.OAuth2Introspection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

var stringConexaoRedis = builder.Configuration.GetConnectionString("Redis")!;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AdicionarBancoDeDados(builder.Configuration);
builder.Services.AdicionarCache(stringConexaoRedis);
builder.Services.AdicionarServicosAplicacao();
builder.Services.AdicionarNotificacoes();
builder.Services.AdicionarIdentidade();

//TODO: informar instrumentação do aspire no tcc
builder.Services.AddProblemDetails();
builder.AddServiceDefaults();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(OAuth2IntrospectionDefaults.AuthenticationScheme)
    .AddOAuth2Introspection(
        opcoes => { opcoes.IntrospectionEndpoint = builder.Configuration["IntrospectionEndpoint"]; });
builder.Services.AddHttpClient(OAuth2IntrospectionDefaults.BackChannelHttpClientName)
    .AddHttpMessageHandler<CorrelationIdMessageHandler>();

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


app.MapPost("/cadastros",
        async (ICadastroClienteServico cadastroClienteServico, INotificacaoContexto notificacaoContexto,
            CadastroClienteModelo cadastroCliente) =>
        {
            var validador = new CadastroClienteValidador();
            var resultadoValidacao = validador.Validate(cadastroCliente);

            if (resultadoValidacao.IsValid)
            {
                var usuario = cadastroCliente.ParaUsuario();
                await cadastroClienteServico.Cadastrar(usuario);

                if (notificacaoContexto.ExisteNotificacao)
                {
                    app.Logger.LogWarning(
                        $"UnprocessableEntity: Cadastro Invalido {string.Join(',', notificacaoContexto.Notificacoes.Select(x => x.Mensagem))}");
                    return Results.UnprocessableEntity(new { Erros = notificacaoContexto.Notificacoes });
                }

                return Results.Ok(cadastroCliente);
            }

            app.Logger.LogWarning(
                $"BadRequest: Cadastro Invalido {string.Join(',', resultadoValidacao.Errors.Select(x => x.ErrorMessage))}");
            return Results.BadRequest(new
            {
                Erros = resultadoValidacao.Errors
                    .Select(x => new { Codigo = x.ErrorMessage, Mensagem = x.ErrorCode, Propriedade = x.PropertyName })
            });
        })
    .WithName("Cadastro")
    .WithOpenApi();

app.MapGet("/perfis", async (IPerfilServico perfilServico, IUsuarioLogado usuarioLogado) =>
    {
        var usuario = usuarioLogado.Recuperar()!;

        var perfil = await perfilServico.Recuperar(usuario.Id);

        return Results.Ok(perfil);
    })
    .WithName("Perfil")
    .RequireAuthorization()
    .WithOpenApi();

app.UseCors();

app.UseCorrelationId();

// app.MapDefaultEndpoints();

app.Run();

public partial class Program
{
}