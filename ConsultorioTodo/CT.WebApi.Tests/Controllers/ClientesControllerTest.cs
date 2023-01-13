using CT.Core.Shared.ModelsViews;
using CT.Core.Shared.ModelsViews.Cliente;
using CT.FakeData.ClienteData;
using CT.Manager.Interfaces.Managers;
using CT.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CT.WebApi.Tests.Controllers
{
    public class ClientesControllerTest
    {
        private readonly IClienteManager _manager;
        private readonly ILogger<ClientesController> _logger;
        private readonly ClientesController _controller;
        private readonly ClienteView _clienteView;
        private readonly List<ClienteView> _listClienteView;
        private readonly NovoCliente _novoCliente;

        public ClientesControllerTest()
        {
            _manager = Substitute.For<IClienteManager>();
            _logger = Substitute.For<ILogger<ClientesController>>();
            _controller = new ClientesController(_manager, _logger);
            _clienteView = new ClienteViewFaker().Generate();
            _listClienteView = new ClienteViewFaker().Generate(10);
            _novoCliente = new NovoClienteFaker().Generate();
        }

        [Fact]
        public async Task GetOk()
        {
            //Arranje
            var controle = new List<ClienteView>();
            _listClienteView.ForEach(p => controle.Add(p.CloneTipado()));
            _manager.GetClientesAsync().Returns(_listClienteView);

            //Act
            var resultado = (ObjectResult)await _controller.Get();

            //Assert 
            await _manager.Received().GetClientesAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task GetNotFound()
        {
            //Arranje 
            _manager.GetClientesAsync().Returns(new List<ClienteView>());

            //Act
            var resultado = (StatusCodeResult)await _controller.Get();

            //Assert 
            await _manager.Received().GetClientesAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task GetByIdOk()
        {
            //Arraje 
            _manager.GetClienteAsync(Arg.Any<int>()).Returns(_clienteView.CloneTipado());

            //Act 
            var resultado = (ObjectResult)await _controller.Get(_clienteView.Id);

            //Assert
            await _manager.Received().GetClienteAsync(Arg.Any<int>());
            resultado.Value.Should().BeEquivalentTo(_clienteView);
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task GetByIdNotFound()
        {
            //Arraje 
            _manager.GetClienteAsync(Arg.Any<int>()).Returns(new ClienteView());

            //Act
            var resultado = (StatusCodeResult)await _controller.Get(1);

            //Assert
            await _manager.Received().GetClienteAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task PostCreated()
        {
            //Arranje
            _manager.InsertClienteAsync(Arg.Any<NovoCliente>()).Returns(_clienteView.CloneTipado());

            //Act
            var resultado = (ObjectResult)await _controller.Post(_novoCliente);

            //Assert
            await _manager.Received().InsertClienteAsync(Arg.Any<NovoCliente>());
            resultado.StatusCode.Should().Be(StatusCodes.Status201Created);
            resultado.Value.Should().BeEquivalentTo(_clienteView);
        }

        [Fact]
        public async Task PutOk()
        {
            //Arranje
            _manager.UpdateClienteAsync(Arg.Any<AlteraCliente>()).Returns(_clienteView.CloneTipado());

            //Act
            var resultado = (ObjectResult)await _controller.Put(new AlteraCliente());

            //Assert
            await _manager.Received().UpdateClienteAsync(Arg.Any<AlteraCliente>());
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(_clienteView);
        }

        [Fact]
        public async Task PutNotFound()
        {
            //Arranje
            _manager.UpdateClienteAsync(Arg.Any<AlteraCliente>()).ReturnsNull();

            //Act
            var resultado = (StatusCodeResult)await _controller.Put(new AlteraCliente());

            //Assert
            await _manager.Received().UpdateClienteAsync(Arg.Any<AlteraCliente>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task DeleteNoContent()
        {
            //Arranje
            _manager.DeleteClienteAsync(Arg.Any<int>()).Returns(_clienteView);

            //Act
            var resultado = (StatusCodeResult)await _controller.Delete(1);

            //Assert
            await _manager.Received().DeleteClienteAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task NotFoundNotFound()
        {
            //Arranje
            _manager.DeleteClienteAsync(Arg.Any<int>()).ReturnsNull();

            //Act
            var resultado = (StatusCodeResult)await _controller.Delete(1);

            //Assert
            await _manager.Received().DeleteClienteAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
