using Api.Autenticacoes.Dominio.AutenticacaoAgregado.ObjetosTransferencia;

namespace Api.Autenticacoes.Modelos;

public record LoginModelo
{
    public string Cpf { get; set; }
    public string Senha { get; set; }

    public LoginDto ParaLoginDto()
    {
        return new LoginDto()
        {
            Cpf = Cpf,
            Senha = Senha
        };
    }
}