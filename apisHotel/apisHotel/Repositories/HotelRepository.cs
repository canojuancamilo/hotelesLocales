using apisHotel.Interfaces;
using apisHotel.Models;
using Microsoft.EntityFrameworkCore;
using System;

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

        public void ActualizarHotel(Hotel hotel)
        {
            _dbContext.Hoteles.Update(hotel);
            _dbContext.SaveChanges();
        }

        public void AgregarHabitacionHotel(int hotelId, Habitacion habitacion)
        {
            var hotel = _dbContext.Hoteles.FirstOrDefault(h => h.Id == hotelId);

            if (hotel != null)
            {
                hotel.Habitaciones.Add(habitacion);
                _dbContext.SaveChanges();
            }
        }

        public Habitacion ObtenerDetalleHabitacion(int id)
        {
            return _dbContext.Habitaciones.FirstOrDefault(h => h.Id == id);
        }
    }
}
