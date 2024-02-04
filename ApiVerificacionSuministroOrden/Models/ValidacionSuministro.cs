using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCVS;
using static ApiVerificacionSuministroOrden.Models.ValidacionSuministro;

namespace ApiVerificacionSuministroOrden.Models
{
    public class ValidacionSuministro
    {
        public int NumeroOrden { get; set; }
        public string CodigoBarraProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public string CodigoBarraAdicional { get; set; }
        public string DescripcionAdicional { get; set; }

        public class Actualizacion
        {
            public int NumeroOrden { get; set; }
            public string CodigoBarraProducto { get; set; }
            public string CodigoProducto { get; set; }
            public string CodigoBarraAdicional { get; set; }
            public UsuarioCredentials credentials { get; set; }
            public bool Respuesta { get; set; }

            
        } 
      
    }

    public static class Conversiones
    {
        public static TCVS.TCVS ToTCVS(this Actualizacion origen)
        {
            return new TCVS.TCVS { TCNROP = origen.NumeroOrden ,
                                   TCDPRO = origen.CodigoBarraProducto,
                                   TCDEIX = origen.CodigoProducto,
                                   TCDADI = origen.CodigoBarraAdicional,
                                   TCFPRO = DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day,
                                   TCHPRO = DateTime.Now.Hour * 10000 + DateTime.Now.Minute * 100 + DateTime.Now.Second,
                                   TCUSRP = origen.credentials.Usuario,
                                   TCRESP = (origen.Respuesta?1:0),
            };
        }
    }
}