﻿using Bogus;
using CT.Core.Domain;
using CT.Core.Shared.ModelsViews.Medico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.FakeData.MedicoData;

public class AlteraMedicoFaker : Faker<AlteraMedico>
{
    public AlteraMedicoFaker()
    {
        var id = new Faker().Random.Number(1, 999999);
        RuleFor(r => r.Id, _ => id);
        RuleFor(r => r.Nome, f => f.Person.FullName);
        RuleFor(r => r.Crm, f => f.Random.Number(1, 9999));
    }
}
