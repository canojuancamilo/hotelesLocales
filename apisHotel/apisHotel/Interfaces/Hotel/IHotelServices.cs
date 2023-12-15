using apisHotel.Models;

namespace apisHotel.Interfaces
{
    public interface IHotelService
    {
        IEnumerable<Hotel> ObtenerHoteles();
        Hotel ObtenerDetalleHotel(int id);
        void AgregarHotel(Hotel hotel);
        void ActualizarHotel(Hotel hotel);
        void ActualizarEstadoHotel(int id, bool estado);
    }
}
