using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace apisHotel.Models
{
    public class HuespedModel
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        public string NombresApellidos { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string NumeroDocumento { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [EmailAddress(ErrorMessage = "Formato incorrecto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string TelefonoContacto { get; set; }
    }
}
