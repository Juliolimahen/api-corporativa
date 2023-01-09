using CT.Core.Shared.ModelsViews.Medico;
using CT.Manager.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.Manager.Validator
{
    public class NovoMedicoValidator : AbstractValidator<NovoMedico>
    {
        public NovoMedicoValidator(IEspecialidadeRepository repository)
        {
            RuleFor(p => p.Nome).NotNull().NotEmpty().MaximumLength(200);

            RuleFor(p => p.Crm).NotNull().NotEmpty().GreaterThan(0);

            RuleForEach(p => p.Especialidades).SetValidator(new ReferenciaEspecialidadeValidator(repository));
        }
    }
}
