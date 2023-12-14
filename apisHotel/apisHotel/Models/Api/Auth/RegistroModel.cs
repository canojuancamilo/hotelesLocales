﻿using System.ComponentModel.DataAnnotations;

namespace apisHotel.Models.Api
{
    public class RegistroModel
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Nombres { get; set; }

        public string Apellidos { get; set; }
        
        [Required(ErrorMessage = "Campo obligatorio")]
        public DateTime FechaNacimiento { get; set; }

        public string NumeroDocumento { get; set; }
        
        [Required(ErrorMessage = "Campo obligatorio")]
        [EmailAddress(ErrorMessage = "Formato incorrecto")]
        public string Email { get; set; }

        public string TelefonoContacto { get; set; }

        public string Usuario { get; set; }
        
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Rol { get; set; }
    }
}
