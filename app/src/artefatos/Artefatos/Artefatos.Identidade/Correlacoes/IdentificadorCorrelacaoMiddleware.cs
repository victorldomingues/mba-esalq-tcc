using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Artefatos.Identidade.Correlacoes;

public class IdentificadorCorrelacaoMiddleware
{
    private readonly RequestDelegate _next;

    public IdentificadorCorrelacaoMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.Headers.TryGetValue("Correlation-Id", out var correlationIds);
        var identificadorCorrelacao = correlationIds.FirstOrDefault() ?? Guid.NewGuid().ToString();
        using(LogContext.PushProperty("CorrelationId", identificadorCorrelacao))
        {
            await _next(context);
        }
    }
}

public static class IdentificadorCorrelacaoExtensoes
{
    public static IApplicationBuilder UseCorrelationId(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<IdentificadorCorrelacaoMiddleware>();
    }
}
