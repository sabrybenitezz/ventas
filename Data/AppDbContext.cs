using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcesionariaVehiculos.Models;
using Microsoft.EntityFrameworkCore; 
namespace ConcesionariaVehiculos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<ServicioPosVenta> ServiciosPosventa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Ventas)
                .WithOne(v => v.Cliente)
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.ServiciosPosventa)
                .WithOne(s => s.Cliente)
                .HasForeignKey(s => s.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vehiculo>()
                .HasMany(v => v.Ventas)
                .WithOne(vt => vt.Vehiculo)
                .HasForeignKey(vt => vt.VehiculoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
