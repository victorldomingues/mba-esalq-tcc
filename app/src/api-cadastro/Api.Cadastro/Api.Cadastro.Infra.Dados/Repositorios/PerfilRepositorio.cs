using Api.Cadastro.Data.Contextos;
using Api.Cadastro.Dominio.ClienteAgregado;
using Api.Cadastro.Dominio.Portas;
using Microsoft.EntityFrameworkCore;

namespace Api.Cadastro.Data.Repositorios;

public class PerfilRepositorio : IPerfilRepositorio
{
    private readonly PostgresDbContext _contexto;
    
    public PerfilRepositorio(PostgresDbContext contexto)
    {
        _contexto = contexto;
    }
    
    public async Task<PerfilDto> Recuperar(Guid id)
    {
        var usuario = await _contexto.Usuarios.FirstAsync(x=>x.Id == id);
        return new PerfilDto(usuario);
    }
}