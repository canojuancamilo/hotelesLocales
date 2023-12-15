﻿using apisHotel.Interfaces;
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
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
            private readonly UserManager<Cliente> _userManager;
            private readonly Usuario _utilidadUsuario;

        public HotelController(IHotelService hotelService,
            UserManager<Cliente> userManager,
            Usuario utilidades)
        {
            _hotelService = hotelService;
            _userManager = userManager;
            _utilidadUsuario = utilidades;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HotelModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Rol = await _utilidadUsuario.ObtenerRolAsync(User);

            if (Rol != "Agente")
                return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

            Hotel hotel = new Hotel()
            {
                Nombre = model.Nombre,
                Habilitado = model.Habilitado
            };

            _hotelService.AgregarHotel(hotel);
            return CreatedAtAction(nameof(Get), new { id = hotel.Id }, hotel);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Rol = await _utilidadUsuario.ObtenerRolAsync(User);

            if (Rol != "Agente")
                return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

            var hoteles = _hotelService.ObtenerHoteles();
            return Ok(hoteles);
        }


        [HttpPut("{id}")]
        public async Task <IActionResult> Put(int id, [FromBody] HotelModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Rol = await _utilidadUsuario.ObtenerRolAsync(User);

            if (Rol != "Agente")
                return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

            var existeHotel = _hotelService.ObtenerDetalleHotel(id);

            if (existeHotel == null)
            {
                return NotFound(new { Message = $"El hotel '{id}' no existe." });
            }

            Hotel hotel = new Hotel()
            {
                Id = id,
                Nombre = model.Nombre,
                Habilitado = model.Habilitado
            };

            _hotelService.ActualizarHotel(hotel);

            return Ok(new { Message = "Hotel modificado exitosamente.", Hotel = hotel });
        }

        [HttpPut("ActualizarEstado/{id}")]
        public async Task<IActionResult> ActualizarEstado(int id, [FromBody] bool estado)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Rol = await _utilidadUsuario.ObtenerRolAsync(User);

            if (Rol != "Agente")
                return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

            var existeHotel = _hotelService.ObtenerDetalleHotel(id);

            if (existeHotel == null)
            {
                return NotFound(new { Message = $"El hotel '{id}' no existe." });
            }

            _hotelService.ActualizarEstadoHotel(id, estado);

            var hotel = _hotelService.ObtenerDetalleHotel(id);

            return Ok(new { Message = "Hotel modificado exitosamente.", Hotel = hotel });
        }

        [HttpGet("Disponibles/{FechaEntrada}/{FechaSalida}/{CantidadPersonas}/{Ciudad}")]
        public async Task<IActionResult> ObenerHotelesDisponibles(string FechaEntrada, string FechaSalida, int CantidadPersonas, string Ciudad)
        {
            var Rol = await _utilidadUsuario.ObtenerRolAsync(User);

            if (Rol != "Agente")
                return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

            DateTime fechaEntrada;
            DateTime fechaSalida;

            if (!(DateTime.TryParseExact(FechaEntrada, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaEntrada)) 
                || !(DateTime.TryParseExact(FechaSalida, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaSalida)))
                return BadRequest("Formato de fecha de entrada no válido. Utiliza el formato dd-MM-yyyy.");

            var hoteles = _hotelService.ObtenerHotelesDisponibles(fechaEntrada, fechaSalida, CantidadPersonas, Ciudad);

            if (hoteles == null)
                return NotFound(new { Message = "No se encontraron hoteles disponibles." });

            return Ok(hoteles);
        }
    }
}
