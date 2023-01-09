using CT.Core.Shared.ModelsViews;
using CT.Manager.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CT.Manager.Validator
{
    public class ReferenciaEspecialidadeValidator : AbstractValidator<ReferenciaEspecialidade>
    {
        private readonly IEspecialidadeRepository _repository;

        public ReferenciaEspecialidadeValidator(IEspecialidadeRepository repository)
        {
            _repository = repository;
            RuleFor(p => p.Id).NotEmpty().NotNull().GreaterThan(0)
                .MustAsync(async (id, _) => await ExisteNaBase(id)).WithMessage("Especialidade não cadastrada");
        }
        private async Task<bool> ExisteNaBase(int id)
        {
            return await _repository.ExisteAsync(id);
        }
    }
}
