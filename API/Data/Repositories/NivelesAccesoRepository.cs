using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class NivelesAccesoRepository(AppDbContext context) : INivelesAccesoRepository
{
  public async Task<IReadOnlyList<NivelesAcceso>> ObtenerNivelesAcceso()
  {
    return await context.NivelesAcceso.ToListAsync();
  }

  public async Task<NivelesAcceso?> ObtenerNivelAcceso(int NivelAcceso)
  {
    return await context.NivelesAcceso.FindAsync(NivelAcceso);
  }

  public async Task<bool> CrearNivelAcceso(NivelesAcceso nivelAcceso)
  {
    await context.NivelesAcceso.AddAsync(nivelAcceso);
    return await context.SaveChangesAsync() > 0;
  }

  public async Task<bool> ActualizarNivelAcceso(NivelesAcceso nivelAcceso)
  {
    var filas = await context.NivelesAcceso
      .Where(na => na.NivelAcceso == nivelAcceso.NivelAcceso)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(na => na.Descripcion, nivelAcceso.Descripcion)
      );

    return filas > 0;
  }
}
