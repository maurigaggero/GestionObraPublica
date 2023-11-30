using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.ENUM
{
    public enum EnumRol
    {
        Admin = 1,         // TODO HABILITADO (GENERA USUARIOS)
        BaseDatos = 2,     // ABM DE TABLAS TIPOS, ITEMS, CONTRATOS, EVENTOS, CERTIFICADOS 
        HyS = 3,           // ABM DE EVENTOS DE TIPO HyS.
        Zona1 = 4,         // ABM DE CERTIFICADOS Y EVENTOS EXCEPTO TIPO HyS 
        Zona2 = 5,         // ABM DE CERTIFICADOS Y EVENTOS EXCEPTO TIPO HyS DE LA ZONA PROPIA
        Frente = 6,        // AM(REGISTRO PROPIO) DE EVENTOS EXCEPTO TIPO HyS.
        Consulta1 = 0,     // CONSULTA DE CERTIFICADOS Y EVENTOS EXCEPTO TIPO HyS DE LA ZONA PROPIA
        Consulta2 = 8      // TABLERO DE CONTROL
    }
}
