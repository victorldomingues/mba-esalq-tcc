using Api.Cadastro.Dominio.ClienteAgregado;

namespace Api.Cadastro.Dominio.Portas;

public interface IPerfilServico
{
    Task<PerfilDto> Recuperar(Guid id);
}