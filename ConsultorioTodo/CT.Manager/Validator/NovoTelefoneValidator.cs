using CT.Core.Shared.ModelsViews;
using FluentValidation;

namespace CT.Manager.Validator;

public class NovoTelefoneValidator: AbstractValidator<NovoTelefone>
{
    public NovoTelefoneValidator()
    {
        RuleFor(p => p.Numero).Matches("[1-9][0-9]{9}").WithMessage("O telefone tem que ter o formato [2-9][0-9]{10}");
    }
}
