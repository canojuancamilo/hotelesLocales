using apisHotel.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace apisHotel.Utilidades
{
    public class Usuario
    {
        private readonly UserManager<Cliente> _userManager;

        public Usuario(UserManager<Cliente> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> ObtenerRolAsync(ClaimsPrincipal user)
        {
            var userName = user.Identity.Name;
            var cliente = await _userManager.FindByNameAsync(userName);
            var rolesUsuario = await _userManager.GetRolesAsync(cliente);
            var rol = rolesUsuario.FirstOrDefault();

            return rol;
        }
    }
}
