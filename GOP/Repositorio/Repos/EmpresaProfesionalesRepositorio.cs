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
    public class EmpresaProfesionalesRepositorio : Repositorio<EmpresaProfesional> , IEmpresaProfesionalesRepositorio
    {
        public EmpresaProfesionalesRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<EmpresaProfesional> GetEmpProfesionalfById(int id)
        {
            try
            {
                var res = await Context.Set<EmpresaProfesional>()
                                    .AsNoTracking()
                                    .Include(e => e.Empresa)
                                    .Include(e => e.Persona)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<EmpresaProfesional>> GetEmpProfesionales(int empresaId)
        {
            try
            {
                var list = await Context.Set<EmpresaProfesional>()
                                    .Include(e => e.Empresa)
                                    .Include(e => e.Persona)
                                    .Where(e => e.EstadoRegistro == 0 && e.EmpresaId == empresaId).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
