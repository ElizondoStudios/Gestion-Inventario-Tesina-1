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

  public async Task<IReadOnlyList<ModulosAcceso>> ObtenerModulosAccesoPorPerfilPuesto(int IDPerfilPuesto)
  {
    return await context.ModulosAcceso
      .Include(ma => ma.Modulo)
        .ThenInclude(m => m.ModuloCategoria)
      .Include(ma => ma.NivelAcceso)
      .Include(ma => ma.PerfilPuesto)
      .Where(ma => ma.PerfilPuesto.IDPerfilPuesto == IDPerfilPuesto)
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

  public async Task<bool> CrearModuloAcceso(ModulosAcceso moduloAcceso)
  {
    await context.ModulosAcceso.AddAsync(moduloAcceso);
    return await context.SaveChangesAsync() > 0;
  }

  public async Task<bool> EliminarModuloAcceso(int IDModuloAcceso)
  {
    var filas = await context.ModulosAcceso
      .Where(ma => ma.IDModuloAcceso == IDModuloAcceso)
      .ExecuteDeleteAsync();

    return filas > 0;
  }
}
