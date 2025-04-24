using Api.Cadastro.Dominio.ClienteAgregado.ObjetosValor;

namespace Api.Cadastro.Dominio.ClienteAgregado.Entidades;

public class Usuario
{
    protected Usuario(){}
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public Senha Senha { get; protected set; }
    public string Email { get; protected set; }
    public Cpf Cpf { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime? AtualizadoEm { get; set; }
    public DateTime? DeletadoEm { get; set; }
    public Situacao Situacao { get; set; }

    public Usuario (Cpf cpf, Senha senha, string nome, string email)
    {
        Id = Guid.NewGuid();
        CriadoEm = DateTime.Now;
        Cpf = cpf ;
        Nome = nome;
        Email = email;
        Senha = senha;
        Situacao = Situacao.Ativo;
    }
}