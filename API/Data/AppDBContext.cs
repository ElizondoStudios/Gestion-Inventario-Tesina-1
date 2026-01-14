using Microsoft.EntityFrameworkCore;
using API.Entities;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<PerfilPuesto> PerfilesPuesto { get; set; }
    public DbSet<Modulo> Modulos { get; set; }
    public DbSet<ModulosCategoria> ModulosCategorias { get; set; }
    public DbSet<NivelesAcceso> NivelesAcceso { get; set; }
    public DbSet<ModulosAcceso> ModulosAcceso { get; set; }
    public DbSet<Sucursal> Sucursales { get; set; }
    public DbSet<SucursalesInventario> SucursalesInventario { get; set; }
    public DbSet<Inventario> Inventario { get; set; }
    public DbSet<Unidad> Unidades { get; set; }
    public DbSet<LogInventario> LogInventario { get; set; }
    public DbSet<UsuarioSucursal> UsuariosSucursales { get; set; }
}