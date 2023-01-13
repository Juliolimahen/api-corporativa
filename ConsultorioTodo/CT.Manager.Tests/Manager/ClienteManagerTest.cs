using AutoMapper;
using CT.Core.Domain;
using CT.Core.Shared.ModelsViews;
using CT.Core.Shared.ModelsViews.Cliente;
using CT.FakeData.ClienteData;
using CT.Manager.Implementation;
using CT.Manager.Interfaces.Managers;
using CT.Manager.Interfaces.Repositories;
using CT.Manager.Mappings;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CT.Manager.Tests.Manager
{
    public class ClienteManagerTest
    {
        private readonly IClienteRepository _repository;
        private readonly ILogger<ClienteManager> _logger;
        private readonly IMapper _mapper;
        private readonly IClienteManager _manager;
        private readonly Cliente Cliente;
        private readonly NovoCliente NovoCliente;
        private readonly AlteraCliente AlteraCliente;
        private readonly ClienteFaker ClienteFaker;
        private readonly NovoClienteFaker NovoClienteFaker;
        private readonly AlteraClienteFaker AlteraClienteFaker;

        public ClienteManagerTest()
        {
            _repository = Substitute.For<IClienteRepository>();
            _logger = Substitute.For<ILogger<ClienteManager>>();
            _mapper = new MapperConfiguration(p => p.AddProfile<NovoClienteMappingProfile>()).CreateMapper();
            _manager = new ClienteManager(_repository, _mapper, _logger);
            ClienteFaker = new ClienteFaker();
            NovoClienteFaker = new NovoClienteFaker();
            AlteraClienteFaker = new AlteraClienteFaker();

            Cliente = ClienteFaker.Generate();
            NovoCliente = NovoClienteFaker.Generate();
            AlteraCliente = AlteraClienteFaker.Generate();
        }

        [Fact]
        public async Task GetClientesAsyncSucesso()
        {
            //Arrange
            var listaClientes = ClienteFaker.Generate(10);
            _repository.GetClientesAsync().Returns(listaClientes);

            //Act
            var controle = _mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteView>>(listaClientes);
            var retorno = await _manager.GetClientesAsync();

            //Assert
            await _repository.Received().GetClientesAsync();
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task GetClientesAsyncVazio()
        {
            //Arrange
            _repository.GetClientesAsync().Returns(new List<Cliente>());

            //Act
            var retorno = await _manager.GetClientesAsync();

            //Assert
            await _repository.Received().GetClientesAsync();
            retorno.Should().BeEquivalentTo(new List<Cliente>());
        }

        [Fact]
        public async Task GetClienteAsyncSucesso()
        {
            //Arrange
            _repository.GetClienteAsync(Arg.Any<int>()).Returns(Cliente);

            //Act
            var controle = _mapper.Map<ClienteView>(Cliente);
            var retorno = await _manager.GetClienteAsync(Cliente.Id);

            //Assert
            await _repository.Received().GetClienteAsync(Arg.Any<int>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task GetClienteAsyncNaoEncontrado()
        {
            //Arrange
            _repository.GetClienteAsync(Arg.Any<int>()).Returns(new Cliente());

            //Act
            var controle = _mapper.Map<ClienteView>(new Cliente());
            var retorno = await _manager.GetClienteAsync(1);

            //Assert
            await _repository.Received().GetClienteAsync(Arg.Any<int>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task InsertClienteAsyncSucesso()
        {
            _repository.InsertClienteAsync(Arg.Any<Cliente>()).Returns(Cliente);
            var controle = _mapper.Map<ClienteView>(Cliente);
            var retorno = await _manager.InsertClienteAsync(NovoCliente);

            await _repository.Received().InsertClienteAsync(Arg.Any<Cliente>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task UpdateClienteAsyncSucesso()
        {
            //Arrange
            _repository.UpdateClienteAsync(Arg.Any<Cliente>()).Returns(Cliente);

            //Act
            var controle = _mapper.Map<ClienteView>(Cliente);
            var retorno = await _manager.UpdateClienteAsync(AlteraCliente);

            //Assert
            await _repository.Received().UpdateClienteAsync(Arg.Any<Cliente>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task UpdateClienteAsyncNaoEncontrado()
        {
            //Arrange
            _repository.UpdateClienteAsync(Arg.Any<Cliente>()).ReturnsNull();

            //Act
            var retorno = await _manager.UpdateClienteAsync(AlteraCliente);

            //Assert
            await _repository.Received().UpdateClienteAsync(Arg.Any<Cliente>());
            retorno.Should().BeNull();
        }

        [Fact]
        public async Task DeleteClienteAsyncSucesso()
        {
            //Arrange
            _repository.DeleteClienteAsync(Arg.Any<int>()).Returns(Cliente);

            //Act
            var controle = _mapper.Map<ClienteView>(Cliente);
            var retorno = await _manager.DeleteClienteAsync(Cliente.Id);

            //Assert
            await _repository.Received().DeleteClienteAsync(Arg.Any<int>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task DeleteClienteAsyncNaoEncontrado()
        {
            //Arrange
            _repository.DeleteClienteAsync(Arg.Any<int>()).ReturnsNull();

            //Act
            var retorno = await _manager.DeleteClienteAsync(1);

            //Assert
            await _repository.Received().DeleteClienteAsync(Arg.Any<int>());
            retorno.Should().BeNull();
        }
    }
}
