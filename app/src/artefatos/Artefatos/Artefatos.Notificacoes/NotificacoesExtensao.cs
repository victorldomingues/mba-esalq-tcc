using Microsoft.Extensions.DependencyInjection;

namespace Artefatos.Notificacoes;

public static class NotificacoesExtensao
{
    public static IServiceCollection AdicionarNotificacoes(this IServiceCollection services)
    {
        services.AddScoped<INotificacaoContexto, NotificacaoContexto>();
        return services;
    }
}