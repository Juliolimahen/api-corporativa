using AutoMapper;
using CT.Core.Domain;
using CT.Core.Shared.ModelsViews;
using CT.Core.Shared.ModelsViews.Medico;
using CT.Manager.Interfaces.Repositories;
using CT.Manager.Managers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CT.Manager.Implementation;
public class MedicoManager : IMedicoManager
{
    private readonly IMedicoRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<MedicoManager> _logger;

    public MedicoManager(IMedicoRepository repository, IMapper mapper, ILogger<MedicoManager> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<MedicoView>> GetMedicosAsync()
    {
        return _mapper.Map<IEnumerable<Medico>, IEnumerable<MedicoView>>(await _repository.GetMedicosAsync());
    }

    public async Task<MedicoView> GetMedicoAsync(int id)
    {
        return _mapper.Map<MedicoView>(await _repository.GetMedicoAsync(id));
    }

    public async Task<MedicoView> InsertMedicoAsync(NovoMedico novoMedico)
    {
        _logger.LogInformation("Chamada de negócio para inserir um médico.");
        var medico = _mapper.Map<Medico>(novoMedico);
        return _mapper.Map<MedicoView>(await _repository.InsertMedicoAsync(medico));
    }

    public async Task<MedicoView> UpdateMedicoAsync(AlteraMedico alteraMedico)
    {
        var medico = _mapper.Map<Medico>(alteraMedico);
        return _mapper.Map<MedicoView>(await _repository.UpdateMedicoAsync(medico));
    }

    public async Task<MedicoView> DeleteMedicoAsync(int id)
    {
       var medico = await _repository.DeleteMedicoAsync(id);
       return _mapper.Map<MedicoView>(medico);
    }
}