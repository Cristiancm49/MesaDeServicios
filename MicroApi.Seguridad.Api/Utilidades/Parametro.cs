using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MicroApi.Seguridad.Api.Utilidades
{
    public class Parametro
    {
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public OracleDbType Tipo { get; set; }
        public ParameterDirection Direccion { get; set; }
        public int Ubicacion { get; set; }
        public int Size { get; set; }

        public Parametro(string nombre, string valor, string tipo, ParameterDirection direction, int size = 32767)
        {
            this.Nombre = nombre;
            this.Valor = valor;
            this.Tipo = OracleTipo(tipo);
            this.Direccion = direction;
            this.Size = size;
        }

        public Parametro(string nombre, string valor, string tipo, ParameterDirection direction)
        {
            this.Nombre = nombre;
            this.Valor = valor;
            this.Tipo = OracleTipo(tipo);
            this.Direccion = direction;
        }

        private OracleDbType OracleTipo(string tipo)
        {
            switch (tipo.ToUpper())
            {
                case "CURSOR":
                    return OracleDbType.RefCursor;
                case "NUMBER":
                    return OracleDbType.Int32;
                case "VARCHAR2":
                    return OracleDbType.Varchar2;
                case "DATE":
                    return OracleDbType.Date;
                case "CLOB":
                    return OracleDbType.Clob;
                case "BLOB":
                    return OracleDbType.Blob;
                default:
                    return OracleDbType.Varchar2;
            }
        }
    }
}
