using Bogus;
using CT.Core.Shared.ModelsViews;
using CT.Core.Shared.ModelsViews.Cliente;
using Bogus.Extensions.Brazil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CT.FakeData.TelefoneData;
using CT.FakeData.EnderecoData;

namespace CT.FakeData.ClienteData
{
    public class NovoClienteFaker : Faker<NovoCliente>
    {
        public NovoClienteFaker()
        {
            RuleFor(p => p.Nome, f => f.Person.FullName);
            RuleFor(p => p.Sexo, f => f.PickRandom<SexoView>());
            RuleFor(p => p.Documento, f => f.Person.Cpf());
            RuleFor(p => p.DataNascimento, f => f.Date.Past());
            RuleFor(p => p.Telefones, _ => new NovoTelefoneFaker().Generate(3));
            RuleFor(p => p.Endereco, _ => new NovoEnderecoFaker().Generate());
        }
    }
}
