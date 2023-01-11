
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.Core.Shared.ModelsViews.Medico;

public class NovoMedico
{
    public string Nome { get; set; }
    public int Crm { get; set; }
    public ICollection<ReferenciaEspecialidade> Especialidades { get; set; }
}
