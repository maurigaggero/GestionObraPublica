using IISPI.BD.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.Repositorio
{
    public class Repositorio<E> : IRepositorio<E> where E : class, IEntidadBase
    {
        private readonly BDContext context;

        public Repositorio(BDContext context)
        {
            this.context = context;
        }

        public async Task<List<E>> GetFull()
        {
            try
            {
                var list = await context.Set<E>().ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<E>> GetActivos()
        {
            {
                try
                {
                    var list = await context.Set<E>()
                        .Where(e => e.EstadoRegistro == 0).ToListAsync();
                    return list;
                }
                catch (Exception) { throw; }
            }
        }

        public async Task<E> GetById(int id)
        {
            try
            {
                var res = await context.Set<E>().AsNoTracking()
                                              .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception e) { throw; }
        }

        public async Task<int> Insert(E entity)
        {
            try
            {
                await context.Set<E>().AddAsync(entity);
                await context.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception e) { throw; }
        }

        public async Task<bool> Update(E entrada, int id)
        {
            try
            {
                if (entrada.Id != id)
                {
                    return false;
                }
                var entity = GetById(id);
                if (entity == null)
                {
                    return false;
                }

                context.Set<E>().Update(entrada);
                var resp = await context.SaveChangesAsync();
                return resp > 0;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var entity = GetById(id).Result;
                if (entity == null)
                {
                    return false;
                }
                context.Set<E>().Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> Baja(int id)
        {
            try
            {
                var entity = GetById(id).Result;
                if (entity == null)
                {
                    return false;
                }
                string pepe = entity.Observacion == string.Empty ? string.Empty : $"{entity.Observacion}/r/n";
                entity.Observacion = $"{pepe}Dado de baja {DateTime.UtcNow.ToString()}";
                entity.EstadoRegistro = Shared.ENUM.EnumEstadoRegistro.borrado; //ERROR

                context.Set<E>().Update(entity);
                var resp = await context.SaveChangesAsync();
                return resp > 0;
            }
            catch (Exception) { throw; }
        }
    }
}
