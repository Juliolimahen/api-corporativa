
using CT.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CT.Manager.Interfaces.Repositories;

public interface IClienteRepository
{
    Task<Cliente> DeleteClienteAsync(int id);

    Task<Cliente> GetClienteAsync(int id);

    Task<IEnumerable<Cliente>> GetClientesAsync();

    Task<Cliente> InsertClienteAsync(Cliente cliente);

    Task<Cliente> UpdateClienteAsync(Cliente cliente);
}
