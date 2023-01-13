using Bogus;
using CT.Core.Shared.ModelsViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.FakeData.TelefoneData;

public class NovoTelefoneFaker : Faker<NovoTelefone>
{
    public NovoTelefoneFaker()
    {
        RuleFor(p => p.Numero, f => f.Person.Phone);
    }
}