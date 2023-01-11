using CT.Core.Shared.ModelsViews.Medico;
using CT.Manager.Interfaces.Repositories;
using CT.Manager.Validator;
using FluentValidation;

namespace CT.Manager.Validator;

public class AlteraMedicoValidator : AbstractValidator<AlteraMedico>
{
    public AlteraMedicoValidator(IEspecialidadeRepository repository)
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().GreaterThan(0);
        Include(new NovoMedicoValidator(repository));
    }
}