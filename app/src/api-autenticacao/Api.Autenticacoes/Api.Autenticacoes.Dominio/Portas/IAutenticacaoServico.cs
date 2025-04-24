using System.Security.Claims;
using Api.Autenticacoes.Dominio.AutenticacaoAgregado.Entidades;
using Api.Autenticacoes.Dominio.AutenticacaoAgregado.ObjetosTransferencia;

namespace Api.Autenticacoes.Dominio.Portas;

public interface IAutenticacaoServico
{
    Task<Sessao?> Logar(LoginDto login);
    ClaimsPrincipal? ValidarToken(string token);
}