
using CT.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CT.Manager.Interfaces.Repositories;

public interface IMedicoRepository
{
    Task<IEnumerable<Medico>> GetMedicosAsync();

    Task<Medico> GetMedicoAsync(int id);

    Task<Medico> InsertMedicoAsync(Medico medico);

    Task<Medico> UpdateMedicoAsync(Medico medico);

    Task<Medico> DeleteMedicoAsync(int id);
}