using apisHotel.Models;

namespace apisHotel.Interfaces
{
    public interface IReservaRepository
    {
        void AgregarReservaHabitacion(Reserva reserva);
        Reserva ObtenerDetalleReserva(int id);
        IEnumerable<Reserva> ObtenerReservas();
    }
}
