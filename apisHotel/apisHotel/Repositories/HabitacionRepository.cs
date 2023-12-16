using apisHotel.Interfaces;
using apisHotel.Models;

namespace apisHotel.Repositorys
{
    public class HabitacionRepository : IHabitacionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public HabitacionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public void ActualizarHabitacion(Habitacion habitacion)
        {
            var Habitacion = _dbContext.Habitaciones.Find(habitacion.Id);
            _dbContext.Entry(Habitacion).CurrentValues.SetValues(habitacion);
            _dbContext.SaveChanges();
        }

        public void ActualizarEstadoHabitacion(int id, bool estado)
        {
            var Habitacion = _dbContext.Habitaciones.FirstOrDefault(m => m.Id == id);
            Habitacion.Habilitada = estado;

            _dbContext.SaveChanges();
        }
    }
}
