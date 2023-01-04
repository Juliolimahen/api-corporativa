using CT.Core.Domain;
using CT.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CT.Manager.Interfaces;

namespace CT.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
            return await _context.Clientes.AsNoTracking().ToListAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<Cliente> InsertClienteAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> UpdateClienteByIdAsync(Cliente cliente)
        {
            var clienteConsultado = await _context.Clientes.FindAsync(cliente.Id);
            if (clienteConsultado == null)
            {
                return null;
            }

            //Atribuindo valores
            _context.Entry(clienteConsultado).CurrentValues.SetValues(cliente);
            await _context.SaveChangesAsync();
            return clienteConsultado;
        }

        public async Task DeleteClienteByIdAsync(int id)
        {
            var clienteConsultado = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(clienteConsultado);
            await _context.SaveChangesAsync();
        }
    }
}
