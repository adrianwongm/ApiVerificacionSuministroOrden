using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;

namespace IIML01
{
    public class IIML01_BLL
    {
        const int cantBusqueda = 3;
        public IIML GetDatosAdicionalesValidacion(string codigoBarra, string usuario, string password)
        {
            try
            {
                string dsn = Utilidades.obtenerParametro(Utilidades.ParametrosString.DNS);
                string uid = usuario;
                string pwd = password;
                string dbq = Utilidades.obtenerParametro(Utilidades.ParametrosString.DQB);
                var producto = new IIML();

                using (OdbcConnection conexion = new OdbcConnection($"DSN={dsn};Uid={uid};Pwd={pwd};DBQ={dbq}"))
                {
                    conexion.Open();
                    for (int i = 0; i < cantBusqueda; i++)
                    {
                        var parametroEnumString = $"PROCESOBUSQUEDA{i + 1}";
                        if (Enum.TryParse<Utilidades.ParametrosString>(parametroEnumString, out Utilidades.ParametrosString parametroEnum))
                        {
                            var busqueda = Utilidades.obtenerParametro(parametroEnum);
                            busqueda = busqueda.Replace("@W8CODREAD", codigoBarra); 
                            using (OdbcCommand command = new OdbcCommand(busqueda, conexion))
                            {
                                using (OdbcDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            producto.codigoBarra = codigoBarra;
                                            producto.codigoProducto = reader.GetString(1).Trim();
                                            producto.nombre = reader.GetString(2).Trim();
                                            return producto;
                                        }
                                        reader.NextResult();
                                    }
                                }
                            }
                        }
                        else
                        {
                            throw new ArgumentException($"No se pudo parsear '{parametroEnumString}' a un valor de enum.");
                        }
                    }
                    return new IIML();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public IIML GetDatosAdicionalesValidacion2(string codigoBarra, string usuario, string password)
        {
            try
            {
                string dsn = Utilidades.obtenerParametro(Utilidades.ParametrosString.DNS);
                string uid = usuario;
                string pwd = password;
                string dbq = Utilidades.obtenerParametro(Utilidades.ParametrosString.DQB);

                using (OdbcConnection conexion = new OdbcConnection($"DSN={dsn};Uid={uid};Pwd={pwd};DBQ={dbq}"))
                {
                    conexion.Open();
                    for (int i = 0; i < cantBusqueda; i++)
                    {
                        var parametroEnumString = $"PROCESOBUSQUEDA{i + 1}";
                        if (Enum.TryParse<Utilidades.ParametrosString>(parametroEnumString, out Utilidades.ParametrosString parametroEnum))
                        {
                            var busqueda = Utilidades.obtenerParametro(parametroEnum);
                            busqueda = busqueda.Replace("@W8CODREAD", "?");

                            using (OdbcCommand command = new OdbcCommand(busqueda, conexion))
                            {
                                command.Parameters.AddWithValue("@W8CODREAD", codigoBarra);

                                using (OdbcDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        return new IIML()
                                        {
                                            codigoBarra = codigoBarra,
                                            codigoProducto = reader.GetString(1).Trim(),
                                            nombre = reader.GetString(2).Trim()
                                        };
                                    }
                                }
                            }
                        }
                        else
                        {
                            throw new ArgumentException($"No se pudo parsear '{parametroEnumString}' a un valor de enum.");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error al obtener los datos adicionales de validación.", exception);
            }

            return new IIML();
        }

        public bool GetOrdenValidacion(int numeroOrden, string codigoBarra, string usuario, string password)
        {
            try
            {
                string dsn = Utilidades.obtenerParametro(Utilidades.ParametrosString.DNS);
                string uid = usuario;
                string pwd = password;
                string dbq = Utilidades.obtenerParametro(Utilidades.ParametrosString.DQB);
                var producto = new IIML();
                var flag = false;
                using (OdbcConnection conexion = new OdbcConnection($"DSN={dsn};Uid={uid};Pwd={pwd};DBQ={dbq}"))
                {
                    conexion.Open();  
                    var busqueda = Utilidades.obtenerParametro(Utilidades.ParametrosString.PROCESOBUSQUEDAORDEN);
                    busqueda = busqueda.Replace("@W8CODPROD", codigoBarra );
                    busqueda = busqueda.Replace("@W8ORDENP", numeroOrden.ToString());
                    using (OdbcCommand command = new OdbcCommand(busqueda, conexion))
                            {
                                using (OdbcDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            flag = true;
                                            return flag;
                                        }
                                        reader.NextResult();
                                    }
                                }
                            } 
                    return flag;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public bool GetOrdenValidacion2(int numeroOrden, string codigoBarra, string usuario, string password)
        {
            try
            {
                string dsn = Utilidades.obtenerParametro(Utilidades.ParametrosString.DNS);
                string uid = usuario;
                string pwd = password;
                string dbq = Utilidades.obtenerParametro(Utilidades.ParametrosString.DQB);

                using (OdbcConnection conexion = new OdbcConnection($"DSN={dsn};Uid={uid};Pwd={pwd};DBQ={dbq}"))
                {
                    conexion.Open();

                    var busqueda = Utilidades.obtenerParametro(Utilidades.ParametrosString.PROCESOBUSQUEDAORDEN);
                    busqueda = busqueda.Replace("@W8CODPROD", "?");
                    busqueda = busqueda.Replace("@W8ORDENP", "?");

                    using (OdbcCommand command = new OdbcCommand(busqueda, conexion))
                    {
                        command.Parameters.AddWithValue("@W8CODPROD", codigoBarra);
                        command.Parameters.AddWithValue("@W8ORDENP", numeroOrden);

                        using (OdbcDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Si encuentra resultados, establece la bandera como verdadera
                                return true;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error al obtener la validación de la orden.", exception);
            }

            return false; // Si no hay resultados, retorna falso
        }

        public List<IIML> GetDatosListaAdicionales(int numeroOrden, string usuario, string password)
        {
            try
            {
                string dsn = Utilidades.obtenerParametro(Utilidades.ParametrosString.DNS);
                string uid = usuario;
                string pwd = password;
                string dbq = Utilidades.obtenerParametro(Utilidades.ParametrosString.DQB);
                var productos = new List<IIML>();

                using (OdbcConnection conexion = new OdbcConnection($"DSN={dsn};Uid={uid};Pwd={pwd};DBQ={dbq}"))
                {
                    conexion.Open();

                    var busqueda = Utilidades.obtenerParametro(Utilidades.ParametrosString.PROCESOLISTASIMPLE);
                    busqueda = busqueda.Replace("@W8ORDENP", numeroOrden.ToString());
                    //busqueda = busqueda.Replace("@W8CODREAD", codigoBarra);
                    using (OdbcCommand command = new OdbcCommand(busqueda, conexion))
                    {
                        using (OdbcDataReader reader = command.ExecuteReader())
                        {
                            while (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    if (!reader.IsDBNull(1))
                                        productos.Add(new IIML() { codigoProducto = reader.GetString(1), nombre = reader.GetString(2) });
                                    if (!reader.IsDBNull(4))
                                        productos.Add(new IIML() { codigoProducto = reader.GetString(4), nombre = reader.GetString(5) });
                                    if (!reader.IsDBNull(7))
                                        productos.Add(new IIML() { codigoProducto = reader.GetString(7), nombre = reader.GetString(8) });
                                    if (!reader.IsDBNull(10))
                                        productos.Add(new IIML() { codigoProducto = reader.GetString(10), nombre = reader.GetString(11) });
                                }
                                reader.NextResult();
                            }
                        }
                    }
                }
                return productos;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public List<IIML> GetDatosListaAdicionales2(int numeroOrden, string usuario, string password)
        {
            var productos = new List<IIML>();

            try
            {
                string dsn = Utilidades.obtenerParametro(Utilidades.ParametrosString.DNS);
                string uid = usuario;
                string pwd = password;
                string dbq = Utilidades.obtenerParametro(Utilidades.ParametrosString.DQB);

                using (OdbcConnection conexion = new OdbcConnection($"DSN={dsn};Uid={uid};Pwd={pwd};DBQ={dbq}"))
                {
                    conexion.Open();

                    string busqueda = Utilidades.obtenerParametro(Utilidades.ParametrosString.PROCESOLISTASIMPLE);
                    busqueda = busqueda.Replace("@W8ORDENP", "?"); // Usando parámetro
                                                                   //busqueda = busqueda.Replace("@W8CODREAD", codigoBarra);

                    using (OdbcCommand command = new OdbcCommand(busqueda, conexion))
                    {
                        command.Parameters.AddWithValue("@W8ORDENP", numeroOrden); // Asignar valor del parámetro
                        using (OdbcDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (int i = 1; i <= 10; i += 3)
                                {
                                    if (!reader.IsDBNull(i) && !reader.IsDBNull(i + 1))
                                    {
                                        productos.Add(new IIML() { codigoProducto = reader.GetString(i), nombre = reader.GetString(i + 1) });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error al obtener datos adicionales.", exception);
            }

            return productos;
        }

    }
}
