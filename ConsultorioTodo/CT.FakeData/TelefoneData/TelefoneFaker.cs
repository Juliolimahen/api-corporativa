using Bogus;
using CT.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.FakeData.TelefoneData;

public class TelefoneFaker : Faker<Telefone>
{
    public TelefoneFaker(int clientId)
    {
        RuleFor(o => o.ClienteId, _ => clientId);
        RuleFor(o => o.Numero, f => f.Person.Phone);
    }
}
