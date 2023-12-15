using apisHotel.Interfaces;
using apisHotel.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace apisHotel.Repositorys
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ReservaRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Reserva> ObtenerReservas()
        {
            return _dbContext.Reservas.ToList();
        }

        public Reserva ObtenerDetalleReserva(int id)
        {
            return _dbContext.Reservas.FirstOrDefault(m => m.Id == id);
        }

        public void AgregarReservaHabitacion(Reserva reserva)
        {
            _dbContext.Reservas.Add(reserva);
            _dbContext.SaveChanges();
        }
    }
}
