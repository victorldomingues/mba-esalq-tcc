using Api.Cadastro.Dominio.ClienteAgregado.Entidades;
using Api.Cadastro.Dominio.ClienteAgregado.ObjetosValor;

namespace Api.Cadastro.Dominio.Portas;

public interface IUsuarioRepositorio
{
    public Task Cadastrar(Usuario usuario);
    public Task<Usuario?> Recuperar(string cpf, string email);
    public Task<Usuario?> Recuperar(Guid id);
}