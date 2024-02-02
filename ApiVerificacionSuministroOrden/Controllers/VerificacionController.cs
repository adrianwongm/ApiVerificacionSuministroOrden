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
    public class VerificacionController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Grabar([FromBody] Actualizacion productoActualizacion)
        {
            try
            {
               
                if (true)
                {
                    // Crear una respuesta ApiResponse<T> con la lista de objetos IIML y devolverla
                    var response = new ApiResponse<bool>(true, "Registro Grabado correctamente", true);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    // Crear una respuesta ApiResponse<T> indicando que no se encontraron datos y devolverla
                    var response = new ApiResponse<bool>(false, "No se encontraron datos", false);
                    return Request.CreateResponse(HttpStatusCode.NotFound, response);
                }
            }
            catch (Exception ex)
            {
                // Crear una respuesta ApiResponse<T> indicando un error interno del servidor y devolverla
                var errorResponse = new ApiResponse<bool>(false, $"Error interno del servidor {ex.Message}", false);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }  
    }
}