using Bogus;
using CT.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.FakeData.EspecialidadeFake;

public class EspecialidadeFaker : Faker<Especialidade>
{
    public EspecialidadeFaker()
    {
        RuleFor(r => r.Id, f => f.Random.Number(1, 9999999));
        RuleFor(r => r.Descricao, f => f.Random.Word());
    }
}
