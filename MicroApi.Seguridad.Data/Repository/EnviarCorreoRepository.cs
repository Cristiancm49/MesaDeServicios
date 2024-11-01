using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MicroApi.Seguridad.Data.Utilities;
using MicroApi.Seguridad.Domain.Interfaces;
using MicroApi.Seguridad.Domain.DTOs.Correo;

namespace MicroApi.Seguridad.Data.Repository
{
    public class EnviarCorreoRepository : IEnviarCorreoRepository
    {
        private readonly EmailSettings emailSettings;
        private readonly PlantillaCorreo plantillaCorreo;

        public EnviarCorreoRepository(IOptions<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value;
            this.plantillaCorreo = new PlantillaCorreo();
        }

        public async Task NotificarCorreoAsync(NotificarCorreoDTO dto)
        {
            /*
            string parrafoPredeterminado = "Estimado usuario, le informamos que:";
            string mensajeCompleto = $"{parrafoPredeterminado}<br/><br/>{dto.Mensaje}";
            string cuerpoCorreo = plantillaCorreo.ObtenerPlantillaCorreo(mensajeCompleto);
            */

            string cuerpoCorreo = plantillaCorreo.ObtenerPlantillaCorreo(dto.Mensaje);

            using (var mail = new MailMessage())
            {
                mail.From = new MailAddress(emailSettings.Username);
                mail.To.Add(dto.Correo);
                mail.Subject = dto.Asunto;
                mail.Body = cuerpoCorreo;
                mail.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    smtp.Host = emailSettings.SmtpServer;
                    smtp.Port = emailSettings.Port;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);
                    await smtp.SendMailAsync(mail);
                }
            }
        }
    }
}