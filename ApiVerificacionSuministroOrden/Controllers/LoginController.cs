using ApiVerificacionSuministroOrden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TA03;

namespace ApiVerificacionSuministroOrden.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public TA03.TA03 ConsultarTA03([FromBody] UsuarioCredentials credentials)
        {
            try
            {
                TA03.TA03 UsuarioEmplSession = new TA03.TA03();
                UsuarioEmplSession.Login = credentials.Usuario;
                UsuarioEmplSession.PasswordSistema = credentials.Password;

                TA03_BLL obj = new TA03_BLL(UsuarioEmplSession);
                TA03.TA03 UsuarioEmplSessionResp = new TA03.TA03() { Login = credentials.Usuario, PasswordSistema = credentials.Password };
                // TA03.TA03 UsuarioEmplSessionResp = obj.obtenerTA03(UsuarioEmplSession.Login, UsuarioEmplSession.PasswordSistema);

                if (UsuarioEmplSessionResp != null)
                {
                    return UsuarioEmplSessionResp;
                }
                else
                {
                    return null;
                } 
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

    }
}