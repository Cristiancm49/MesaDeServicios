using MicroApi.Seguridad.Domain.DTOs;

using Microsoft.IdentityModel.Tokens;
using System.DirectoryServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MicroApi.Seguridad.Data.Utilities
{
    public class Crypto
    {
        //byte[] bytes = ASCIIEncoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("passwordCrypto"));
        byte[] bytes = ASCIIEncoding.ASCII.GetBytes("xxxxx");

        public RespuestaGeneral Descrypt(string text)
        {
            try
            {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(text));
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cryptoStream);

                return new RespuestaGeneral
                {
                    Status = "Success",
                    Answer = reader.ReadToEnd()
                };
            }
            catch (Exception ex)
            {
                return new RespuestaGeneral()
                {
                    Status = "Fail",
                    Answer = $"No se logró desencriptar {ex.Message}"
                };
            }
        }
    }
}
