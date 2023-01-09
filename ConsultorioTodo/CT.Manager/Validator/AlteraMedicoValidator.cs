using CT.Core.Shared.ModelsViews.Medico;
using CT.Manager.Interfaces;
using CT.Manager.Validator;
using FluentValidation;

namespace CT.WebApi.Configuration
{
    public class AlteraMedicoValidator : AbstractValidator<AlteraMedico>
    {
        public AlteraMedicoValidator(IEspecialidadeRepository repository)
        {
            RuleFor(p => p.Id).NotNull().NotEmpty().GreaterThan(0);
            Include(new NovoMedicoValidator(repository));
        }
    }
}
