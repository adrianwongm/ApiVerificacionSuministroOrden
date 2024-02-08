using ApiVerificacionSuministroOrden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using TA03;

namespace ApiVerificacionSuministroOrden.Controllers
{
    public class LoginController : ApiController
    {
        [System.Web.Http.HttpPost]
        public HttpResponseMessage ConsultarTA03([FromBody] UsuarioCredentials credentials)
        {
            try
            {
                TA03.TA03 UsuarioEmplSession = new TA03.TA03();
                UsuarioEmplSession.Login = credentials.Usuario;
                UsuarioEmplSession.PasswordSistema = credentials.Password;

                TA03_BLL obj = new TA03_BLL(UsuarioEmplSession);
                //Linea de prueba
                //TA03.TA03 UsuarioEmplSessionResp = UsuarioEmplSession;

                TA03.TA03 UsuarioEmplSessionResp = obj.obtenerTA03(UsuarioEmplSession.Login, UsuarioEmplSession.PasswordSistema);

                if (UsuarioEmplSessionResp != null)
                {
                    var response = new ApiResponse<TA03.TA03>(true, "Consulta exitosa", UsuarioEmplSessionResp);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    var response = new ApiResponse<TA03.TA03>(false, "No se encontraron resultados", null);
                    return Request.CreateResponse(HttpStatusCode.NotFound, response);
                }
            }
            catch (Exception ex)
            {
                // Aquí podrías devolver un mensaje de error específico en la respuesta si lo deseas
                var errorResponse = new ApiResponse<TA03.TA03>(false, $"Error interno del servidor {ex.Message}", null);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            }
        }
    }

}