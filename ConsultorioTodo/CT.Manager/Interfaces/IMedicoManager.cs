using CT.Core.Domain;
using CT.Core.Shared.ModelsViews;
using CT.Core.Shared.ModelsViews.Medico;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CT.Manager.Implementation
{
    public interface IMedicoManager
    {
        Task<MedicoView> GetMedicoAsync(int id);

        Task<IEnumerable<MedicoView>> GetMedicosAsync();

        Task<MedicoView> InsertMedicoAsync(NovoMedico novoMedico);

        Task<MedicoView> UpdateMedicoAsync(AlteraMedico alteraMedico);
        Task DeleteMedicoAsync(int id);
    }
}
