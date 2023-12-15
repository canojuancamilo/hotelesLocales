using apisHotel.Interfaces;
using apisHotel.Models;

namespace apisHotel.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
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

        public void AgregarHabitacionHotel(int HotelId, Habitacion habitacion)
        {
            _hotelRepository.AgregarHabitacionHotel(HotelId, habitacion);
        }

        public Habitacion ObtenerDetalleHabitacion(int id)
        {
            return _hotelRepository.ObtenerDetalleHabitacion(id);
        }

        public void ActualizarHabitacion(Habitacion habitacion)
        {
            _hotelRepository.ActualizarHabitacion(habitacion);
        }
    }
}
