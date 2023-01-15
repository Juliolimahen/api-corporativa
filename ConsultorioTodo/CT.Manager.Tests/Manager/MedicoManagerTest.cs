using AutoMapper;
using CT.Core.Domain;
using CT.Core.Shared.ModelsViews;
using CT.Core.Shared.ModelsViews.Medico;
using CT.FakeData.MedicoData;
using CT.Manager.Implementation;
using CT.Manager.Interfaces.Managers;
using CT.Manager.Interfaces.Repositories;
using CT.Manager.Managers;
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
    public class MedicoManagerTest
    {
        private readonly IMedicoRepository _repository;
        private readonly ILogger<MedicoManager> _logger;
        private readonly IMapper _mapper;
        private readonly IMedicoManager _manager;
        private readonly Medico Medico;
        private readonly NovoMedico NovoMedico;
        private readonly AlteraMedico AlteraMedico;
        private readonly MedicoFaker MedicoFaker;
        private readonly NovoMedicoFaker NovoMedicoFaker;
        private readonly AlteraMedicoFaker AlteraMedicoFaker;

        public MedicoManagerTest()
        {
            _repository = Substitute.For<IMedicoRepository>();
            _logger = Substitute.For<ILogger<MedicoManager>>();
            _mapper = new MapperConfiguration(p => p.AddProfile<NovoMedicoMappingProfile>()).CreateMapper();
            _manager = new MedicoManager(_repository, _mapper, _logger);
            MedicoFaker = new MedicoFaker();
            NovoMedicoFaker = new NovoMedicoFaker();
            AlteraMedicoFaker = new AlteraMedicoFaker();

            Medico = MedicoFaker.Generate();
            NovoMedico = NovoMedicoFaker.Generate();
            AlteraMedico = AlteraMedicoFaker.Generate();
        }

        [Fact]
        public async Task GetMedicosAsyncSucesso()
        {
            //Arrange
            var listaMedicos = MedicoFaker.Generate(10);
            _repository.GetMedicosAsync().Returns(listaMedicos);

            //Act
            var controle = _mapper.Map<IEnumerable<Medico>, IEnumerable<MedicoView>>(listaMedicos);
            var retorno = await _manager.GetMedicosAsync();

            //Assert
            await _repository.Received().GetMedicosAsync();
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task GetMedicosAsyncVazio()
        {
            //Arrange
            _repository.GetMedicosAsync().Returns(new List<Medico>());

            //Act
            var retorno = await _manager.GetMedicosAsync();

            //Assert
            await _repository.Received().GetMedicosAsync();
            retorno.Should().BeEquivalentTo(new List<Medico>());
        }

        [Fact]
        public async Task GetMedicoAsyncSucesso()
        {
            //Arrange
            _repository.GetMedicoAsync(Arg.Any<int>()).Returns(Medico);

            //Act
            var controle = _mapper.Map<MedicoView>(Medico);
            var retorno = await _manager.GetMedicoAsync(Medico.Id);

            //Assert
            await _repository.Received().GetMedicoAsync(Arg.Any<int>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task GetMedicoAsyncNaoEncontrado()
        {
            //Arrange
            _repository.GetMedicoAsync(Arg.Any<int>()).Returns(new Medico());

            //Act
            var controle = _mapper.Map<MedicoView>(new Medico());
            var retorno = await _manager.GetMedicoAsync(1);

            //Assert
            await _repository.Received().GetMedicoAsync(Arg.Any<int>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task InsertMedicoAsyncSucesso()
        {
            _repository.InsertMedicoAsync(Arg.Any<Medico>()).Returns(Medico);
            var controle = _mapper.Map<MedicoView>(Medico);
            var retorno = await _manager.InsertMedicoAsync(NovoMedico);

            await _repository.Received().InsertMedicoAsync(Arg.Any<Medico>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task UpdateMedicoAsyncSucesso()
        {
            //Arrange
            _repository.UpdateMedicoAsync(Arg.Any<Medico>()).Returns(Medico);

            //Act
            var controle = _mapper.Map<MedicoView>(Medico);
            var retorno = await _manager.UpdateMedicoAsync(AlteraMedico);

            //Assert
            await _repository.Received().UpdateMedicoAsync(Arg.Any<Medico>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task UpdateMedicoAsyncNaoEncontrado()
        {
            //Arrange
            _repository.UpdateMedicoAsync(Arg.Any<Medico>()).ReturnsNull();

            //Act
            var retorno = await _manager.UpdateMedicoAsync(AlteraMedico);

            //Assert
            await _repository.Received().UpdateMedicoAsync(Arg.Any<Medico>());
            retorno.Should().BeNull();
        }

        [Fact]
        public async Task DeleteMedicoAsyncSucesso()
        {
            //Arrange
            _repository.DeleteMedicoAsync(Arg.Any<int>()).Returns(Medico);

            //Act
            var controle = _mapper.Map<MedicoView>(Medico);
            var retorno = await _manager.DeleteMedicoAsync(Medico.Id);

            //Assert
            await _repository.Received().DeleteMedicoAsync(Arg.Any<int>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task DeleteMedicoAsyncNaoEncontrado()
        {
            //Arrange
            _repository.DeleteMedicoAsync(Arg.Any<int>()).ReturnsNull();

            //Act
            var retorno = await _manager.DeleteMedicoAsync(1);

            //Assert
            await _repository.Received().DeleteMedicoAsync(Arg.Any<int>());
            retorno.Should().BeNull();
        }
    }
}
