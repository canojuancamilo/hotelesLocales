using apisHotel.Interfaces;
using apisHotel.Models;
using apisHotel.Models.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
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
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(
            UserManager<Cliente> userManager,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Obtiene todos los Roles, esto nos sirve al momento de registrar un cliente.
        /// </summary>
        [HttpGet("Roles")]
        public async Task<IActionResult> Roles()
        {
            try
            {
                var roles = _roleManager.Roles.ToList();

                if (roles.Count == 0)
                {
                    return NoContent();
                }

                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Registra el cliente con el rol, para realizar validaciones de roles por medio del token en las demás Apis.
        /// </summary>
        [HttpPost("RegistrarCliente")]
        public async Task<IActionResult> RegistrarCliente([FromBody] RegistroModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                DateTime fechaNacimiento;

                if (!(DateTime.TryParseExact(model.FechaNacimiento, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaNacimiento)))
                    return BadRequest("Formato de fecha de entrada no válido. Utiliza el formato dd-MM-yyyy.");

                if (await _userManager.FindByEmailAsync(model.Email) != null)
                {
                    ModelState.AddModelError("Email", $"El correo '{model.Email}' ya se encuentra registrado.");
                    return BadRequest(ModelState);
                }

                var roleExists = await _roleManager.RoleExistsAsync(model.Rol);

                if (!roleExists)
                {
                    ModelState.AddModelError("Rol", $"El rol '{model.Rol}' no existe.");
                    return BadRequest(ModelState);
                }

                var cliente = new Cliente
                {
                    Nombres = model.Nombres,
                    Apellidos = model.Apellidos,
                    FechaNacimiento = fechaNacimiento,
                    NumeroDocumento = model.NumeroDocumento,
                    Email = model.Email,
                    TelefonoContacto = model.TelefonoContacto,
                    UserName = model.Usuario,
                    Genero = model.Genero,
                    TipoDocumento = model.TipoDocumento,
                };

                var result = await _userManager.CreateAsync(cliente, model.Contrasena);

                if (!result.Succeeded)
                    // Si hay errores en la creación del usuario, devolver los errores
                    return BadRequest(new { Errors = result.Errors });

                try
                {
                    await _userManager.AddToRoleAsync(cliente, model.Rol);
                }
                catch (Exception ex)
                {
                    await _userManager.DeleteAsync(cliente);
                    return BadRequest(new { Errors = ex.Message });
                }

                //Generamos el token y lo devolvemos en la respuesta.
                var token = GenerateJwtToken(cliente);
                return Ok(new { Message = "Usuario registrado exitosamente.", Token = $"Bearer {token}" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el token Bearer de validación para las demás Apis.
        /// </summary>
        [HttpPost("ObtenerToken")]
        public async Task<IActionResult> ObtenerToken([FromBody] LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var cliente = await _userManager.FindByNameAsync(model.Usuario);

                if (cliente != null && await _userManager.CheckPasswordAsync(cliente, model.Contrasena))
                {
                    var token = GenerateJwtToken(cliente);

                    return Ok(new { Token = $"Bearer {token}" });
                }

                return Unauthorized(new { Mensaje = "Acceso no autorizado." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
