using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Core.Shared.ModelsViews.Usuario;

public class NovoUsuario
{
    public string Login { get; set; }
    public string Senha { get; set; }

    public ICollection<ReferenciaFuncao> Funcoes { get; set; }
}