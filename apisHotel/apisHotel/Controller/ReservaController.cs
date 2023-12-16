using apisHotel.Interfaces;
using apisHotel.Models;
using apisHotel.Models.Api;
using apisHotel.Services;
using apisHotel.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace apisHotel.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _reservaService;
        private readonly IHabitacionService _habitacionService;
        private readonly Usuario _utilidadUsuario;
        private readonly EmailService _emailService;

        public ReservaController(IReservaService reservaService,
            IHabitacionService habitacionService, EmailService emailService,
            Usuario utilidades)
        {
            _reservaService = reservaService;
            _habitacionService = habitacionService;
            _utilidadUsuario = utilidades;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Rol = await _utilidadUsuario.ObtenerRolAsync(User);

            if (Rol != "Agente")
                return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

            var reservas = _reservaService.ObtenerReservas();

            return Ok(reservas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var Rol = await _utilidadUsuario.ObtenerRolAsync(User);

            if (Rol != "Agente")
                return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

            var reserva = _reservaService.ObtenerDetalleReserva(id);

            if (reserva == null)
                return NotFound(new { Message = $"La reserva '{id}' no existe." });

            return Ok(reserva);
        }

        [HttpPost("{IdHabitacion}")]
        public async Task<IActionResult> Post(int IdHabitacion, [FromBody] ReservaModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Rol = await _utilidadUsuario.ObtenerRolAsync(User);

            if (Rol != "Agente")
                return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

            DateTime fechaEntrada;
            DateTime fechaSalida;

            if (!(DateTime.TryParseExact(model.FechaEntrada, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaEntrada))
                || !(DateTime.TryParseExact(model.FechaSalida, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaSalida)))
                return BadRequest("Formato de fecha de entrada no válido. Utiliza el formato dd-MM-yyyy.");

            foreach (var huesped in model.Huespedes)
            {
                if (!(DateTime.TryParseExact(model.FechaSalida, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaNacimiento)))
                    return BadRequest("Formato de fecha de entrada no válido. Utiliza el formato dd-MM-yyyy.");
            }

            if(model.CantidadPersonas != model.Huespedes.Count)
                return BadRequest("La CantidadPersonas no es igual a la cantidad de huespedes a registrar.");

            if (fechaEntrada > fechaSalida)
                return BadRequest("Las fecha de entrada no puede ser mayor a la de salida.");

            bool habitacionExiste = _habitacionService.ObtenerDetalleHabitacion(IdHabitacion) != null;

            if (!habitacionExiste)
            {
                return NotFound(new { Message = $"La habitacón '{IdHabitacion}' no existe." });
            }

            Reserva reserva = new Reserva()
            {
                HabitacionId = IdHabitacion,
                FechaEntrada = fechaEntrada,
                FechaSalida = fechaSalida,
                CantidadPersonas = model.CantidadPersonas,
                Huespedes = model.Huespedes.Select(h => new Huesped()
                {
                    NombresApellidos = h.NombresApellidos,
                    FechaNacimiento = DateTime.ParseExact(h.FechaNacimiento, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    Genero = h.Genero,
                    TipoDocumento = h.TipoDocumento,
                    NumeroDocumento = h.NumeroDocumento,
                    Email = h.Email,
                    TelefonoContacto = h.TelefonoContacto
                }).ToList(),
                ContactoEmergencia = new ContactoEmergencia()
                {
                    Nombres = model.ContactoEmergencia.Nombres,
                    TelefonoContacto = model.ContactoEmergencia.TelefonoContacto
                }
            };

            try
            {
                _reservaService.AgregarReservaHabitacion(reserva);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            _emailService.EnviarNotificacionReserva(reserva);

            return CreatedAtAction(nameof(Get), new { id = reserva.Id }, reserva);
        }
    }
}
