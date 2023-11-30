using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.Helpers
{
    public static class DllStatic //Sergio
    {
        public static string PeriodoSeparado(string peridoMMYYYY,
                                             string separador="/")
        {
            return $"{peridoMMYYYY.Substring(0, 2)}{separador}{peridoMMYYYY.Substring(2)}";
        }

        public static string CertificadoAnterior(string certificadoMMYYYY)
        {
            int mes = Convert.ToInt32(certificadoMMYYYY.Substring(0, 2));
            int año = Convert.ToInt32(certificadoMMYYYY.Substring(2));
            if (mes == 1)
            {
                mes = 12;
                año = año - 1;
            }
            else
            {
                mes = mes - 1;
            }
            return $"{mes:0#}{año}";
        }

        public static string CertificadoPosterior(string certificadoMMYYYY)
        {
            int mes = Convert.ToInt32(certificadoMMYYYY.Substring(0, 2));
            int año = Convert.ToInt32(certificadoMMYYYY.Substring(2));
            if (mes == 12)
            {
                mes = 1;
                año = año + 1;
            }
            else
            {
                mes = mes + 1;
            }
            return $"{mes:0#}{año}";
        }
    }
}
