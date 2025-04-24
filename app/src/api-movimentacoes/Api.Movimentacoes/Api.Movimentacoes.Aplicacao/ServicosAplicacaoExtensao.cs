using Api.Movimentacoes.Aplicacao.Servicos;
using Api.Movimentacoes.Dominio.Portas;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Movimentacoes.Aplicacao;

public static class ServicosAplicacaoExtensao
{
    public static IServiceCollection AdicionarServicosAplicacao(this IServiceCollection services)
    {
        services.AddScoped<IMovimentacaoServico, MovimentacaoServico>();
        return services;
    }
}