using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class SucursalesInventarioRepository(AppDbContext context) : ISucursalesInventarioRepository
{
  public async Task<IReadOnlyList<SucursalesInventario>> ObtenerSucursalesInventario()
  {
    return await context.SucursalesInventario
      .Include(si => si.Producto)
        .ThenInclude(p => p.Unidad)
      .Include(si => si.Sucursal)
      .ToListAsync();
  }

  public async Task<IReadOnlyList<SucursalesInventario>> ObtenerInventarioPorSucursal(int IDSucursal)
  {
    return await context.SucursalesInventario
      .Include(si => si.Producto)
        .ThenInclude(p => p.Unidad)
      .Include(si => si.Sucursal)
      .Where(si => si.Sucursal.IDSucursal == IDSucursal)
      .ToListAsync();
  }

  public async Task<SucursalesInventario?> ObtenerSucursalInventario(int IDSucursalInventario)
  {
    return await context.SucursalesInventario
      .Include(si => si.Producto)
        .ThenInclude(p => p.Unidad)
      .Include(si => si.Sucursal)
      .FirstOrDefaultAsync(si => si.IDSucursalInventario == IDSucursalInventario);
  }

  public async Task<SucursalesInventario?> ObtenerInventarioPorProductoYSucursal(string NoParte, int IDSucursal)
  {
    return await context.SucursalesInventario
      .Include(si => si.Producto)
        .ThenInclude(p => p.Unidad)
      .Include(si => si.Sucursal)
      .FirstOrDefaultAsync(si => si.NoParte == NoParte && si.IDSucursal == IDSucursal);
  }

  public async Task<bool> CrearSucursalInventario(SucursalesInventario sucursalInventario)
  {
    await context.SucursalesInventario.AddAsync(sucursalInventario);
    return await context.SaveChangesAsync() > 0;
  }

  public async Task<bool> ActualizarSucursalInventario(SucursalesInventario sucursalInventario)
  {
    var filas = await context.SucursalesInventario
      .Where(si => si.IDSucursalInventario == sucursalInventario.IDSucursalInventario)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(si => si.Existencia, sucursalInventario.Existencia)
        .SetProperty(si => si.UmbralExistencia, sucursalInventario.UmbralExistencia)
      );

    return filas > 0;
  }

  public async Task<bool> EliminarSucursalInventario(int IDSucursalInventario)
  {
    var filas = await context.SucursalesInventario
      .Where(si => si.IDSucursalInventario == IDSucursalInventario)
      .ExecuteDeleteAsync();

    return filas > 0;
  }

  public async Task<IReadOnlyList<SucursalesInventario>> ObtenerAlertasInventario()
  {
    return await context.SucursalesInventario
      .Include(si => si.Producto)
        .ThenInclude(p => p.Unidad)
      .Include(si => si.Sucursal)
      .Where(si => si.Existencia <= si.UmbralExistencia)
      .ToListAsync();
  }
}
