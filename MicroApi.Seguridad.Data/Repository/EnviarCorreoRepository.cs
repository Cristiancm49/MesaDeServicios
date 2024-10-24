using Microsoft.Extensions.Options;
using MicroApi.Seguridad.Data.Utilities;
using System.Net;
using System.Net.Mail;
using MicroApi.Seguridad.Domain.DTOs.Correo;
using MicroApi.Seguridad.Domain.Interfaces;

namespace MicroApi.Seguridad.Data.Repository
{
    public class EnviarCorreoRepository : IEnviarCorreoRepository
    {
        private readonly SmtpSettings _smtpSettings;

        public EnviarCorreoRepository(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task NotificarCorreoAsync(NotificarCorreoDTO dto)
        {
            try
            {
                var mensaje = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.User, "Tu Nombre"),
                    Subject = dto.Asunto ?? "Asunto predeterminado",
                    Body = new PlantillaCorreo().ObtenerPlantillaCorreo(dto.Mensaje),
                    IsBodyHtml = true
                };

                mensaje.To.Add(new MailAddress(dto.Correo, dto.Nombres));

                using (var cliente = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port))
                {
                    cliente.Credentials = new NetworkCredential(_smtpSettings.User, _smtpSettings.Password);
                    cliente.EnableSsl = true;
                    await cliente.SendMailAsync(mensaje);
                }
            }
            catch (SmtpException smtpEx)
            {
                // Manejo de errores de SMTP
                throw new Exception($"Error al enviar el correo: {smtpEx.Message}");
            }
        }
    }
}
