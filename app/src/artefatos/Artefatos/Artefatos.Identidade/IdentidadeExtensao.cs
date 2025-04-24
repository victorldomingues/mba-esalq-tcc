using Artefatos.Identidade.Correlacoes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Artefatos.Identidade;

public static class IdentidadeExtensao
{
    public static IServiceCollection AdicionarIdentidade(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUsuarioLogado, UsuarioLogado>();
        services.TryAddScoped<CorrelationIdMessageHandler>();
        return services;
    }
}