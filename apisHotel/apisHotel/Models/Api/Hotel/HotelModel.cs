using System.ComponentModel.DataAnnotations;

namespace apisHotel.Models.Api
{
    public class HotelModel
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public bool Habilitado { get; set; }
    }
}