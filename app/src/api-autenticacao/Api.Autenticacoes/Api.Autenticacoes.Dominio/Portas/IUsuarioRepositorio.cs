using Api.Autenticacoes.Dominio.AutenticacaoAgregado.Entidades;
using Api.Autenticacoes.Dominio.AutenticacaoAgregado.ObjetosTransferencia;

namespace Api.Autenticacoes.Dominio.Portas;

public interface IUsuarioRepositorio
{
    Task<Usuario?> RecuperarUsuarioAsync(LoginDto login);
}