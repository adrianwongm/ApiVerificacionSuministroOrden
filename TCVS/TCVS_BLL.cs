using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCVS
{
    public class TCVS_BLL
    {
        public bool Veriificar (TCVS proceso, string usuario, string password)
        {
            bool band = false;
            try
            {
                string dsn = Utilidades.obtenerParametro(Utilidades.ParametrosString.DNS);
                string uid = usuario;
                string pwd = password;
                string dbq = Utilidades.obtenerParametro(Utilidades.ParametrosString.DQB);

                using (OdbcConnection conexion = new OdbcConnection($"DSN={dsn};Uid={uid};Pwd={pwd};DBQ={dbq}"))
                {
                    conexion.Open();

                    var sqlString = Utilidades.obtenerParametro(Utilidades.ParametrosString.PROCESOINSERCION);
 

                    using (OdbcCommand command = new OdbcCommand(sqlString, conexion))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@TCNROP", proceso.TCNROP);
                        command.Parameters.AddWithValue("@TCDPRO", proceso.TCDPRO);
                        command.Parameters.AddWithValue("@TCDEIX", proceso.TCDEIX);
                        command.Parameters.AddWithValue("@TCDADI", proceso.TCDADI);
                        command.Parameters.AddWithValue("@TCUSRP", proceso.TCUSRP);
                        command.Parameters.AddWithValue("@TCFPRO", proceso.TCFPRO);
                        command.Parameters.AddWithValue("@TCHPRO", proceso.TCHPRO);
                        command.Parameters.AddWithValue("@TCRESP", proceso.TCRESP);

                        command.ExecuteNonQuery();
                        command.Dispose();
                    }
                }
                band =  true;
            }
            catch (Exception exception)
            {
                throw new Exception("Error al obtener la validación de la orden.", exception);
            }

            return band; // Si no hay resultados, retorna falso
        }

    }
}
