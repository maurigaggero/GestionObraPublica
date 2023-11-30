using GOP.BD.Data;
using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public class PersonasRepositorio : Repositorio<Persona>, IPersonasRepositorio
    {
        private readonly BDContext context;

        public PersonasRepositorio(BDContext context) : base(context)
        {
            this.context = context;
        }
    }
}
