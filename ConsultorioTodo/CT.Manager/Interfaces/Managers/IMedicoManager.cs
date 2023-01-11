using CT.Core.Shared.ModelsViews;
using CT.Core.Shared.ModelsViews.Medico;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CT.Manager.Managers;

public interface IMedicoManager
{
    Task DeleteMedicoAsync(int id);

    Task<MedicoView> GetMedicoAsync(int id);

    Task<IEnumerable<MedicoView>> GetMedicosAsync();

    Task<MedicoView> InsertMedicoAsync(NovoMedico novoMedico);

    Task<MedicoView> UpdateMedicoAsync(AlteraMedico alteraMedico);
}