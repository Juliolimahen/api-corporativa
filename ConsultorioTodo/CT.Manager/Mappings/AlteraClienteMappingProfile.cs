using AutoMapper;
using CT.Core.Domain;
using CT.Core.Shared.ModelsViews;
using System;

namespace CT.Manager.Mappings;

public class AlteraClienteMappingProfile : Profile
{
    public AlteraClienteMappingProfile()
    {
        CreateMap<AlteraCliente, Cliente>()
            .ForMember(d => d.UltimaAtualizacao, o => o.MapFrom(x => DateTime.Now))
            .ForMember(d => d.DataNascimento, o => o.MapFrom(x => x.DataNascimento.Date));
    }
}
