using System.Text.Json.Serialization;

namespace apisHotel.Models
{
    public class Habitacion
    {
        public int Id { get; set; }

        public string Tipo { get; set; }

        public decimal CostoBase { get; set; }

        public decimal Impuestos { get; set; }

        public string Ubicacion { get; set; }

        public bool Habilitada { get; set; }
    }
}