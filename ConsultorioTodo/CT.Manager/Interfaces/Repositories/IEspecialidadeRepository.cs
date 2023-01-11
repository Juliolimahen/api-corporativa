using System.Threading.Tasks;

namespace CT.Manager.Interfaces.Repositories;

public interface IEspecialidadeRepository
{
    Task<bool> ExisteAsync(int id);
}
