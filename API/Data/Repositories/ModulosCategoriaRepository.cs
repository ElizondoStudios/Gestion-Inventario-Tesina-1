using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class ModulosCategoriaRepository(AppDbContext context) : IModulosCategoriaRepository
{
  public async Task<IReadOnlyList<ModulosCategoria>> ObtenerModulosCategorias()
  {
    return await context.ModulosCategorias
      .Include(mc => mc.Modulos)
      .ToListAsync();
  }

  public async Task<ModulosCategoria?> ObtenerModuloCategoria(int IDModuloCategoria)
  {
    return await context.ModulosCategorias
      .Include(mc => mc.Modulos)
      .FirstOrDefaultAsync(mc => mc.IDModuloCategoria == IDModuloCategoria);
  }

  public async Task<bool> CrearModuloCategoria(ModulosCategoria moduloCategoria)
  {
    await context.ModulosCategorias.AddAsync(moduloCategoria);
    return await context.SaveChangesAsync() > 0;
  }

  public async Task<bool> ActualizarModuloCategoria(ModulosCategoria moduloCategoria)
  {
    var filas = await context.ModulosCategorias
      .Where(mc => mc.IDModuloCategoria == moduloCategoria.IDModuloCategoria)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(mc => mc.Nombre, moduloCategoria.Nombre)
        .SetProperty(mc => mc.Icono, moduloCategoria.Icono)
      );

    return filas > 0;
  }

  public async Task<bool> InhabilitarModuloCategoria(int IDModuloCategoria)
  {
    var filas = await context.ModulosCategorias
      .Where(mc => mc.IDModuloCategoria == IDModuloCategoria && mc.Activo)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(mc => mc.Activo, false)
      );

    return filas > 0;
  }

  public async Task<bool> HabilitarModuloCategoria(int IDModuloCategoria)
  {
    var filas = await context.ModulosCategorias
      .Where(mc => mc.IDModuloCategoria == IDModuloCategoria && !mc.Activo)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(mc => mc.Activo, true)
      );

    return filas > 0;
  }
}
