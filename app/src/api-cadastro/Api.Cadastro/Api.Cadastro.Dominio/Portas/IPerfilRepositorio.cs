using Api.Cadastro.Dominio.ClienteAgregado;

namespace Api.Cadastro.Dominio.Portas;

public interface IPerfilRepositorio
{
    Task<PerfilDto> Recuperar(Guid id);
}