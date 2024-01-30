using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiVerificacionSuministroOrden.Models
{
    public class ValidacionSuministro
    {
        public string CodigoBarraProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public int NumeroOrden { get; set; }
        public string CodigoBarraAdicional { get; set; }
        public string DescripcionAdicional { get; set; }

        public class Actualizacion
        {
            public string CodigoBarraProducto { get; set; } 
            public int NumeroOrden { get; set; }
            public string CodigoBarraAdicional { get; set; }

            public UsuarioCredentials credentials { get; set; }
        }
    }
}