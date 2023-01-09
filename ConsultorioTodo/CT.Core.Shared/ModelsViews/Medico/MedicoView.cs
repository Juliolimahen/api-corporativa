using System;
using System.Collections.Generic;
using System.Text;

namespace CT.Core.Shared.ModelsViews
{
    public class MedicoView
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Crm { get; set; }
        public ICollection<EspecialidadeView> Especialidades { get; set; }
    }
}
