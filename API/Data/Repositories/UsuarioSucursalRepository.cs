using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class UsuarioSucursalRepository(AppDbContext context) : IUsuarioSucursalRepository
{
  public async Task<IReadOnlyList<UsuarioSucursal>> ObtenerUsuariosSucursales()
  {
    return await context.UsuariosSucursales
      .Include(us => us.Usuario)
        .ThenInclude(u => u.PerfilPuesto)
      .Include(us => us.Sucursal)
      .ToListAsync();
  }

  public async Task<IReadOnlyList<UsuarioSucursal>> ObtenerSucursalesPorUsuario(int IDUsuario)
  {
    return await context.UsuariosSucursales
      .Include(us => us.Usuario)
        .ThenInclude(u => u.PerfilPuesto)
      .Include(us => us.Sucursal)
      .Where(us => us.Usuario.IDUsuario == IDUsuario)
      .ToListAsync();
  }

  public async Task<IReadOnlyList<UsuarioSucursal>> ObtenerUsuariosPorSucursal(int IDSucursal)
  {
    return await context.UsuariosSucursales
      .Include(us => us.Usuario)
        .ThenInclude(u => u.PerfilPuesto)
      .Include(us => us.Sucursal)
      .Where(us => us.Sucursal.IDSucursal == IDSucursal)
      .ToListAsync();
  }

  public async Task<UsuarioSucursal?> ObtenerUsuarioSucursal(int IDSucursalUsuario)
  {
    return await context.UsuariosSucursales
      .Include(us => us.Usuario)
        .ThenInclude(u => u.PerfilPuesto)
      .Include(us => us.Sucursal)
      .FirstOrDefaultAsync(us => us.IDSucursalUsuario == IDSucursalUsuario);
  }

  public async Task<bool> CrearUsuarioSucursal(UsuarioSucursal usuarioSucursal)
  {
    await context.UsuariosSucursales.AddAsync(usuarioSucursal);
    return await context.SaveChangesAsync() > 0;
  }

  public async Task<bool> InhabilitarUsuarioSucursal(int IDSucursalUsuario)
  {
    var filas = await context.UsuariosSucursales
      .Where(us => us.IDSucursalUsuario == IDSucursalUsuario)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(us => us.Activo, false)
      );

    return filas > 0;
  }

  public async Task<bool> HabilitarUsuarioSucursal(int IDSucursalUsuario)
  {
    var filas = await context.UsuariosSucursales
      .Where(us => us.IDSucursalUsuario == IDSucursalUsuario)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(us => us.Activo, true)
      );

    return filas > 0;
  }
  
  public async Task<bool> EliminarUsuarioSucursal(int IDSucursalUsuario)
  {
    var filas = await context.UsuariosSucursales
      .Where(us => us.IDSucursalUsuario == IDSucursalUsuario)
      .ExecuteDeleteAsync();

    return filas > 0;
  }
}
