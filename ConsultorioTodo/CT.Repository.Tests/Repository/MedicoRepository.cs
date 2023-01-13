using CT.Core.Domain;
using CT.Data.Context;
using CT.Data.Repository;
using CT.FakeData.EspecialidadeFake;
using CT.FakeData.MedicoData;
using CT.Manager.Interfaces.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CT.Repository.Tests.Repository;

public class MedicoRepositoryTest : IDisposable
{
    private readonly IMedicoRepository _repository;
    private readonly AppDbContext _context;
    private readonly Medico _medico;
    private readonly MedicoFaker _medicoFaker;

    public MedicoRepositoryTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new AppDbContext(optionsBuilder.Options);
        _repository = new MedicoRepository(_context);

        _medicoFaker = new MedicoFaker();
        _medico = _medicoFaker.Generate();
    }

    private async Task<List<Medico>> InsereMedicos()
    {
        List<Especialidade> especialidades = await InsereEspecialidades();

        var medicos = _medicoFaker.Generate(100);
        foreach (var med in medicos)
        {
            med.Id = 0;
            var random = new Random();
            var listEsp = new List<Especialidade> {
                especialidades.ElementAt(random.Next(especialidades.Count)),
                especialidades.ElementAt(random.Next(especialidades.Count))
            };
            med.Especialidades = listEsp;
            await _context.Medicos.AddAsync(med);
        }
        await _context.SaveChangesAsync();
        return medicos;
    }

    private async Task<List<Especialidade>> InsereEspecialidades()
    {
        var especialidades = new EspecialidadeFaker().Generate(100);

        foreach (var especialidade in especialidades)
        {
            especialidade.Id = 0;
            await _context.Especialidades.AddAsync(especialidade);
        }
        await _context.SaveChangesAsync();
        return especialidades;
    }

    [Fact]
    public async Task GetMedicosAsyncComRetorno()
    {
        var registros = await InsereMedicos();
        var retorno = await _repository.GetMedicosAsync();

        retorno.Should().HaveCount(registros.Count);
        retorno.First().Especialidades.Should().NotBeNull();
    }

    [Fact]
    public async Task GetMedicosAsyncVazio()
    {
        var retorno = await _repository.GetMedicosAsync();
        retorno.Should().HaveCount(0);
    }

    [Fact]
    public async Task GetMedicoAsyncEncontrado()
    {
        var registros = await InsereMedicos();
        var retorno = await _repository.GetMedicoAsync(registros.First().Id);
        retorno.Nome.Should().Be(registros.First().Nome);
        retorno.Crm.Should().Be(registros.First().Crm);
        retorno.Especialidades.Should().HaveCount(registros.First().Especialidades.Count);
    }

    [Fact]
    public async Task GetMedicoAsyncNaoEncontrado()
    {
        var retorno = await _repository.GetMedicoAsync(1);
        retorno.Should().BeNull();
    }

    [Fact]
    public async Task InsertMedicoAsyncSucesso()
    {
        var especialidades = await InsereEspecialidades();
        var random = new Random();
        var listEsp = new List<Especialidade> {
                especialidades.ElementAt(random.Next(especialidades.Count)),
                especialidades.ElementAt(random.Next(especialidades.Count))
            };
        _medico.Especialidades = listEsp;
        var retorno = await _repository.InsertMedicoAsync(_medico);
        retorno.Should().BeEquivalentTo(_medico);
    }

    [Fact]
    public async Task UpdateMedicoAsyncSucesso()
    {
        var registros = await InsereMedicos();
        var medicoAlterado = _medicoFaker.Generate();
        medicoAlterado.Id = registros.First().Id;
        var retorno = await _repository.UpdateMedicoAsync(medicoAlterado);
        retorno.Should().BeEquivalentTo(medicoAlterado);
    }

    [Fact]
    public async Task UpdateMedicoAsyncAdicionaEspecialidade()
    {
        await InsereMedicos();

        var medicoAlterado = await _context.Medicos.Include(p => p.Especialidades).AsNoTracking().FirstAsync();
        var especialidade = await _context.Especialidades
                                            .Where(p => !medicoAlterado
                                                        .Especialidades
                                                        .Select(i => i.Id)
                                                        .Contains(p.Id))
                                            .AsNoTracking()
                                            .FirstAsync();
        medicoAlterado.Especialidades.Add(especialidade);
        var retorno = await _repository.UpdateMedicoAsync(medicoAlterado);

        retorno.Especialidades.Should().HaveCount(medicoAlterado.Especialidades.Count);
    }

    [Fact]
    public async Task UpdateMedicoAsyncRemoveEspecialidade()
    {
        await InsereMedicos();
        var medicoAlterado = await _context.Medicos.Include(p => p.Especialidades).AsNoTracking().FirstAsync();
        medicoAlterado.Especialidades.Remove(medicoAlterado.Especialidades.First());
        var retorno = await _repository.UpdateMedicoAsync(medicoAlterado);
        retorno.Especialidades.Should().HaveCount(medicoAlterado.Especialidades.Count);
        retorno.Especialidades.First().Id.Should().Be(medicoAlterado.Especialidades.First().Id);
    }

    [Fact]
    public async Task UpdateMedicoAsyncRemoveTodasEspecialidades()
    {
        var registros = await InsereMedicos();
        var medicoAlterado = registros.First();
        medicoAlterado.Especialidades.Clear();
        var retorno = await _repository.UpdateMedicoAsync(medicoAlterado);
        retorno.Should().BeEquivalentTo(medicoAlterado);
    }

    [Fact]
    public async Task UpdateMedicoAsyncNaoEncontrado()
    {
        var retorno = await _repository.UpdateMedicoAsync(_medico);
        retorno.Should().BeNull();
    }

    [Fact]
    public async Task DeleteMedicoAsyncSucesso()
    {
        var registros = await InsereMedicos();
        var retorno = await _repository.DeleteMedicoAsync(registros.First().Id);
        retorno.Should().BeEquivalentTo(registros.First());
    }

    [Fact]
    public async Task DeleteMedicoAsyncNaoEncontrado()
    {
        var retorno = await _repository.DeleteMedicoAsync(1);
        retorno.Should().BeNull();
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
    }
}
