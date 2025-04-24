using Api.Cadastro.Dominio.ClienteAgregado.Entidades;
using Api.Cadastro.Dominio.ClienteAgregado.ObjetosValor;

namespace Api.Cadastro.Modelos;

public record CadastroClienteModelo
{
    public string Nome { get; set; }
    public string Senha { get; set; }
    public string ConfirmaSenha { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }

    public Usuario ParaUsuario()
    {
        return new Usuario(new Cpf(Cpf), new Senha(Senha), Nome, Email);
    }
}