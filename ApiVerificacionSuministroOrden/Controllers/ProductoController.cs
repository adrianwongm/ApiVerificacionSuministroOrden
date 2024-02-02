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
using static ApiVerificacionSuministroOrden.Models.ValidacionSuministro;

namespace ApiVerificacionSuministroOrden.Controllers
{
    public class ProductoController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage ObtenerListaSimple([FromBody] Actualizacion productoActualizacion)
        {
            try
            {
                // Simulamos la creación de una lista simple de objetos IIML
                var listaSimple = new List<IIML>();
                listaSimple.Add(new IIML() { codigoProducto = "WDEB103CK4055", nombre = "W DEO 10W30 CK-4 8.7 SBL" });
                listaSimple.Add(new IIML() { codigoProducto = "WDEB103CK4000", nombre = "W DEO 10W30 CK-4 8.7 SBL" });
                listaSimple.Add(new IIML() { codigoProducto = "TB55THXSWS01CO", nombre = "TB 55 TH SWISS LUB ROJO - BLAN" });
                listaSimple.Add(new IIML() { codigoProducto = "ETGENERICTX", nombre = "ETIQ TEXACO GENERICA 5 Y 55 GL" });
                listaSimple.Add(new IIML() { codigoProducto = "SEMESOG2", nombre = "SELLO MET GENERICO 2" });
                listaSimple.Add(new IIML() { codigoProducto = "SEMESOG3", nombre = "SELLO MET GENERICO 3 / 4" });
                listaSimple.Add(new IIML() { codigoProducto = "42717", nombre = "GROUP II 100 / 120" });

                // Lógica para obtener la lista simple desde alguna fuente de datos
                // Puedes descomentar esta línea y adaptarla según tu lógica real
                // var listaSimple = obj.GetDatosListaAdicionales2(productoActualizacion.NumeroOrden, productoActualizacion.credentials.Usuario, productoActualizacion.credentials.Password);

                if (listaSimple != null)
                {
                    // Crear una respuesta ApiResponse<T> con la lista de objetos IIML y devolverla
                    var response = new ApiResponse<List<IIML>>(true, "Lista obtenida exitosamente", listaSimple);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    // Crear una respuesta ApiResponse<T> indicando que no se encontraron datos y devolverla
                    var response = new ApiResponse<List<IIML>>(false, "No se encontraron datos", null);
                    return Request.CreateResponse(HttpStatusCode.NotFound, response);
                }
            }
            catch (Exception ex)
            {
                // Crear una respuesta ApiResponse<T> indicando un error interno del servidor y devolverla
                var errorResponse = new ApiResponse<List<IIML>>(false, $"Error interno del servidor {ex.Message}", null);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        [HttpPost]
        public HttpResponseMessage VerificarProducto(string codigoBarra, [FromBody] UsuarioCredentials credentials)
        {
            try
            {
                // Simulamos la verificación de un producto
                var producto = new IIML() { codigoBarra = codigoBarra, codigoProducto = "WDEB103CK4055", nombre = "W DEO 10W30 CK-4 8.7 SBL" };
                //var producto = obj.GetDatosAdicionalesValidacion(codigoBarra, credentials.Usuario, credentials.Password);

                if (producto != null)
                {
                    // Crear una respuesta ApiResponse<T> con el producto y devolverla
                    var response = new ApiResponse<IIML>(true, "Producto verificado exitosamente", producto);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    // Crear una respuesta ApiResponse<T> indicando que no se encontraron datos y devolverla
                    var response = new ApiResponse<IIML>(false, "No se encontraron datos para el producto", null);
                    return Request.CreateResponse(HttpStatusCode.NotFound, response);
                }
            }
            catch (Exception ex)
            {
                // Crear una respuesta ApiResponse<T> indicando un error interno del servidor y devolverla
                var errorResponse = new ApiResponse<IIML>(false, "Error interno del servidor", null);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }
    }
}