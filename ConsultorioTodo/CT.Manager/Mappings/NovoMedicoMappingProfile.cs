using AutoMapper;
using CT.Core.Domain;
using CT.Core.Shared.ModelsViews;
using CT.Core.Shared.ModelsViews.Medico;

namespace CT.Manager.Mappings;

public class NovoMedicoMappingProfile : Profile
{
    public NovoMedicoMappingProfile()
    {
        CreateMap<NovoMedico, Medico>().ReverseMap();
        CreateMap<Medico, MedicoView>().ReverseMap();
        CreateMap<Especialidade, ReferenciaEspecialidade>().ReverseMap();
        CreateMap<Especialidade, EspecialidadeView>().ReverseMap();
        CreateMap<Especialidade, NovaEspecialidade>().ReverseMap();
        CreateMap<AlteraMedico, Medico>().ReverseMap();
    }
}
