using CT.Core.Shared.ModelsViews;
using CT.Core.Shared.ModelsViews.Cliente;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CT.Manager.Interfaces.Managers;

public interface IClienteManager
{
    Task<ClienteView> GetClienteAsync(int id);

    Task<IEnumerable<ClienteView>> GetClientesAsync();

    Task<ClienteView> InsertClienteAsync(NovoCliente cliente);

    Task<ClienteView> UpdateClienteAsync(AlteraCliente cliente);
    Task<ClienteView> DeleteClienteAsync(int id);
}
