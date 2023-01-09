using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CT.Manager.Interfaces
{
    public interface IEspecialidadeRepository
    {
        Task<bool> ExisteAsync(int id);
    }
}
