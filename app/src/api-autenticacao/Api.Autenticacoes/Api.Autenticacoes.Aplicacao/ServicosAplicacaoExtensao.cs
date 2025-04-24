using Api.Autenticacoes.Aplicacao.Modelos;
using Api.Autenticacoes.Aplicacao.Servicos;
using Api.Autenticacoes.Dominio.Portas;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Autenticacoes.Aplicacao;

public  static class ServicosAplicacaoExtensao
{
    public static IServiceCollection AdicionarServicosAplicacao(this IServiceCollection servicos, OpcoesDeSeguranca opcoesDeSeguranca)
    {
        servicos.AddSingleton(opcoesDeSeguranca);
        servicos.AddScoped<IAutenticacaoServico, AutenticacaoServico>();
        return servicos;
    }
}