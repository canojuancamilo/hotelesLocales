using apisHotel.Models;

namespace apisHotel.Interfaces
{
    public interface IHabitacionService
    {
        void AgregarHabitacionHotel(int HotelId, Habitacion hotel);
        Habitacion ObtenerDetalleHabitacion(int id);
        void ActualizarHabitacion(Habitacion hotel);
    }
}
