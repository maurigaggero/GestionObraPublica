using IISPI.BD.Data;
using IISPI.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.Repositorio.Repos
{
    public class ItemsControlsDocsRepositorio : Repositorio<ItemControlDoc> , IItemsControlsDocsRepositorio
    {
        private readonly BDContext context;
        public ItemsControlsDocsRepositorio(BDContext context) : base(context)
        {
            this.context = context;
        }
    }
}
