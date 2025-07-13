using Microsoft.EntityFrameworkCore;
using Backend.Api.Models;

namespace Backend.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<UsuarioPermiso> UsuarioPermisos { get; set; }
        public DbSet<Articulo> Articulos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación UsuarioPermiso -> Usuario
            modelBuilder.Entity<UsuarioPermiso>()
                .HasOne(up => up.Usuario)
                .WithMany(u => u.UsuarioPermisos)
                .HasForeignKey(up => up.UsuarioId);

            // Relación UsuarioPermiso -> Permiso
            modelBuilder.Entity<UsuarioPermiso>()
                .HasOne(up => up.Permiso)
                .WithMany(p => p.UsuarioPermisos)
                .HasForeignKey(up => up.PermisoId);

            // Seed de permisos default (ahora con Id)
            modelBuilder.Entity<Permiso>().HasData(
                new Permiso { Id = 1, Nombre = "admin", Descripcion = "Administrador total" },
                new Permiso { Id = 2, Nombre = "editor", Descripcion = "Puede editar artículos" },
                new Permiso { Id = 3, Nombre = "visualizador", Descripcion = "Solo puede ver información" },
                new Permiso { Id = 4, Nombre = "sin acceso", Descripcion = "Sin acceso al sistema" }
            );
        }
    }
}
