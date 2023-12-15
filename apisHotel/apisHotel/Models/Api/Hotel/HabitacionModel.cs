using System.ComponentModel.DataAnnotations;

namespace apisHotel.Models
{
    public class HabitacionModel
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public decimal CostoBase { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public decimal Impuestos { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Ubicacion { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public bool Habilitada { get; set; }
    }
}