using CT.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CT.Manager.Interfaces
{
    public interface IMedicoRepository
    {
        Task<IEnumerable<Medico>> GetMedicosAsync();

        Task<Medico> GetMedicoAsync(int id);

        Task<Medico> InsertMedicoAsync(Medico medico);

        Task<Medico> UpdateMedicoAsync(Medico medico);

        Task<Medico> DeleteMedicoAsync(int id);
    }
}
