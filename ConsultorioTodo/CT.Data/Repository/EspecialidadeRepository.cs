using CT.Data.Context;
using CT.Manager.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CT.Data.Repository
{
    public class EspecialidadeRepository : IEspecialidadeRepository
    {
        private readonly AppDbContext _context;

        public EspecialidadeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Especialidades.AnyAsync(p => p.Id == id);
        }
    }
}
