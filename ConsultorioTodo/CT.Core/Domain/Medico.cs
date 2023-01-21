using System;
using System.Collections.Generic;
using System.Text;

namespace CT.Core.Domain;

public class Medico
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Crm { get; set; }
    public ICollection<Especialidade> Especialidades { get; set; }

    public Medico()
    {
        Especialidades = new HashSet<Especialidade>();
    }
}
