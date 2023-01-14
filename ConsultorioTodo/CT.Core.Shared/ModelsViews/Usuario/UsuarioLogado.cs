using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Core.Shared.ModelsViews.Usuario;

public class UsuarioLogado
{
    public string Login { get; set; }
    public ICollection<FuncaoView> Funcoes { get; set; }
    public string Token { get; set; }
}