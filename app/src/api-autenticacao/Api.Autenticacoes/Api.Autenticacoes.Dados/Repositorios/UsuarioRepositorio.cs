using Api.Autenticacoes.Dados.Contextos;
using Api.Autenticacoes.Dominio.AutenticacaoAgregado.Entidades;
using Api.Autenticacoes.Dominio.AutenticacaoAgregado.ObjetosDeValor;
using Api.Autenticacoes.Dominio.AutenticacaoAgregado.ObjetosTransferencia;
using Api.Autenticacoes.Dominio.Portas;
using Microsoft.EntityFrameworkCore;

namespace Api.Autenticacoes.Dados.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly PostgresDbContexto _contexto;

    public UsuarioRepositorio(PostgresDbContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<Usuario?> RecuperarUsuarioAsync(LoginDto login)
    {
        return await _contexto.Usuarios.AsNoTracking()
            .FirstOrDefaultAsync(usuario => usuario.Cpf == login.Cpf && usuario.Situacao == Situacao.Ativo);
    }
}