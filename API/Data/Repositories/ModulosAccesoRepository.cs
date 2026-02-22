using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class ModulosAccesoRepository(AppDbContext context) : IModulosAccesoRepository
{
  public async Task<IReadOnlyList<ModulosAcceso>> ObtenerModulosAcceso()
  {
    return await context.ModulosAcceso
      .Include(ma => ma.Modulo)
        .ThenInclude(m => m.ModuloCategoria)
      .Include(ma => ma.NivelAcceso)
      .Include(ma => ma.PerfilPuesto)
      .ToListAsync();
  }

  public async Task<ModulosAcceso?> ObtenerModuloAcceso(int IDModuloAcceso)
  {
    return await context.ModulosAcceso
      .Include(ma => ma.Modulo)
        .ThenInclude(m => m.ModuloCategoria)
      .Include(ma => ma.NivelAcceso)
      .Include(ma => ma.PerfilPuesto)
      .FirstOrDefaultAsync(ma => ma.IDModuloAcceso == IDModuloAcceso);
  }

  public async Task<ModulosAcceso?> ValidarAccesoModulo(int IDUsuario, int IDModulo)
  {
    var usuario = await context.Usuarios.FirstOrDefaultAsync(u => u.IDUsuario == IDUsuario) ?? throw new Exception("No se encontró el usuario");
    return await context.ModulosAcceso
      .Include(ma => ma.Modulo)
        .ThenInclude(m => m.ModuloCategoria)
      .Include(ma => ma.NivelAcceso)
      .Include(ma => ma.PerfilPuesto)
      .FirstOrDefaultAsync(ma => ma.IDPerfilPuesto == usuario.IDPerfilPuesto && ma.IDModulo == IDModulo);
  }

  public async Task<ModulosAcceso?> RegistrarAccesoModulo(ModulosAcceso modulosAcceso)
  {
    context.ModulosAcceso.Add(modulosAcceso);
    await context.SaveChangesAsync();
    
    return await ObtenerModuloAcceso(modulosAcceso.IDModuloAcceso);
  }

  public async Task<bool> EliminarAccesoModulo(int IDModuloAcceso)
  {
    var moduloAcceso = await context.ModulosAcceso.FindAsync(IDModuloAcceso);
    
    if (moduloAcceso == null)
      return false;
    
    context.ModulosAcceso.Remove(moduloAcceso);
    await context.SaveChangesAsync();
    
    return true;
  }

  public async Task<IReadOnlyList<Modulo>> ObtenerModulos()
  {
    return await context.Modulos.Where(m => m.Activo).ToListAsync();
  }
  public async Task<IReadOnlyList<NivelesAcceso>> ObtenerNiveles()
  {
    return await context.NivelesAcceso.ToListAsync();
  }
}
