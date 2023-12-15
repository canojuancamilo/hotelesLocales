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

        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    var hotel = _hotelService.ObtenerDetalleHotel(id);

        //    if (hotel == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(hotel);
        //}

        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] Hotel hotel)
        //{
        //    var existeHotel = _hotelService.ObtenerDetalleHotel(id);

        //    if (existeHotel == null)
        //    {
        //        return NotFound();
        //    }

        //    _hotelService.ActualizarHotel(hotel);

        //    return NoContent();
        //}
    }
}
