using apisHotel.Models;

namespace apisHotel.Interfaces
{
    public interface IHotelService
    {
        IEnumerable<Hotel> GetAllHoteles();
        Hotel GetHotelById(int id);
        void CreateHotel(Hotel hotel);
        void UpdateHotel(Hotel hotel);
    }
}
