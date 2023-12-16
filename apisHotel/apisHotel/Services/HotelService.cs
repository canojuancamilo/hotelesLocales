using apisHotel.Interfaces;
using apisHotel.Models;

namespace apisHotel.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IReservaRepository _reservaRepository;

        public HotelService(IHotelRepository hotelRepository, IReservaRepository reservaRepository)
        {
            _hotelRepository = hotelRepository;
            _reservaRepository = reservaRepository;
        }

        public IEnumerable<Hotel> ObtenerHoteles()
        {
            return _hotelRepository.ObtenerHoteles();
        }

        public Hotel ObtenerDetalleHotel(int id)
        {
            return _hotelRepository.ObtenerDetalleHotel(id);
        }

        public void AgregarHotel(Hotel hotel)
        {
            _hotelRepository.AgregarHotel(hotel);
        }

        public void ActualizarHotel(Hotel hotel)
        {
            _hotelRepository.ActualizarHotel(hotel);
        }

        public void ActualizarEstadoHotel(int id, bool estado)
        {
            _hotelRepository.ActualizarEstadoHotel(id, estado);
        }

        public IEnumerable<Hotel> ObtenerHotelesDisponibles(DateTime fechaEntrada, DateTime fechaSalida, int cantidaPersonas, string ciudad)
        {
            // Asegurarse de que los repositorios no sean nulos
            var hoteles = _hotelRepository.ObtenerHoteles().Where(m => m.Habilitado && m.Habitaciones.Any(h => h.Habilitada))
                            .Select(h => new Hotel
                            {
                                Id = h.Id,
                                Nombre = h.Nombre,
                                Habilitado = h.Habilitado,
                                Habitaciones = h.Habitaciones.Where(hab => hab.Habilitada).ToList()
                            }).ToList();

            var reservas = _reservaRepository.ObtenerReservas()
                .Where(m =>
                    (m.FechaEntrada >= fechaEntrada && m.FechaEntrada <= fechaSalida) ||
                    (m.FechaSalida >= fechaEntrada && m.FechaSalida <= fechaSalida) ||
                    (m.FechaEntrada <= fechaEntrada && m.FechaSalida >= fechaSalida))?.ToList();

            // Filtrar por hoteles con habitaciones en la ciudad y con cantidad de personas suficiente
            hoteles = hoteles?
                .Where(m => m.Habitaciones?
                        .Any(h => h.Ubicacion == ciudad && h.CantidadPersonas >= cantidaPersonas && h.Habilitada) == true)
                .ToList();

            // Filtrar por habitaciones que no tienen reservas
            hoteles = hoteles?
                .Where(m => m.Habitaciones?
                        .Any(h => reservas.All(r => r.HabitacionId != h.Id)) == true)
                .ToList();


            return hoteles;
        }
    }
}
