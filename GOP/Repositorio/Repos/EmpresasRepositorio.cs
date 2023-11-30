using GOP.BD.Data;
using GOP.BD.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public class EmpresasRepositorio : Repositorio<Empresa> , IEmpresasRepositorio
    {
        public EmpresasRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<Empresa> GetEmpresaById(int id)
        {
            try
            {
                var res = await Context.Set<Empresa>()
                                    .AsNoTracking()
                                    .Include(e => e.Profesionales)
                                    .ThenInclude(e => e.Persona)
                                    .Include(e => e.Eventos)
                                    .Include(e => e.ContratoObras)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<Empresa>> GetEmpresas()
        {
            try
            {
                var list = await Context.Set<Empresa>()
                                    .Include(e => e.Profesionales)
                                    .Include(e => e.Eventos)
                                    .Include(e => e.ContratoObras)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
