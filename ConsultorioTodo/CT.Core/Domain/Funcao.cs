using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Core.Domain;

public class Funcao
{
    public int Id { get; set; }
    public string Descricao { get; set; }

    public ICollection<Usuario> Usuarios { get; set; }
}
