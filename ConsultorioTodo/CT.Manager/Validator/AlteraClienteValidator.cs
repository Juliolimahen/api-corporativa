using CT.Core.Shared.ModelsViews;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.Manager.Validator
{
    public class AlteraClienteValidator : AbstractValidator<AlteraCliente>
    {
        public AlteraClienteValidator()
        {
            RuleFor(p => p.Id).NotNull().NotEmpty().GreaterThan(0);
            Include(new NovoClienteValidator());
        }
    }
}
