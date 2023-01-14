using AutoMapper;
using CT.Core.Domain;
using CT.Core.Shared.ModelsViews.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Manager.Mappings;

public class UsuarioMappingProfile : Profile
{
    public UsuarioMappingProfile()
    {
        CreateMap<Usuario, UsuarioView>().ReverseMap();
        CreateMap<Usuario, NovoUsuario>().ReverseMap();
        CreateMap<Usuario, UsuarioLogado>().ReverseMap();
        CreateMap<Funcao, FuncaoView>().ReverseMap();
        CreateMap<Funcao, ReferenciaFuncao>().ReverseMap();
    }
}
