using CT.Core.Shared.ModelsViews;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.Manager.Validator
{
    public class NovoEnderecoValidator: AbstractValidator<NovoEndereco>
    {
        public NovoEnderecoValidator()
        {
            RuleFor(e => e.Cep).NotEmpty().NotNull();
            RuleFor(e => e.Estado).NotNull();
            RuleFor(e => e.Cidade).NotEmpty().NotNull().MaximumLength(200);
            RuleFor(e => e.Logradouro).NotEmpty().NotNull().MaximumLength(200);
            RuleFor(e => e.Numero).NotEmpty().NotNull().MaximumLength(10);
            RuleFor(e => e.Complemento).MaximumLength(200);
        }
    }
}
