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
    public DbSet<TiposMovimientosInventario> TiposMovimientosInventario { get; set; }
    public DbSet<UsuarioSucursal> UsuariosSucursales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar la relación Usuario -> PerfilPuesto
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.PerfilPuesto)
            .WithMany(p => p.Usuarios)
            .HasForeignKey(u => u.IDPerfilPuesto);

        // Configurar la relación Modulo -> ModulosCategoria
        modelBuilder.Entity<Modulo>()
            .HasOne(m => m.ModuloCategoria)
            .WithMany(mc => mc.Modulos)
            .HasForeignKey(m => m.IDModuloCategoria);

        // Configurar las relaciones de ModulosAcceso
        modelBuilder.Entity<ModulosAcceso>()
            .HasOne(ma => ma.Modulo)
            .WithMany(m => m.ModulosAcceso)
            .HasForeignKey(ma => ma.IDModulo);

        modelBuilder.Entity<ModulosAcceso>()
            .HasOne(ma => ma.NivelAcceso)
            .WithMany(na => na.ModulosAcceso)
            .HasForeignKey(ma => ma.IDNivelAcceso);

        modelBuilder.Entity<ModulosAcceso>()
            .HasOne(ma => ma.PerfilPuesto)
            .WithMany(pp => pp.ModulosAcceso)
            .HasForeignKey(ma => ma.IDPerfilPuesto);

        // Configurar la relación Inventario -> Unidad
        modelBuilder.Entity<Inventario>()
            .HasOne(i => i.Unidad)
            .WithMany(u => u.Productos)
            .HasForeignKey(i => i.IDUnidad);

        // Configurar las relaciones de SucursalesInventario
        modelBuilder.Entity<SucursalesInventario>()
            .HasOne(si => si.Producto)
            .WithMany(i => i.SucursalesInventario)
            .HasForeignKey(si => si.NoParte);

        modelBuilder.Entity<SucursalesInventario>()
            .HasOne(si => si.Sucursal)
            .WithMany(s => s.SucursalesInventario)
            .HasForeignKey(si => si.IDSucursal);

        // Configurar las relaciones de UsuarioSucursal
        modelBuilder.Entity<UsuarioSucursal>()
            .HasOne(us => us.Usuario)
            .WithMany(u => u.UsuarioSucursales)
            .HasForeignKey(us => us.IDUsuario);

        modelBuilder.Entity<UsuarioSucursal>()
            .HasOne(us => us.Sucursal)
            .WithMany(s => s.UsuarioSucursales)
            .HasForeignKey(us => us.IDSucursal);

        // Configurar las relaciones de LogInventario
        modelBuilder.Entity<LogInventario>()
            .HasOne(li => li.QuienRealiza)
            .WithMany(u => u.LogInventario)
            .HasForeignKey(li => li.IDUsuario);

        modelBuilder.Entity<LogInventario>()
            .HasOne(li => li.Producto)
            .WithMany(i => i.LogInventario)
            .HasForeignKey(li => li.NoParte);

        modelBuilder.Entity<LogInventario>()
            .HasOne(li => li.Sucursal)
            .WithMany(s => s.LogInventario)
            .HasForeignKey(li => li.IDSucursal);

        modelBuilder.Entity<LogInventario>()
            .HasOne(li => li.TipoMovimiento)
            .WithMany(tm => tm.LogInventario)
            .HasForeignKey(li => li.IDTipoMovimiento);
    }
}