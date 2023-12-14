using apisHotel.Interfaces;
using apisHotel.Models;
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

        [HttpGet]
        public async Task<IActionResult> GetAllHoteles()
        {
            var nombreCliente = User.Identity.Name;
            var cliente = await userManager.FindByNameAsync(nombreCliente);
            var rolesUsuario = await userManager.GetRolesAsync(cliente);
            var primerRol = rolesUsuario.FirstOrDefault();

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

        [HttpPost]
        public IActionResult CreateHotel([FromBody] Hotel hotel)
        {
            _hotelService.CreateHotel(hotel);
            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.Id }, hotel);
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
    }
}
