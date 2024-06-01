using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MicroApi.Seguridad.Data.Conections
{
    public class BdConection
    {
        public string ProcedureAPI(string procedimiento, List<Parametro> parametros, string cadena) {
            DataTable datos = new DataTable();
            try
            {
                using (OracleConnection connection = new OracleConnection(cadena))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand(procedimiento, connection))
                    {
                        OracleDataReader reader;
                        command.CommandType = CommandType.StoredProcedure;

                        foreach (Parametro x in parametros)
                        {
                            if (x.Direccion == ParameterDirection.ReturnValue)
                                command.Parameters.Insert(0, new OracleParameter(x.Nombre, x.Tipo));
                            else
                                command.Parameters.Add(new OracleParameter(x.Nombre, x.Tipo));

                            command.Parameters[x.Nombre].Direction = x.Direccion;
                            if (x.Direccion == ParameterDirection.Input)
                                command.Parameters[x.Nombre].Value = x.Valor;
                            else
                                command.Parameters[x.Nombre].Size = x.Size;
                        }
                        reader = command.ExecuteReader();
                        datos.Load(reader);
                        // Convierte el DataTable a formato JSON
                        var jsonData = JsonConvert.SerializeObject(datos, Newtonsoft.Json.Formatting.Indented);

                        return (jsonData);
                    }
                }
            }
            catch (Exception ex)
            {
                return "[]";
            }
        }
    }
}
