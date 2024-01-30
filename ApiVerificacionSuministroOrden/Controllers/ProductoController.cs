using ApiVerificacionSuministroOrden.Models;
using IIML01;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using TA03;

namespace ApiVerificacionSuministroOrden.Controllers
{
    public class ProductoController : ApiController
    {
        [HttpPost]
        public IEnumerable<IIML> ObtenerListaSimple([FromBody] ValidacionSuministro.Actualizacion productoActualizacion)
        {
            try
            { 
                IIML01_BLL obj = new IIML01_BLL();
                var listaSimple = new   List<IIML>();
                listaSimple.Add(new IIML() { codigoProducto = "WDEB103CK4055", nombre = "W DEO 10W30 CK-4 8.7 SBL" });
                listaSimple.Add(new IIML() { codigoProducto = "WDEB103CK4000", nombre = "W DEO 10W30 CK-4 8.7 SBL" });
                listaSimple.Add(new IIML() { codigoProducto = "TB55THXSWS01CO", nombre = "TB 55 TH SWISS LUB ROJO - BLAN" });
                listaSimple.Add(new IIML() { codigoProducto = "ETGENERICTX", nombre = "ETIQ TEXACO GENERICA 5 Y 55 GL" });
                listaSimple.Add(new IIML() { codigoProducto = "SEMESOG2", nombre = "SELLO MET GENERICO 2" });
                listaSimple.Add(new IIML() { codigoProducto = "SEMESOG3", nombre = "SELLO MET GENERICO 3 / 4" });
                listaSimple.Add(new IIML() { codigoProducto = "42717", nombre = "GROUP II 100 / 120" });

                //var listaSimple = obj.GetDatosListaAdicionales2(productoActualizacion.NumeroOrden, productoActualizacion.credentials.Usuario, productoActualizacion.credentials.Password);

                if (listaSimple != null)
                { 
                    return listaSimple;
                } 
                return new List<IIML>();
            }
            catch (Exception ex)
            { 
                    throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public IIML VerificarProducto(string codigoBarra, [FromBody] UsuarioCredentials credentials)
        {
            try
            { 
                IIML01_BLL obj = new IIML01_BLL();
                var producto = new IIML() { codigoBarra = codigoBarra, codigoProducto = "WDEB103CK4055", nombre = "W DEO 10W30 CK-4 8.7 SBL" };
                //var producto = obj.GetDatosAdicionalesValidacion(codigoBarra, credentials.Usuario, credentials.Password);

                if (producto != null)
                {
                    return producto;
                }
                return new IIML();
            }
            catch (Exception ex)
            { 
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
             }
}
    }
}