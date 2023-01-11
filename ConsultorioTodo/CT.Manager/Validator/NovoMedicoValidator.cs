
using CT.Core.Shared.ModelsViews.Medico;
using CT.Manager.Interfaces.Repositories;
using FluentValidation;

namespace CT.Manager.Validator;

public class NovoMedicoValidator : AbstractValidator<NovoMedico>
{
    public NovoMedicoValidator(IEspecialidadeRepository repository)
    {
        RuleFor(m => m.Nome).NotNull().NotEmpty().MaximumLength(200);

        RuleFor(m => m.Crm).NotNull().NotEmpty().GreaterThan(0);

        RuleForEach(m => m.Especialidades).SetValidator(new ReferenciaEspecialidadeValidator(repository));
    }
}