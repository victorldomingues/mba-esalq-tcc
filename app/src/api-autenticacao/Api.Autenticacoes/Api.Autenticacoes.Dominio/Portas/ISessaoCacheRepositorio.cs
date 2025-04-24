using Api.Autenticacoes.Dominio.AutenticacaoAgregado.Entidades;
using Api.Autenticacoes.Dominio.AutenticacaoAgregado.ObjetosTransferencia;

namespace Api.Autenticacoes.Dominio.Portas;

public interface ISessaoCacheRepositorio
{
    Task CachearSessaoAsync(Sessao sessao);
    Task<Sessao?> RecuperarSessaoAsync(string chave);
}