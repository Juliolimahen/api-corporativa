using AutoMapper;
using CT.Core.Domain;
using CT.Core.Shared.ModelsViews;
using CT.Core.Shared.ModelsViews.Medico;
using CT.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CT.Manager.Implementation
{
    public class MedicoManager : IMedicoManager
    {
        private readonly IMedicoRepository _repository;
        private readonly IMapper _mapper;

        public MedicoManager(IMedicoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
            var medico = _mapper.Map<Medico>(novoMedico);
            return _mapper.Map<MedicoView>(await _repository.InsertMedicoAsync(medico));
        }

        public async Task<MedicoView> UpdateMedicoAsync(AlteraMedico alteraMedico)
        {
            var medico = _mapper.Map<Medico>(alteraMedico);
            return _mapper.Map<MedicoView>(await _repository.UpdateMedicoAsync(medico));
        }

        public async Task DeleteMedicoAsync(int id)
        {
            await _repository.DeleteMedicoAsync(id);
        }
    }
}
