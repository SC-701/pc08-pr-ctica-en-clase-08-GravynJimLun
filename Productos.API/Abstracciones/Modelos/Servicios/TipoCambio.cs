using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Servicios
{
    namespace Abstracciones.Modelos.Servicios
    {
        public class TipoCambioBCCRResponse
        {
            public bool estado { get; set; }
            public string mensaje { get; set; }
            public List<Dato> datos { get; set; }
        }

        public class Dato
        {
            public string titulo { get; set; }
            public string periodicidad { get; set; }
            public List<Indicador> indicadores { get; set; }
        }

        public class Indicador
        {
            public string codigoIndicador { get; set; }
            public string nombreIndicador { get; set; }
            public List<Serie> series { get; set; }
        }

        public class Serie
        {
            public string fecha { get; set; }
            public decimal valorDatoPorPeriodo { get; set; }
        }
    }
}
