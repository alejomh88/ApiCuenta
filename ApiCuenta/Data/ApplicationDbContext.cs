using ApiCuenta.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ApiCuenta.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //Agregar modelos aquí
        public DbSet<Cuenta> Cuenta { get; set; }
        public DbSet<Movimiento> Movimimento { get; set; }

        public DbSet<Reporte> Reporte { get; set; }

    }
}
