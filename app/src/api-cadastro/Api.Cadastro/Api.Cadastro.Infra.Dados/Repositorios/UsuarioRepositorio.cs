using Api.Cadastro.Data.Contextos;
using Api.Cadastro.Dominio.ClienteAgregado.Entidades;
using Api.Cadastro.Dominio.ClienteAgregado.ObjetosValor;
using Api.Cadastro.Dominio.Portas;
using Microsoft.EntityFrameworkCore;

namespace Api.Cadastro.Data.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly PostgresDbContext _context;

    public UsuarioRepositorio(PostgresDbContext context)
    {
        _context = context;
    }

    public async Task Cadastrar(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<Usuario?> Recuperar(string cpf, string email)
    {
        var usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(usr => usr.Cpf.Numero == cpf || usr.Email == email);
        return usuario;
    }

    public async Task<Usuario?> Recuperar(Guid id)
    {
        var usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(usr => usr.Id == id);
        return usuario;
    }
}