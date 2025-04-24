using Api.Cadastro.Dominio.ClienteAgregado.ObjetosValor;
using Api.Cadastro.Modelos;
using FluentValidation;

namespace Api.Cadastro.Validadores;

public class CadastroClienteValidador : AbstractValidator<CadastroClienteModelo>
{
    public CadastroClienteValidador()
    {
        RuleFor(cadsatroCliente => cadsatroCliente.Nome).NotNull()
            .WithMessage("Nome é obrigatório").WithErrorCode("ERRO_CLIENTE_0001");

        RuleFor(cadastroCliente => cadastroCliente.Cpf).NotNull()
            .WithMessage("CPF é obrigatório").WithErrorCode("ERRO_CLIENTE_0002");

        RuleFor(cadastroCliente => cadastroCliente.Senha).NotNull()
            .WithMessage("Senha é obrigatório").WithErrorCode("ERRO_CLIENTE_003");

        RuleFor(cadastroCliente => cadastroCliente.ConfirmaSenha).NotNull()
            .WithMessage("Confirma senha é obrigatório").WithErrorCode("ERRO_CLIENTE_004");
        
        RuleFor(cadastroCliente => cadastroCliente.Email).NotNull()
            .WithMessage("E-mail é obrigatório").WithErrorCode("ERRO_CLIENTE_0005");


        RuleFor(cadastroCliente => cadastroCliente.Email).EmailAddress()
            .WithMessage("E-mail é invalido").WithErrorCode("ERRO_CLIENTE_0006");

        RuleFor(cadastroCliente => cadastroCliente.ConfirmaSenha).Equal(castroCliente => castroCliente.Senha)
            .WithMessage("Confirma senha deve ser igual a confirma senha")
            .WithErrorCode("ERRO_CLIENTE_0007");

        RuleFor(cadastroCliente => cadastroCliente.Cpf).Must(cpf => new Cpf(cpf).Valido)
            .WithMessage("CPF é invalido").WithErrorCode("ERRO_CLIENTE_0008");
    }
}