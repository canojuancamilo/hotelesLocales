﻿using apisHotel.Interfaces;
using apisHotel.Models;
using apisHotel.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace apisHotel.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HabitacionController : ControllerBase
    {
        private readonly IHabitacionService _habitacionService;
        private readonly IHotelService _hotelService;
        private readonly UserManager<Cliente> userManager;
        private readonly Usuario _utilidadUsuario;

        public HabitacionController(IHabitacionService habitacionService,
            IHotelService hoelService,
            UserManager<Cliente> userManager,
            Usuario utilidades)
        {
            _habitacionService = habitacionService;
            _hotelService = hoelService;
            this.userManager = userManager;
            _utilidadUsuario = utilidades;
        }

        /// <summary>
        /// Permite a los agentes registrar una habitación en un hotel.
        /// </summary>
        [HttpPost("{IdHotel}")]
        public async Task<IActionResult> Post(int IdHotel, [FromBody] HabitacionModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var Rol = await _utilidadUsuario.ObtenerRolAsync(User);

                if (Rol != "Agente")
                    return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

                bool hotelExiste = _hotelService.ObtenerDetalleHotel(IdHotel) != null;

                if (!hotelExiste)
                {
                    return NotFound(new { Message = $"El hotel '{IdHotel}' no existe." });
                }

                Habitacion habitacion = new Habitacion()
                {
                    Tipo = model.Tipo,
                    CostoBase = model.CostoBase,
                    Impuestos = model.Impuestos,
                    Ubicacion = model.Ubicacion,
                    Habilitada = model.Habilitada,
                    CantidadPersonas = model.CantidadPeronas
                };

                _habitacionService.AgregarHabitacionHotel(IdHotel, habitacion);
                return CreatedAtAction(nameof(Get), new { id = habitacion.Id }, habitacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Permite a los agentes obtener el detalle de una habitación.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var Rol = await _utilidadUsuario.ObtenerRolAsync(User);

                if (Rol != "Agente")
                    return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

                var habitacion = _habitacionService.ObtenerDetalleHabitacion(id);

                if (habitacion == null)
                    return NotFound(new { Message = $"La habitacion '{id}' no existe." });

                return Ok(habitacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Permite a los agentes actualizar la información de una habitación.
        /// </summary>
        [HttpPut("{idHabitacion}")]
        public async Task<IActionResult> Put(int idHabitacion, [FromBody] HabitacionModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var Rol = await _utilidadUsuario.ObtenerRolAsync(User);

                if (Rol != "Agente")
                    return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

                var existeHabitacion = _habitacionService.ObtenerDetalleHabitacion(idHabitacion);

                if (existeHabitacion == null)
                {
                    return NotFound(new { Message = $"La habitacion '{idHabitacion}' no existe." });
                }

                Habitacion habitacion = new Habitacion()
                {
                    Id = idHabitacion,
                    Tipo = model.Tipo,
                    CostoBase = model.CostoBase,
                    Impuestos = model.Impuestos,
                    Ubicacion = model.Ubicacion,
                    Habilitada = model.Habilitada,
                    CantidadPersonas = model.CantidadPeronas
                };

                _habitacionService.ActualizarHabitacion(habitacion);

                return Ok(new { Message = "Habitación modificada exitosamente.", Habitacion = habitacion });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Permite a los agentes habilitar o inhabilitar una habitación
        /// </summary>
        [HttpPut("ActualizarEstado/{id}")]
        public async Task<IActionResult> ActualizarEstado(int id, [FromBody] bool estado)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var Rol = await _utilidadUsuario.ObtenerRolAsync(User);

                if (Rol != "Agente")
                    return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

                var existeHabitacion = _habitacionService.ObtenerDetalleHabitacion(id);

                if (existeHabitacion == null)
                    return NotFound(new { Message = $"La habitación '{id}' no existe." });

                _habitacionService.ActualizarEstadoHabitacion(id, estado);

                var habitacion = _habitacionService.ObtenerDetalleHabitacion(id);

                return Ok(new { Message = "Habitación modificada exitosamente.", Habitacion = habitacion });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
