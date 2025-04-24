using Artefatos.Seguranca;

namespace Api.Cadastro.Dominio.ClienteAgregado.ObjetosValor;

public class Senha
{
    private string _senha;

    protected Senha() { }

    public Senha(string senha)
    {
        _senha = senha;
        CriptografarSenha();
    }

    private void CriptografarSenha()
    {
        Encriptada = Criptografia.CriptografarSHA256(_senha);
    }

    public string Encriptada { get; protected set; }
}