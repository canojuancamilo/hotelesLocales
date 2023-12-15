using apisHotel.Interfaces;
using apisHotel.Models;
using apisHotel.Models.Api;
using apisHotel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace apisHotel.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly UserManager<Cliente> userManager;

        public HotelController(IHotelService hotelService, UserManager<Cliente> userManager)
        {
            _hotelService = hotelService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HotelModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Rol = await obtenerRol();

            if (Rol != "Agente")
                return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

            Hotel hotel = new Hotel()
            {
                Nombre = model.Nombre,
                Habilitado = model.Habilitado
            };

            _hotelService.AgregarHotel(hotel);
            return CreatedAtAction(nameof(GetDetalle), new { id = hotel.Id }, hotel);
        }

        #region Habitacion
        [HttpPost("Habiticion/{HotelId}")]
        public async Task<IActionResult> Post(int HotelId, [FromBody] HabitacionModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Rol = await obtenerRol();

            if (Rol != "Agente")
                return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

            bool hotelExiste = _hotelService.ObtenerDetalleHotel(HotelId) != null;

            if (!hotelExiste)
            {
                ModelState.AddModelError("HotelId", $"El hotel '{HotelId}' no existe.");
                return BadRequest(ModelState);
            }

            Habitacion habitacion = new Habitacion()
            {
                Tipo = model.Tipo,
                CostoBase = model.CostoBase,
                Impuestos = model.Impuestos,
                Ubicacion = model.Ubicacion,
                Habilitada = model.Habilitada
            };

            _hotelService.AgregarHabitacionHotel(HotelId, habitacion);
            return CreatedAtAction(nameof(GetDetalleHabitacion), new { id = habitacion.Id }, habitacion);
        }

        [HttpGet("Habitacion/{id}")]
        public IActionResult GetDetalleHabitacion(int id)
        {
            var habitacion = _hotelService.ObtenerDetalleHabitacion(id);

            if (habitacion == null)
            {
                return NotFound();
            }

            return Ok(habitacion);
        }

        [HttpPut("Habitacion/{idHabitacion}")]
        public async Task<IActionResult> PutHabitacion(int idHabitacion, [FromBody] HabitacionModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Rol = await obtenerRol();

            if (Rol != "Agente")
                return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

            var existeHabitacion = _hotelService.ObtenerDetalleHabitacion(idHabitacion);

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
                Habilitada = model.Habilitada
            };

            _hotelService.ActualizarHabitacion(habitacion);

            return Ok(new { Message = "Habitacio modificada exitosamente.", Habitacion = habitacion });
        }

        #endregion Habitacion

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Rol = await obtenerRol();

            if (Rol != "Agente")
                return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

            var hoteles = _hotelService.ObtenerHoteles();
            return Ok(hoteles);
        }

        [HttpGet("{id}")]
        public IActionResult GetDetalle(int id)
        {
            var hotel = _hotelService.ObtenerDetalleHotel(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Hotel hotel)
        {
            var existeHotel = _hotelService.ObtenerDetalleHotel(id);

            if (existeHotel == null)
            {
                return NotFound();
            }

            _hotelService.ActualizarHotel(hotel);

            return NoContent();
        }

        private async Task<string> obtenerRol()
        {
            var UsuarioCliente = User.Identity.Name;
            var cliente = await userManager.FindByNameAsync(UsuarioCliente);
            var rolesUsuario = await userManager.GetRolesAsync(cliente);
            var Rol = rolesUsuario.FirstOrDefault();

            return Rol;
        }
    }
}
