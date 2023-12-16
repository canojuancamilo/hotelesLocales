using apisHotel.Interfaces;
using apisHotel.Models;
using apisHotel.Models.Api;
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

        /// <summary>
        /// Permite a los agentes registrar un hotel.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HotelModel model)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Permite a los agentes obtener todos los hoteles registrados.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var Rol = await _utilidadUsuario.ObtenerRolAsync(User);

                if (Rol != "Agente")
                    return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

                var hoteles = _hotelService.ObtenerHoteles();
                return Ok(hoteles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Permite a los agentes actualizar la información de un hotel.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] HotelModel model)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Permite a los agentes habilitar o inhabilitar un hotel
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

                var existeHotel = _hotelService.ObtenerDetalleHotel(id);

                if (existeHotel == null)
                {
                    return NotFound(new { Message = $"El hotel '{id}' no existe." });
                }

                _hotelService.ActualizarEstadoHotel(id, estado);

                var hotel = _hotelService.ObtenerDetalleHotel(id);

                return Ok(new { Message = "Hotel modificado exitosamente.", Hotel = hotel });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Permite a los viajeros obtener los hoteles con habitaciones disponibles.
        /// </summary>
        [HttpGet("Disponibles/{FechaEntrada}/{FechaSalida}/{CantidadPersonas}/{Ciudad}")]
        public async Task<IActionResult> ObenerHotelesDisponibles(string FechaEntrada, string FechaSalida, int CantidadPersonas, string Ciudad)
        {
            try
            {
                var Rol = await _utilidadUsuario.ObtenerRolAsync(User);

                if (Rol != "Viajero")
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}