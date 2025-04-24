using Api.Movimentacoes.Dominio.MovimentacaoAgregado.ObjetosValor;
using Api.Movimentacoes.Modelos;
using FluentValidation;

namespace Api.Movimentacoes.Validadores;

public class MovimentacaoModeloValidador : AbstractValidator<MovimentacaoModelo>
{
    public MovimentacaoModeloValidador()
    {
        RuleFor(movimentacaoModelo => movimentacaoModelo.Valor).NotNull().GreaterThan(0)
            .WithMessage($"{nameof(MovimentacaoModelo.Valor)} é obrigatório").WithErrorCode("ERRO_MOVIMENTACAO_0001");

        RuleFor(movimentacaoModelo => movimentacaoModelo.Destinatario).NotNull().NotEmpty()
            .WithMessage($"{nameof(MovimentacaoModelo.Destinatario)} é obrigatório")
            .WithErrorCode("ERRO_MOVIMENTACAO_0002");
        
        RuleFor(movimentacaoModelo => movimentacaoModelo.Tipo).NotNull().NotEqual(default(TipoMovimentacao))
            .WithMessage($"{nameof(MovimentacaoModelo.Tipo)} é obrigatório").WithErrorCode("ERRO_MOVIMENTACAO_0003");
        
        RuleFor(movimentacaoModelo => movimentacaoModelo.Forma).NotNull().NotEqual(default(FormaMovimentacao))
            .WithMessage($"{nameof(MovimentacaoModelo.Forma)} é obrigatória").WithErrorCode("ERRO_MOVIMENTACAO_0004");

        RuleFor(movimentacaoModelo => movimentacaoModelo.Forma).Must((movimentacaoModelo,forma) =>
            {
                switch (forma)
                {
                    case FormaMovimentacao.Doc:
                        return ValidaDadosBanco(movimentacaoModelo);
                    case FormaMovimentacao.Ted:
                        return ValidaDadosBanco(movimentacaoModelo);
                    default:
                        return true;
                }
            })
            .WithMessage($"{nameof(MovimentacaoModelo.Banco)} {nameof(MovimentacaoModelo.Agencia)} {nameof(MovimentacaoModelo.Conta)} {nameof(MovimentacaoModelo.Dac)} é obrigatório")
            .WithErrorCode("ERRO_MOVIMENTACAO_0002");
    }

    private bool ValidaDadosBanco(MovimentacaoModelo movimentacaoModelo)
        => !(string.IsNullOrEmpty(movimentacaoModelo.Banco) || string.IsNullOrEmpty(movimentacaoModelo.Agencia)
            || string.IsNullOrEmpty(movimentacaoModelo.Conta) || movimentacaoModelo.Dac == null);
    
}