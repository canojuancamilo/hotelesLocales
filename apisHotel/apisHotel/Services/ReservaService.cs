using apisHotel.Interfaces;
using apisHotel.Models;
using apisHotel.Repositorys;

namespace apisHotel.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;

        public ReservaService(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public void AgregarReservaHabitacion(Reserva reserva)
        {
            _reservaRepository.AgregarReservaHabitacion(reserva);
        }

        public Reserva ObtenerDetalleReserva(int id)
        {
          return _reservaRepository.ObtenerDetalleReserva(id);
        }

        public IEnumerable<Reserva> ObtenerReservas()
        {
            return _reservaRepository.ObtenerReservas();
        }
    }
}
