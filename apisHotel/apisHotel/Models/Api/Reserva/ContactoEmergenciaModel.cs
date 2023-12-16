using System.ComponentModel.DataAnnotations;

namespace apisHotel.Models
{
    public class ContactoEmergenciaModel
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string TelefonoContacto { get; set; }
    }
}
