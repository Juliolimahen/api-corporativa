
using CT.Core.Shared.ModelsViews;
using CT.Manager.Interfaces.Repositories;
using FluentValidation;
using System.Threading.Tasks;

namespace CT.Manager.Validator;

public class ReferenciaEspecialidadeValidator : AbstractValidator<ReferenciaEspecialidade>
{
    private readonly IEspecialidadeRepository repository;

    public ReferenciaEspecialidadeValidator(IEspecialidadeRepository repository)
    {
        this.repository = repository;
        RuleFor(p => p.Id).NotEmpty().NotNull().GreaterThan(0)
            .MustAsync(async (id, _) => await ExisteNaBase(id)).WithMessage("Especialidade não cadastrada");
    }

    private async Task<bool> ExisteNaBase(int id)
    {
        return await repository.ExisteAsync(id);
    }
}