using Bogus;
using CT.Core.Shared.ModelsViews;
using CT.Core.Shared.ModelsViews.Cliente;
using CT.FakeData.EnderecoData;
using CT.FakeData.TelefoneData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.FakeData.ClienteData;

public class AlteraClienteFaker : Faker<AlteraCliente>
{
    public AlteraClienteFaker()
    {
        var id = new Faker().Random.Number(1, 100);
        RuleFor(o => o.Id, _ => id);
        RuleFor(o => o.Nome, f => f.Person.FullName);
        RuleFor(o => o.Sexo, f => f.PickRandom<SexoView>());
        RuleFor(o => o.Telefones, _ => new NovoTelefoneFaker().Generate(3));
        RuleFor(o => o.Endereco, _ => new NovoEnderecoFaker().Generate());
    }
}
