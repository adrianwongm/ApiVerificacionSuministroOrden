using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCVS
{
    public class TCVS
    {

        /// <summary>
        /// Numero de orden de produccion
        /// </summary>
        public int TCNROP { get; set; }
        /// <summary>
        /// Codigo IPROD (codigo producto)
        /// </summary>
        public string TCDPRO { get; set; }
        /// <summary>
        /// Codigo EIX (codigo de barra)
        /// </summary>
        public string TCDEIX { get; set; }
        /// <summary>
        /// Codigo Adicional (2° codigo de barra)
        /// </summary>
        public string TCDADI { get; set; }
        /// <summary>
        /// Usuario de proceso
        /// </summary>
        public string TCUSRP { get; set; }
        /// <summary>
        /// Fecha de proceso
        /// </summary>
        public int TCFPRO { get; set; }
        /// <summary>
        /// Hora de proceso
        /// </summary>
        public int TCHPRO { get; set; }
        /// <summary>
        /// Resultado (1 - ok  ... 2 - error)
        /// </summary>
        public int TCRESP { get; set; }
    }
}
