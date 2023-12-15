using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace apisHotel.Models
{
    public class Huesped
    {
        public int Id { get; set; }
        public string NombresApellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Email { get; set; }
        public string TelefonoContacto { get; set; }
    }
}
