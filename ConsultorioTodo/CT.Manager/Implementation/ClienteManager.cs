using AutoMapper;
using CT.Core.Domain;
using CT.Core.Shared.ModelsViews;
using CT.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CT.Manager.Implementation
{
    public class ClienteManager : IClienteManager
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteManager(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
            return await _clienteRepository.GetClientesAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            return await _clienteRepository.GetClienteByIdAsync(id);
        }

        public async Task<Cliente> InsertClienteAsync(NovoCliente novoCliente)
        {
            var cliente = _mapper.Map<Cliente>(novoCliente);
            return await _clienteRepository.InsertClienteAsync(cliente);
        }

        public async Task<Cliente> UpdateClienteByIdAsync(AlteraCliente AlteraCliente)
        {
            var cliente = _mapper.Map<Cliente>(AlteraCliente);
            return await _clienteRepository.UpdateClienteByIdAsync(cliente);
        }

        public async Task DeleteClienteByIdAsync(int id)
        {
            await _clienteRepository.DeleteClienteByIdAsync(id);
        }
    }
}
