namespace apisHotel.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Habitacion> Habitaciones { get; set; }
        public bool Habilitado { get; set; }
    }
}
