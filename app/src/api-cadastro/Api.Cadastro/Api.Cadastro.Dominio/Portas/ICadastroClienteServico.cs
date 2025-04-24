using Api.Cadastro.Dominio.ClienteAgregado.Entidades;

namespace Api.Cadastro.Dominio.Portas;

public interface ICadastroClienteServico
{
    public Task Cadastrar(Usuario usuario);
}