using apisHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace apisHotel
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Hotel> Hoteles { get; set; }
        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Huesped> Huespedes { get; set; }
        public DbSet<ContactoEmergencia> ContactosEmergencia { get; set; }
    }
}
