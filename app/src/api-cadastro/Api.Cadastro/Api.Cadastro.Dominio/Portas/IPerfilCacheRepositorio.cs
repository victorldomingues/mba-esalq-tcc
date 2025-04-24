using Api.Cadastro.Dominio.ClienteAgregado;

namespace Api.Cadastro.Dominio.Portas;

public interface IPerfilCacheRepositorio
{
    Task<PerfilDto?> Recuperar(string chave);
    Task Cachear(string chave, PerfilDto perfilDto);
}