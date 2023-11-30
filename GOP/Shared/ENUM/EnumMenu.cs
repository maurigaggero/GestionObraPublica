using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.ENUM
{
    public enum EnumMenu
    {
        Nada = 0,          // muestra solo evento
        Evento = 1,        // muestra solo evento
        Certificado = 2,   // muestra evento y certificado
        Consulta1 = 3,     // muestra informes de consulta técnica
        Consulta2 = 4,     // muestra informes de consulta marquetin
        Contrato = 5,      // muestra evento, certificado y contrato
        Admin = 6,         // muestra todo
        BaseDatos = 7      // muestra todo menos Persona
    }
}
