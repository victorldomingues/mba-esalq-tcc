using Api.Cadastro.Dominio.ClienteAgregado.Entidades;
using Api.Cadastro.Dominio.ClienteAgregado.ObjetosValor;

namespace Api.Cadastro.Testes.Construtures;

public class UsuarioBuilder
{
    private Usuario _usuario;

    public UsuarioBuilder()
    {
        Resetar();
    }

    public UsuarioBuilder Resetar()
    {
        _usuario = new Usuario(new Cpf("928.532.020-11"), new Senha("senha"), "nome", "email@email.com");
        return this;
    }
    
    public static UsuarioBuilder Construir()
    {
        return new UsuarioBuilder();
    }

    public UsuarioBuilder ComCpf(string cpf)
    {
        _usuario.Cpf = new Cpf(cpf);
        return this;
    }
    
    public Usuario Instancia => _usuario;
}