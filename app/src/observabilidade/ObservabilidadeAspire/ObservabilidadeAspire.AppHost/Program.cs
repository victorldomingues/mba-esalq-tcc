var builder = DistributedApplication.CreateBuilder(args);

var apiSaldos = builder.AddProject<Projects.Api_Saldos>("apiSaldos");
var apiMovimentacoes = builder.AddProject<Projects.Api_Movimentacoes>("apiMovimentacoes");
var apiAutenticacao = builder.AddProject<Projects.Api_Autenticacoes>("apiAutenticacao");
var apiCadastro = builder.AddProject<Projects.Api_Cadastro>("apiCadastro");

builder.AddNpmApp("angular", "../../../ui")
    .WithReference(apiSaldos)
    .WithReference(apiMovimentacoes)
    .WithReference(apiAutenticacao)
    .WithReference(apiCadastro)
    .WaitFor(apiSaldos)
    .WaitFor(apiMovimentacoes)
    .WaitFor(apiAutenticacao)
    .WaitFor(apiCadastro)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints();



builder.Build().Run();

