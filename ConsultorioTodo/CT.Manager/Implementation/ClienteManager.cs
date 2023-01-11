using AutoMapper;
using CT.Core.Domain;
using CT.Core.Shared.ModelsViews;
using CT.Core.Shared.ModelsViews.Cliente;
using CT.Manager.Interfaces.Managers;
using CT.Manager.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CT.Manager.Implementation;

public class ClienteManager : IClienteManager
{

    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ClienteManager> _logger;

    public ClienteManager(IClienteRepository clienteRepository, IMapper mapper, ILogger<ClienteManager> logger)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<ClienteView>> GetClientesAsync()
    {
        var clientes = await _clienteRepository.GetClientesAsync();
        return _mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteView>>(clientes);
    }

    public async Task<ClienteView> GetClienteAsync(int id)
    {
        var cliente = await _clienteRepository.GetClienteAsync(id);
        return _mapper.Map<ClienteView>(cliente);
    }

    public async Task<ClienteView> DeleteClienteAsync(int id)
    {
        var cliente = await _clienteRepository.DeleteClienteAsync(id);
        return _mapper.Map<ClienteView>(cliente);
    }

    public async Task<ClienteView> InsertClienteAsync(NovoCliente novoCliente)
    {
        _logger.LogInformation("Chamada de negócio para inserir um cliente.");
        var cliente = _mapper.Map<Cliente>(novoCliente);
        cliente = await _clienteRepository.InsertClienteAsync(cliente);
        return _mapper.Map<ClienteView>(cliente);
    }

    public async Task<ClienteView> UpdateClienteAsync(AlteraCliente alteraCliente)
    {
        var cliente = _mapper.Map<Cliente>(alteraCliente);
        cliente = await _clienteRepository.UpdateClienteAsync(cliente);
        return _mapper.Map<ClienteView>(cliente);
    }
}
