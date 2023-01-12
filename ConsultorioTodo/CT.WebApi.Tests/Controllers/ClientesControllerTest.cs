using CT.Core.Shared.ModelsViews.Cliente;
using CT.Manager.Implementation;
using CT.Manager.Interfaces.Managers;
using CT.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CT.WebApi.Tests.Controllers
{
    public class ClientesControllerTest
    {
        private readonly IClienteManager _manager;
        private readonly ILogger<ClientesController> _logger;
        private readonly ClientesController _controller;

        public ClientesControllerTest()
        {
            _manager = Substitute.For<IClienteManager>();
            _logger = Substitute.For<ILogger<ClientesController>>();
            _controller = new ClientesController(_manager, _logger);
        }

        [Fact]
        public async Task GetOk()
        {
            _manager.GetClientesAsync().Returns(new List<ClienteView> { new ClienteView { Nome = "Irineu" } });
            var resultado = (ObjectResult)await _controller.Get();
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
