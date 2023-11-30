using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; }
        private int registrosPorPagina = 10;
        private readonly int CantMaximaPorPagina = 25;

        public int RegistrosPorPagina
        {
            get
            {
                return registrosPorPagina;
            }
            set
            {
                registrosPorPagina = value > CantMaximaPorPagina ? CantMaximaPorPagina : value;
            }
        }
    }
}
