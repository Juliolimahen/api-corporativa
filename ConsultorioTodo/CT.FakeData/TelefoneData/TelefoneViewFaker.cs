using Bogus;
using CT.Core.Domain;
using CT.Core.Shared.ModelsViews.Telefone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.FakeData.TelefoneData;

public class TelefoneViewFaker : Faker<TelefoneView>
{
    public TelefoneViewFaker()
    {
        RuleFor(p => p.Id, f => f.Random.Number(1, 10));
        RuleFor(p => p.Numero, f => f.Person.Phone);
    }
}