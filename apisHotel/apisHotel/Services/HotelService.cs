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

        public IEnumerable<Hotel> GetAllHoteles()
        {
            return _hotelRepository.GetAll();
        }

        public Hotel GetHotelById(int id)
        {
            return _hotelRepository.GetById(id);
        }

        public void CreateHotel(Hotel hotel)
        {
            _hotelRepository.Add(hotel);
        }

        public void UpdateHotel(Hotel hotel)
        {
            _hotelRepository.Update(hotel);
        }
    }
}
