using System.ComponentModel.DataAnnotations;

namespace apisHotel.Models
{    public class ReservaModel
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        public string FechaEntrada { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string FechaSalida { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public int CantidadPersonas { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public List<HuespedModel> Huespedes { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public ContactoEmergenciaModel ContactoEmergencia { get; set; }
    }
}