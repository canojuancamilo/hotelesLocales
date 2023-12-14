using apisHotel.Models;

namespace apisHotel.Interfaces
{
    public interface IHotelRepository
    {
        IEnumerable<Hotel> GetAll();
        Hotel GetById(int id);
        void Add(Hotel hotel);
        void Update(Hotel hotel);
    }
}
