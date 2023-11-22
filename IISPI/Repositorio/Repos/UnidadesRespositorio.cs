using IISPI.BD.Data;
using IISPI.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.Repositorio.Repos
{
    public class UnidadesRespositorio : Repositorio<Unidad>, IUnidadesRepositorio
    {
        private readonly BDContext context;

        public UnidadesRespositorio(BDContext context) : base(context)
        {
            this.context = context;
        }
    }
}
