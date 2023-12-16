using apisHotel.Models;
using apisHotel.Models.Api.Correo;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace apisHotel.Services
{
    public class EmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly ConfiguracionEmail _configuracionEmail;

        public EmailService(IOptions<ConfiguracionEmail> configuracionEmail)
        {
            _configuracionEmail = configuracionEmail.Value;

            _smtpClient = new SmtpClient(_configuracionEmail.SmtpServer)
            {
                Port = _configuracionEmail.SmtpPort,
                Credentials = new NetworkCredential(_configuracionEmail.Username, _configuracionEmail.Password),
                EnableSsl = true,
            };
        }

        public void EnviarNotificacionReserva(Reserva reservaNotification)
        {
            foreach (var huesped in reservaNotification.Huespedes)
            {
                MailMessage mensaje = new MailMessage("canojuancamilo@hotmail.com", huesped.Email)
                {
                    Subject = "Notificación de Reserva",
                    Body = $"Hola {huesped.NombresApellidos}, tu reserva para el {reservaNotification.FechaEntrada.ToString("dd/MM/yyyy")} ha sido confirmada con el id '{reservaNotification.Id}'."
                };
                _smtpClient.Send(mensaje);
            }
        }
    }
}
