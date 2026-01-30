using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class ModuloRepository(AppDbContext context) : IModuloRepository
{
  public async Task<IReadOnlyList<Modulo>> ObtenerModulos()
  {
    return await context.Modulos
      .Include(m => m.ModuloCategoria)
      .ToListAsync();
  }

  public async Task<Modulo?> ObtenerModulo(int IDModulo)
  {
    return await context.Modulos
      .Include(m => m.ModuloCategoria)
      .FirstOrDefaultAsync(m => m.IDModulo == IDModulo);
  }

  public async Task<bool> CrearModulo(Modulo modulo)
  {
    await context.Modulos.AddAsync(modulo);
    return await context.SaveChangesAsync() > 0;
  }

  public async Task<bool> ActualizarModulo(Modulo modulo)
  {
    var filas = await context.Modulos
      .Where(m => m.IDModulo == modulo.IDModulo)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(m => m.Nombre, modulo.Nombre)
        .SetProperty(m => m.Icono, modulo.Icono)
      );

    return filas > 0;
  }

  public async Task<bool> InhabilitarModulo(int IDModulo)
  {
    var filas = await context.Modulos
      .Where(m => m.IDModulo == IDModulo)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(m => m.Activo, false)
      );

    return filas > 0;
  }

  public async Task<bool> HabilitarModulo(int IDModulo)
  {
    var filas = await context.Modulos
      .Where(m => m.IDModulo == IDModulo)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(m => m.Activo, true)
      );

    return filas > 0;
  }
}
