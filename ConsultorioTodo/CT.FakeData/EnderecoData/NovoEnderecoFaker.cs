using Bogus;
using CT.Core.Shared.ModelsViews;
using CT.Core.Shared.ModelsViews.Endereco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.FakeData.EnderecoData;

public class NovoEnderecoFaker : Faker<NovoEndereco>
{
    public NovoEnderecoFaker()
    {
        RuleFor(p => p.Numero, f => f.Address.BuildingNumber());
        RuleFor(p => p.Cep, f => Convert.ToInt32(f.Address.ZipCode().Replace("-", "")));
        RuleFor(p => p.Cidade, f => f.Address.City());
        RuleFor(p => p.Estado, f => f.PickRandom<EstadoView>());
        RuleFor(p => p.Logradouro, f => f.Address.StreetName());
        RuleFor(p => p.Complemento, f => f.Lorem.Sentence(20));
    }
}
