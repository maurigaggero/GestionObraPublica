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
    public class ContratoRepositorio : Repositorio<Contrato>, IContratoRepositorio
    {
        public ContratoRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<Contrato> GetContratoByIdNoFull(int id) //Sergio
        {
            try
            {
                var res = await Context.Set<Contrato>()
                                    .AsNoTracking()
                                    .Include(e => e.Empresa)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<Contrato> GetContratoById(int id) //full
        {
            try
            {
                var res = await Context.Set<Contrato>()
                                    .AsNoTracking()
                                    .Include(e => e.Empresa)
                                    .Include(e => e.Eventos)
                                    .Include(e => e.Certificados)
                                    .Include(e => e.Zona)
                                    .Include(e => e.Certificados)
                                    .Include(e => e.ContratoEstructuras)
                                    .Include(e => e.ContratoItems)
                                    .ThenInclude(e => e.Item)
                                    .Include(e => e.ContratoDocs)
                                    .OrderBy(e => e.Zona.CodigoZona)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<Contrato>> GetContratos()
        {
            try
            {
                var list = await Context.Set<Contrato>()
                                    .Include(e => e.Empresa)
                                    .Include(e => e.Zona)
                                    .OrderBy(e => e.Zona.CodigoZona)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync(); //Sergio
                return list;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<Contrato>> GetContratosAndItems()
        {
            try
            {
                var list = await Context.Set<Contrato>()
                                    .Include(e => e.Zona)
                                    .Include(e => e.ContratoItems)
                                    .ThenInclude(i => i.Item)
                                    .ThenInclude(i => i.Unidad)
                                    .OrderBy(e => e.Zona.CodigoZona)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }

        public async Task<string> GetCaratula(int id)  //sergio
        {
            try
            {
                var res = await Context.Set<Contrato>()
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(e => e.Id == id);
                if (res == null)
                {
                    return "";
                }
                return res.Caratula;
            }
            catch (Exception) { throw; }
        }
    }
}
