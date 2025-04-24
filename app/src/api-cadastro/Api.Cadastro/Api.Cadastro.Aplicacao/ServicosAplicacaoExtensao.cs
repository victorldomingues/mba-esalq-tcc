using Api.Cadastro.Aplicacao.Servicos;
using Api.Cadastro.Dominio.Portas;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Cadastro.Aplicacao;

public static class ServicosAplicacaoExtensao
{
    public static IServiceCollection AdicionarServicosAplicacao(this IServiceCollection services)
    {
        services.AddScoped<ICadastroClienteServico, CadastroClienteServico>();
        services.AddScoped<IPerfilServico, PerfilServico>();
        return services;
    }
}