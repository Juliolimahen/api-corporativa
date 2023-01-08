using System;
using System.Collections.Generic;
using System.Text;

namespace CT.Core.Domain
{
    public class Especialidade
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public ICollection<Medico> Medicos { get; set; }
    }
}
