using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace apisHotel.Models
{
    public class Cliente : IdentityUser
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string TelefonoContacto { get; set; }
    }
}
