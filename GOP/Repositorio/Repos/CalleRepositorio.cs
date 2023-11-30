using GOP.BD.Data;
using GOP.BD.Data.Entity;

namespace GOP.Repositorio.Repos
{
    public class CalleRepositorio : Repositorio<Calle>, ICalleRepositorio
    {
        public CalleRepositorio(BDContext context) : base(context)
        {
        }
    }
}
