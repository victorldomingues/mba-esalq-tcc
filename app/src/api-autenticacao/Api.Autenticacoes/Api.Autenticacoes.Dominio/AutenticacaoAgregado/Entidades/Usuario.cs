using Api.Autenticacoes.Dominio.AutenticacaoAgregado.ObjetosDeValor;
using Artefatos.Seguranca;

namespace Api.Autenticacoes.Dominio.AutenticacaoAgregado.Entidades;

public class Usuario
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public string Senha { get; set; }
    
    public Situacao Situacao { get; set; }
    public bool SenhaValida(string senha)
        => Senha == Criptografia.CriptografarSHA256(senha);
}