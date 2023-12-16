using apisHotel.Interfaces;
using apisHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace apisHotel.Repositorys
{
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public HotelRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Hotel> ObtenerHoteles()
        {
            return _dbContext.Hoteles.Include(h => h.Habitaciones).ToList();
        }

        public Hotel ObtenerDetalleHotel(int id)
        {
            return _dbContext.Hoteles.Include(h => h.Habitaciones).FirstOrDefault(h => h.Id == id);
        }

        public void AgregarHotel(Hotel hotel)
        {
            _dbContext.Hoteles.Add(hotel);
            _dbContext.SaveChanges();
        }

        public void ActualizarHotel(Hotel model)
        {
            var hotel = _dbContext.Hoteles.FirstOrDefault(m => m.Id == model.Id);
            
            hotel.Nombre = model.Nombre;
            hotel.Habilitado = model.Habilitado;

            _dbContext.SaveChanges();
        }

        public void ActualizarEstadoHotel(int id, bool estado)
        {
            var hotel = _dbContext.Hoteles.FirstOrDefault(m => m.Id == id);
            hotel.Habilitado = estado;

            _dbContext.SaveChanges();
        }
    }
}
