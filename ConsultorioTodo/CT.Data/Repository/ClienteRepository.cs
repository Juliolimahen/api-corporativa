using CT.Core.Domain;
using CT.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CT.Manager.Interfaces.Repositories;

namespace CT.Data.Repository;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> GetClientesAsync()
    {
        return await _context.Clientes
            .Include(p => p.Endereco)
            .Include(p => p.Telefones)
            .AsNoTracking().ToListAsync();
    }

    public async Task<Cliente> GetClienteAsync(int id)
    {
        return await _context.Clientes
            .Include(p => p.Endereco)
            .Include(p => p.Telefones)
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Cliente> InsertClienteAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
    {
        var clienteConsultado = await _context.Clientes
                                             .Include(p => p.Endereco)
                                             .Include(p => p.Telefones)
                                             .FirstOrDefaultAsync(p => p.Id == cliente.Id);
        if (clienteConsultado == null)
        {
            return null;
        }
        _context.Entry(clienteConsultado).CurrentValues.SetValues(cliente);
        clienteConsultado.Endereco = cliente.Endereco;
        UpdateClienteTelefones(cliente, clienteConsultado);
        await _context.SaveChangesAsync();
        return clienteConsultado;
    }

    private static void UpdateClienteTelefones(Cliente cliente, Cliente clienteConsultado)
    {
        clienteConsultado.Telefones.Clear();
        foreach (var telefone in cliente.Telefones)
        {
            clienteConsultado.Telefones.Add(telefone);
        }
    }

    public async Task<Cliente> DeleteClienteAsync(int id)
    {
        var clienteConsultado = await _context.Clientes.FindAsync(id);
        if (clienteConsultado == null)
        {
            return null;
        }
        var clienteRemovido = _context.Clientes.Remove(clienteConsultado);
        await _context.SaveChangesAsync();
        return clienteRemovido.Entity;
    }
}
