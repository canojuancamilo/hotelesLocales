using apisHotel.Interfaces;
using apisHotel.Models;
using apisHotel.Models.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace apisHotel.Controller
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Cliente> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<Cliente> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistroModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cliente = new Cliente
            {
                Nombres = model.Nombres,
                Apellidos = model.Apellidos,
                FechaNacimiento = model.FechaNacimiento,
                NumeroDocumento = model.NumeroDocumento,
                Email = model.Email,
                TelefonoContacto = model.TelefonoContacto
            };

            var result = await _userManager.CreateAsync(cliente, model.Contrasena);

            if (result.Succeeded)
            {
                // Asignar el rol al nuevo cliente
                await _userManager.AddToRoleAsync(cliente, model.Rol);
                var token = GenerateJwtToken(cliente);
                // Puedes devolver una respuesta de éxito o cualquier otra información necesaria
                return Ok(new { Message = "Usuario registrado exitosamente.", Token = token });
            }

            // Si hay errores en la creación del usuario, devolver los errores
            return BadRequest(new { Errors = result.Errors });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cliente = await _userManager.FindByNameAsync(model.UserName);

            if (cliente != null && await _userManager.CheckPasswordAsync(cliente, model.Contrasena))
            {
                var token = GenerateJwtToken(cliente);

                return Ok(new{Token = token});
            }

            return Unauthorized(new { Mensaje = "Acceso no autorizado." });
        }

        private string GenerateJwtToken(Cliente cliente)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, cliente.Id.ToString()),
                new Claim(ClaimTypes.Name, cliente.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
