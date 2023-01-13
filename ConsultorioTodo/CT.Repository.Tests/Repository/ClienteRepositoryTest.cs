using CT.Core.Domain;
using CT.Data.Context;
using CT.Data.Repository;
using CT.FakeData.ClienteData;
using CT.FakeData.TelefoneData;
using CT.Manager.Interfaces.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CT.Repository.Tests.Repository
{
    public class ClienteRepositoryTest : IDisposable
    {
        private readonly IClienteRepository _repository;
        private readonly AppDbContext _context;
        private readonly Cliente _cliente;
        private ClienteFaker _clienteFaker;

        public ClienteRepositoryTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseInMemoryDatabase("Db_Test");

            _context = new AppDbContext(optionsBuilder.Options);
            _repository = new ClienteRepository(_context);

            _clienteFaker = new ClienteFaker();
            _cliente = _clienteFaker.Generate();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        private async Task<List<Cliente>> InsereRegistros()
        {
            var clientes = _clienteFaker.Generate(100);
            foreach (var item in clientes)
            {
                item.Id = 0;
                await _context.Clientes.AddAsync(item);
            }
            await _context.SaveChangesAsync();
            return clientes;
        }

        [Fact]
        public async Task GetClientesAsyncComRetorno()
        {
            var registros = await InsereRegistros();
            var retorno = await _repository.GetClientesAsync();

            retorno.Should().HaveCount(registros.Count);
            retorno.First().Endereco.Should().NotBeNull();
            retorno.First().Telefones.Should().NotBeNull();
        }

        [Fact]
        public async Task GetClientesAsyncVazio()
        {
            var retorno = await _repository.GetClientesAsync();
            retorno.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetClienteAsyncEncontrado()
        {
            var registros = await InsereRegistros();
            var retorno = await _repository.GetClienteAsync(registros.First().Id);
            retorno.Should().BeEquivalentTo(registros.First());
        }

        [Fact]
        public async Task GetClienteAsyncNaoEncontrado()
        {
            var retorno = await _repository.GetClienteAsync(1);
            retorno.Should().BeNull();
        }

        [Fact]
        public async Task InsertClienteAsyncSucesso()
        {
            var retorno = await _repository.InsertClienteAsync(_cliente);
            retorno.Should().BeEquivalentTo(_cliente);
        }

        [Fact]
        public async Task UpdateClienteAsyncSucesso()
        {
            var registros = await InsereRegistros();
            var clienteAlterado = _clienteFaker.Generate();
            clienteAlterado.Id = registros.First().Id;
            var retorno = await _repository.UpdateClienteAsync(clienteAlterado);
            retorno.Should().BeEquivalentTo(clienteAlterado);
        }

        [Fact]
        public async Task UpdateClienteAsyncAdicionaTelefone()
        {
            var registros = await InsereRegistros();
            var clienteAlterado = registros.First();
            clienteAlterado.Telefones.Add(new TelefoneFaker(clienteAlterado.Id).Generate());
            var retorno = await _repository.UpdateClienteAsync(clienteAlterado);
            retorno.Should().BeEquivalentTo(clienteAlterado);
        }

        [Fact]
        public async Task UpdateClienteAsyncRemoveTelefone()
        {
            var registros = await InsereRegistros();
            var clienteAlterado = registros.First();
            clienteAlterado.Telefones.Remove(clienteAlterado.Telefones.First());
            var retorno = await _repository.UpdateClienteAsync(clienteAlterado);
            retorno.Should().BeEquivalentTo(clienteAlterado);
        }

        [Fact]
        public async Task UpdateClienteAsyncRemoveTodosTelefones()
        {
            var registros = await InsereRegistros();
            var clienteAlterado = registros.First();
            clienteAlterado.Telefones.Clear();
            var retorno = await _repository.UpdateClienteAsync(clienteAlterado);
            retorno.Should().BeEquivalentTo(clienteAlterado);
        }

        [Fact]
        public async Task UpdateClienteAsyncNaoEncontrado()
        {
            var retorno = await _repository.UpdateClienteAsync(_cliente);
            retorno.Should().BeNull();
        }

        [Fact]
        public async Task DeleteClienteAsyncSucesso()
        {
            var registros = await InsereRegistros();
            var retorno = await _repository.DeleteClienteAsync(registros.First().Id);
            retorno.Should().BeEquivalentTo(registros.First());
        }

        [Fact]
        public async Task DeleteClienteAsyncNaoEncontrado()
        {
            var retorno = await _repository.DeleteClienteAsync(1);
            retorno.Should().BeNull();
        }

    }
}
