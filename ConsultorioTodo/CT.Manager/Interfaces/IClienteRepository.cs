using CT.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CT.Manager.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente> GetClienteByIdAsync(int id);
        Task<IEnumerable<Cliente>> GetClientesAsync();
        Task<Cliente> InsertClienteAsync(Cliente cliente);
        Task<Cliente> UpdateClienteByIdAsync(Cliente cliente);
        Task DeleteClienteByIdAsync(int id);
    }
}
