using Bogus;
using Bogus.Extensions.Brazil;
using CT.Core.Domain;
using CT.Core.Shared.ModelsViews.Cliente;
using CT.FakeData.EnderecoData;
using CT.FakeData.TelefoneData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.FakeData.ClienteData;

public class ClienteViewFaker : Faker<ClienteView>
{
    public ClienteViewFaker()
    {
        var id = new Faker().Random.Number(1, 999999);

        RuleFor(o => o.Id, _ => id);
        RuleFor(o => o.Nome, f => f.Person.FullName);
        RuleFor(o => o.Sexo, f => f.PickRandom<SexoView>());
        RuleFor(o => o.Documento, f => f.Person.Cpf());
        RuleFor(o => o.Criacao, f => f.Date.Past());
        RuleFor(o => o.UltimaAtualizacao, f => f.Date.Past());
        RuleFor(o => o.Telefones, _ => new TelefoneViewFaker().Generate(3));
        RuleFor(o => o.Endereco, _ => new EnderecoViewFaker().Generate());
    }
}
