using Api.Saldos.Aplicacao.Servicos;
using Api.Saldos.Dominio.Portas;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Saldos.Aplicacao;

public static class ServicosAplicacaoExtensao
{
    public static IServiceCollection AdicionarServiceosAplicacao(this IServiceCollection services)
    {
        services.AddScoped<ISaldoServico, SaldoServico>();
        return services;
    }
}