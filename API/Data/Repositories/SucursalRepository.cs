using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class SucursalRepository(AppDbContext context) : ISucursalRepository
{
  public async Task<IReadOnlyList<Sucursal>> ObtenerSucursales()
  {
    return await context.Sucursales.ToListAsync();
  }

  public async Task<Sucursal?> ObtenerSucursal(int IDSucursal)
  {
    return await context.Sucursales.FindAsync(IDSucursal);
  }

  public async Task<bool> CrearSucursal(Sucursal sucursal)
  {
    await context.Sucursales.AddAsync(sucursal);
    return await context.SaveChangesAsync() > 0;
  }

  public async Task<bool> ActualizarSucursal(Sucursal sucursal)
  {
    var filas = await context.Sucursales
      .Where(s => s.IDSucursal == sucursal.IDSucursal)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(s => s.Nombre, sucursal.Nombre)
        .SetProperty(s => s.Direccion, sucursal.Direccion)
      );

    return filas > 0;
  }

  public async Task<bool> InhabilitarSucursal(int IDSucursal)
  {
    var filas = await context.Sucursales
      .Where(s => s.IDSucursal == IDSucursal && s.Activo)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(s => s.Activo, false)
      );

    return filas > 0;
  }

  public async Task<bool> HabilitarSucursal(int IDSucursal)
  {
    var filas = await context.Sucursales
      .Where(s => s.IDSucursal == IDSucursal && !s.Activo)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(s => s.Activo, true)
      );

    return filas > 0;
  }
}
