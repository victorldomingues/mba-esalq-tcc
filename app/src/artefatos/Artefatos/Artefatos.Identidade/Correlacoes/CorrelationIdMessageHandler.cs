using Microsoft.AspNetCore.Http;

namespace Artefatos.Identidade.Correlacoes;

public class CorrelationIdMessageHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CorrelationIdMessageHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected async override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var correlationId = _httpContextAccessor.HttpContext?.Request.Headers["X-Correlation-ID"].FirstOrDefault();
        
        if(correlationId != null)
            request.Headers.Add("Correlation-ID", _httpContextAccessor.HttpContext!.Request.Headers["Correlation-ID"].FirstOrDefault());
        
        var response = await base.SendAsync(request, cancellationToken);

        return response;
    }
}