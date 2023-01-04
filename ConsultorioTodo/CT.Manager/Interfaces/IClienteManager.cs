using CT.Core.Domain;
using CT.Core.Shared.ModelsViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CT.Manager.Interfaces
{
    public interface IClienteManager
    {
        Task<Cliente> GetClienteByIdAsync(int id);
        Task<IEnumerable<Cliente>> GetClientesAsync();
        Task<Cliente> InsertClienteAsync(NovoCliente novoCliente);
        Task<Cliente> UpdateClienteByIdAsync(AlteraCliente AlteraCliente);
        Task DeleteClienteByIdAsync(int id);
    }
}
