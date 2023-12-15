using apisHotel.Models;

namespace apisHotel.Interfaces
{
    public interface IReservaService
    {
        void AgregarReservaHabitacion(Reserva reserva);
        Reserva ObtenerDetalleReserva(int id);
        IEnumerable<Reserva> ObtenerReservas();
    }
}
