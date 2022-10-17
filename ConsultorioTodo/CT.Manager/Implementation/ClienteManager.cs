using CT.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.Manager.Implementation
{
    public class ClienteManager:IClienteManager
    {
       private readonly IClienteRepository _clienteRepository;

        public ClienteManager(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
    }
}
