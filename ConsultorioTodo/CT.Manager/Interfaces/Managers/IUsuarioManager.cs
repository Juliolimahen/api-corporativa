using CT.Core.Domain;
using CT.Core.Shared.ModelsViews.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Manager.Interfaces.Managers
{
    public interface IUsuarioManager
    {
        Task<IEnumerable<UsuarioView>> GetAsync();

        Task<UsuarioView> GetAsync(string login);

        Task<UsuarioView> InsertAsync(NovoUsuario usuario);

        Task<UsuarioView> UpdateMedicoAsync(Usuario usuario);

        Task<UsuarioLogado> ValidaUsuarioEGeraTokenAsync(Usuario usuario);
    }
}
