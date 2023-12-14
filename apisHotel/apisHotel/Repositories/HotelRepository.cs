using apisHotel.Interfaces;
using apisHotel.Models;
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

        public IEnumerable<Hotel> GetAll()
        {
            return _dbContext.Hoteles.ToList();
        }

        public Hotel GetById(int id)
        {
            return _dbContext.Hoteles.FirstOrDefault(h => h.Id == id);
        }

        public void Add(Hotel hotel)
        {
            _dbContext.Hoteles.Add(hotel);
            _dbContext.SaveChanges();
        }

        public void Update(Hotel hotel)
        {
            _dbContext.Hoteles.Update(hotel);
            _dbContext.SaveChanges();
        }
    }
}
