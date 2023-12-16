using apisHotel.Interfaces;
using apisHotel.Models;

namespace apisHotel.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IHotelService _hotelService;
        private readonly IHabitacionService _habitacioService;

        public ReservaService(IReservaRepository reservaRepository, IHotelService hotelService, IHabitacionService habitacioService)
        {
            _reservaRepository = reservaRepository;
            _hotelService = hotelService;
            _habitacioService = habitacioService;
        }

        public void AgregarReservaHabitacion(Reserva reserva)
        {
            var habitacion = _habitacioService.ObtenerDetalleHabitacion(reserva.HabitacionId);
            var hotelesHabitaDisponibles = _hotelService.ObtenerHotelesDisponibles(reserva.FechaEntrada, reserva.FechaSalida, reserva.CantidadPersonas, habitacion.Ubicacion);
            var HabitacionDisponible = hotelesHabitaDisponibles?.Where(m => m.Habitaciones.Any(h => h.Id == reserva.HabitacionId))?.ToList();

            if (HabitacionDisponible == null || HabitacionDisponible.Count == 0)
                throw new InvalidOperationException($"La habitación {habitacion.Id} no esta disponible con los parametros ingresados.");

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
