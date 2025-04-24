using Api.Cadastro.Dominio.ClienteAgregado.Entidades;

namespace Api.Cadastro.Dominio.ClienteAgregado;

public class PerfilDto
{
    public PerfilDto()
    {
        
    }
    public PerfilDto(Usuario usuario)
    {
        Id = usuario.Id;
        Nome = usuario.Nome;
        Email = usuario.Email;
        Cpf = usuario.Cpf.Numero;
    }
    
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
}