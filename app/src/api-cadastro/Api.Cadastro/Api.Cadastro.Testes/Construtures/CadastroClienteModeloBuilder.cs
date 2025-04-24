using Api.Cadastro.Dominio.ClienteAgregado.Entidades;
using Api.Cadastro.Dominio.ClienteAgregado.ObjetosValor;
using Api.Cadastro.Modelos;

namespace Api.Cadastro.Testes.Construtures;

public class CadastroClienteModeloBuilder
{
    private CadastroClienteModelo _cliente;

    public CadastroClienteModeloBuilder()
    {
        Resetar();
    }

    public CadastroClienteModeloBuilder Resetar()
    {
        _cliente = new CadastroClienteModelo()
        {
            Cpf = "928.532.020-11",
            Nome = "nome",
            Email = "email@email.com",
            Senha = "senha",
            ConfirmaSenha = "senha"
        };
        return this;
    }
    
    public static CadastroClienteModeloBuilder Construir()
    {
        return new CadastroClienteModeloBuilder().Resetar();
    }

    public CadastroClienteModelo Instancia => _cliente;
    
}