using AutoMapper;
using CT.Core.Domain;
using CT.Core.Shared.ModelsViews;
using CT.Core.Shared.ModelsViews.Cliente;
using CT.Core.Shared.ModelsViews.Endereco;
using CT.Core.Shared.ModelsViews.Telefone;
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.Manager.Mappings
{
    public class NovoClienteMappingProfile : Profile
    {
        public NovoClienteMappingProfile()
        {
            CreateMap<NovoCliente, Cliente>()
                .ForMember(d => d.Criacao, o => o.MapFrom(c => DateTime.Now))
                .ForMember(d => d.DataNascimento, o => o.MapFrom(c => c.DataNascimento.Date))
                //.ReverseMap()
                ;
            CreateMap<NovoEndereco, Endereco>();
            CreateMap<NovoTelefone, Telefone>();
            CreateMap<Cliente, ClienteView>();
            CreateMap<Endereco, EnderecoView>();
            CreateMap<Telefone, TelefoneView>();
        }
    }
}
