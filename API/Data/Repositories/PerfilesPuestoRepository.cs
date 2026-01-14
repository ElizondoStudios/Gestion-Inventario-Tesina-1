using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace API.Repositories;

public class PerfilesPuestoRepository(AppDbContext context): IPerfilesPuestoRepository 
{
  public async Task<IReadOnlyList<PerfilPuesto>> ObtenerPerfilesPuesto()
  {
    return await context.PerfilesPuesto.ToListAsync();
  }

  public async Task<PerfilPuesto?> ObtenerPerfilPuesto(int IDPerfilPuesto)
  {
    return await context.PerfilesPuesto.FindAsync(IDPerfilPuesto);
  }

  public async Task<bool> CrearPerfilPuesto(PerfilPuesto perfilPuesto)
  {
    await context.PerfilesPuesto.AddAsync(perfilPuesto);
    return await context.SaveChangesAsync() > 0;
  }

  public async Task<bool> ActualizarPerfilPuesto(PerfilPuesto perfilPuesto)
  {
    var filas = await context.PerfilesPuesto
    .Where(u => u.IDPerfilPuesto == perfilPuesto.IDPerfilPuesto)
    .ExecuteUpdateAsync(setters => setters
      .SetProperty(u => u.Descripcion, perfilPuesto.Descripcion)
    );

    return filas > 0;
  }

  public async Task<bool> InhabilitarPerfilPuesto(int IDPerfilPuesto)
  {
    var filas = await context.PerfilesPuesto
    .Where(u => u.IDPerfilPuesto == IDPerfilPuesto && u.Activo)
    .ExecuteUpdateAsync(setters => setters
      .SetProperty(u => u.Activo, false)
    );

    return filas > 0;
  }
  
  public async Task<bool> HabilitarPerfilPuesto(int IDPerfilPuesto)
  {
    var filas = await context.PerfilesPuesto
    .Where(u => u.IDPerfilPuesto == IDPerfilPuesto && u.Activo)
    .ExecuteUpdateAsync(setters => setters
      .SetProperty(u => u.Activo, true)
    );

    return filas > 0;
  }
}