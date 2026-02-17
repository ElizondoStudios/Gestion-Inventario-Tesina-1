using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class InventarioRepository(AppDbContext context) : IInventarioRepository
{
  public async Task<IReadOnlyList<Inventario>> ObtenerInventario()
  {
    return await context.Inventario
      .Include(i => i.Unidad)
      .Where(i => i.Unidad.Activo)
      .ToListAsync();
  }

  public async Task<Inventario?> ObtenerProducto(string NoParte)
  {
    return await context.Inventario
      .Include(i => i.Unidad)
      .Where(i => i.Unidad.Activo)
      .FirstOrDefaultAsync(i => i.NoParte == NoParte);
  }

  public async Task<bool> CrearProducto(Inventario producto)
  {
    await context.Inventario.AddAsync(producto);
    return await context.SaveChangesAsync() > 0;
  }

  public async Task<bool> ActualizarProducto(Inventario producto)
  {
    var filas = await context.Inventario
      .Where(i => i.NoParte == producto.NoParte)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(i => i.NombreProducto, producto.NombreProducto)
        .SetProperty(i => i.DescripcionProducto, producto.DescripcionProducto)
        .SetProperty(i => i.Precio, producto.Precio)
        .SetProperty(i => i.Costo, producto.Costo)
      );

    return filas > 0;
  }

  public async Task<bool> InhabilitarProducto(string NoParte)
  {
    var filas = await context.Inventario
      .Where(i => i.NoParte == NoParte)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(i => i.Activo, false)
      );

    return filas > 0;
  }

  public async Task<bool> HabilitarProducto(string NoParte)
  {
    var filas = await context.Inventario
      .Where(i => i.NoParte == NoParte)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(i => i.Activo, true)
      );

    return filas > 0;
  }
}
