using apisHotel.Interfaces;
using apisHotel.Models;
using apisHotel.Models.Api;
using apisHotel.Services;
using apisHotel.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace apisHotel.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _reservaService;
            private readonly Usuario _utilidadUsuario;

        public ReservaController(IReservaService reservaService,
            Usuario utilidades)
        {
            _reservaService = reservaService;
            _utilidadUsuario = utilidades;
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
    }
}
