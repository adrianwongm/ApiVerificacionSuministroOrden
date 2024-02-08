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
using TCVS;
using static ApiVerificacionSuministroOrden.Models.ValidacionSuministro;

namespace ApiVerificacionSuministroOrden.Controllers
{
    public class VerificacionController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Grabar([FromBody] Actualizacion productoActualizacion)
        {
            
            try
            {
                Tuple<string,bool> resp = new Tuple<string,bool>("Registro verificado", true);
                //Linea de prueba
                //return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse<bool>(resp.Item2, resp.Item1, false));
                IIML01_BLL producto = new IIML01_BLL();
                productoActualizacion.Respuesta = false;
                //1 - Validacion de codigo barra, se comprueba la existencia en la tablas de productos.
                var validacionProducto = producto.GetDatosProductoValidacion2(productoActualizacion.CodigoBarraProducto,
                                                     productoActualizacion.credentials.Usuario,
                                                     productoActualizacion.credentials.Password);
                if(validacionProducto == null || string.IsNullOrEmpty(validacionProducto.codigoProducto))
                {
                    resp = new Tuple<string, bool>("Registro no verificado . - Codigo producto no valido", false);
                  
                    verficar(productoActualizacion.ToTCVS(), productoActualizacion.credentials.Usuario,
                                                  productoActualizacion.credentials.Password);
                    return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse<bool>(resp.Item2, resp.Item1, false));

                }
                productoActualizacion.CodigoProducto = validacionProducto.codigoProducto;

                //2. - Valicacion del producto con el numero de orden de produccion
                var validacionOrden = producto.GetOrdenValidacion2(productoActualizacion.NumeroOrden,
                                                                   productoActualizacion.CodigoProducto,
                                                                   productoActualizacion.credentials.Usuario,
                                                                   productoActualizacion.credentials.Password);
                if (!validacionOrden)
                {
                    resp = new Tuple<string, bool>("Registro no verificado . - Orden no valida con sus productos", false);
                    verficar(productoActualizacion.ToTCVS(), productoActualizacion.credentials.Usuario,
                                             productoActualizacion.credentials.Password);
                    return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse<bool>(resp.Item2, resp.Item1, false));
                }
                //Validacion de codigo adicional (opcional)
                if (validacionOrden && !string.IsNullOrEmpty(productoActualizacion.CodigoBarraAdicional))
                {
                    var validacionAdicional = producto.GetDatosListaAdicionales2(productoActualizacion.NumeroOrden,
                                                                                 productoActualizacion.credentials.Usuario,
                                                                                 productoActualizacion.credentials.Password);
                    if(validacionAdicional != null && validacionAdicional.Count > 0)
                    {
                       if(!validacionAdicional.Any(x=>x.codigoBarra.Trim() == productoActualizacion.CodigoBarraAdicional))
                        {
                            resp = new Tuple<string, bool>("Registro no verificado . - Codigo adicional de producto no valido", false);
                            verficar(productoActualizacion.ToTCVS(), productoActualizacion.credentials.Usuario,
                                            productoActualizacion.credentials.Password);
                            return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse<bool>(resp.Item2, resp.Item1, false));
                        }
                    }
                }

                //Proceso interno valido
                productoActualizacion.Respuesta = true;
                var respuestaFinal = verficar(productoActualizacion.ToTCVS(),productoActualizacion.credentials.Usuario,
                                                   productoActualizacion.credentials.Password);
                if (!respuestaFinal)
                {
                    resp = new Tuple<string, bool>("Registro no verificado . - Error en el proceso interno de verificacion", false);
                }
                return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse<bool>(resp.Item2, resp.Item1, respuestaFinal));
            }
            catch (Exception ex)
            {
                // Crear una respuesta ApiResponse<T> indicando un error interno del servidor y devolverla
                var errorResponse = new ApiResponse<bool>(false, $"Error interno del servidor. {ex.Message}", false);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }  

        public bool verficar(TCVS.TCVS producto, string usuario, string password)
        { 
           return new TCVS_BLL().Veriificar(producto, 
                                            usuario, 
                                            password);
        }
    }
}