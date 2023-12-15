using apisHotel.Models;

namespace apisHotel.Interfaces
{
    public interface IHotelRepository
    {
        IEnumerable<Hotel> ObtenerHoteles();
        Hotel ObtenerDetalleHotel(int id);
        void AgregarHotel(Hotel hotel);
        void ActualizarHotel(Hotel hotel);
    }
}
