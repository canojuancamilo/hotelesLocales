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
        public async Task <IActionResult> CrearHotel([FromBody] HotelModel model)
        {
            var Rol = await obtenerRol();

            if (Rol != "Agente")
                return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

            Hotel hotel = new Hotel()
            {
                Nombre = model.Nombre,
                Habilitado = model.Habilitado
            };

            _hotelService.CreateHotel(hotel);
            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.Id }, hotel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHoteles()
        {
            var Rol = await obtenerRol();

            if (Rol != "Agente")
                return Unauthorized(new { Mensaje = $"El rol '{Rol}' no puede acceder a esta información." });

            var hoteles = _hotelService.GetAllHoteles();
            return Ok(hoteles);
        }

        [HttpGet("{id}")]
        public IActionResult GetHotelById(int id)
        {
            var hotel = _hotelService.GetHotelById(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateHotel(int id, [FromBody] Hotel hotel)
        {
            var existingHotel = _hotelService.GetHotelById(id);

            if (existingHotel == null)
            {
                return NotFound();
            }

            _hotelService.UpdateHotel(hotel);

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
