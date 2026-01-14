using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class UnidadRepository(AppDbContext context) : IUnidadRepository
{
  public async Task<IReadOnlyList<Unidad>> ObtenerUnidades()
  {
    return await context.Unidades.ToListAsync();
  }

  public async Task<Unidad?> ObtenerUnidad(int IDUnidad)
  {
    return await context.Unidades.FindAsync(IDUnidad);
  }

  public async Task<bool> CrearUnidad(Unidad unidad)
  {
    await context.Unidades.AddAsync(unidad);
    return await context.SaveChangesAsync() > 0;
  }

  public async Task<bool> ActualizarUnidad(Unidad unidad)
  {
    var filas = await context.Unidades
      .Where(u => u.IDUnidad == unidad.IDUnidad)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(u => u.Descripcion, unidad.Descripcion)
        .SetProperty(u => u.Abreviacion, unidad.Abreviacion)
      );

    return filas > 0;
  }

  public async Task<bool> InhabilitarUnidad(int IDUnidad)
  {
    var filas = await context.Unidades
      .Where(u => u.IDUnidad == IDUnidad && u.Activo)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(u => u.Activo, false)
      );

    return filas > 0;
  }

  public async Task<bool> HabilitarUnidad(int IDUnidad)
  {
    var filas = await context.Unidades
      .Where(u => u.IDUnidad == IDUnidad && !u.Activo)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(u => u.Activo, true)
      );

    return filas > 0;
  }
}
