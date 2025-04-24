using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Artefatos.Identidade;

internal class UsuarioLogado : IUsuarioLogado
{
    private readonly IHttpContextAccessor _contexto;

    public UsuarioLogado(IHttpContextAccessor contexto)
    {
        _contexto = contexto;
    }

    public UsuarioLogadoDto? Recuperar()
    {
        var contexto = _contexto.HttpContext;
        if (contexto.User?.Identity?.IsAuthenticated != true) return new UsuarioLogadoDto();
        var claimsIdentity = (contexto.User?.Identity as ClaimsIdentity)!;
        var id = RecuperarValorClaim(claimsIdentity, "sub");
        var email = RecuperarValorClaim(claimsIdentity, "email");
        return new UsuarioLogadoDto(Guid.Parse(id), email, true);
    }

    private static string RecuperarValorClaim(ClaimsIdentity claimsIdentity, string claimType)
    {
        var claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == claimType);
        return claim?.Value!;
    }
}