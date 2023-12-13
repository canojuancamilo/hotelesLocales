namespace apisHotel.Models
{    public class Reserva
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public int HabitacionId { get; set; }
        public Habitacion Habitacion { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public int CantidadPersonas { get; set; }
        public List<Huesped> Huespedes { get; set; }
        public ContactoEmergencia ContactoEmergencia { get; set; }
    }
}