using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class LogInventarioRepository(AppDbContext context) : ILogInventarioRepository
{
  public async Task<IReadOnlyList<LogInventario>> ObtenerLogInventario()
  {
    return await context.LogInventario
      .Include(l => l.QuienRealiza)
      .Include(l => l.Producto)
        .ThenInclude(p => p.Unidad)
      .Include(l => l.Sucursal)
      .Include(l => l.TipoMovimiento)
      .OrderByDescending(l => l.Fecha)
      .ToListAsync();
  }

  public async Task<IReadOnlyList<LogInventario>> ObtenerLogPorProducto(string NoParte)
  {
    return await context.LogInventario
      .Include(l => l.QuienRealiza)
      .Include(l => l.Producto)
        .ThenInclude(p => p.Unidad)
      .Include(l => l.Sucursal)
      .Include(l => l.TipoMovimiento)
      .Where(l => l.Producto.NoParte == NoParte)
      .OrderByDescending(l => l.Fecha)
      .ToListAsync();
  }

  public async Task<IReadOnlyList<LogInventario>> ObtenerLogPorSucursal(int IDSucursal)
  {
    return await context.LogInventario
      .Include(l => l.QuienRealiza)
      .Include(l => l.Producto)
        .ThenInclude(p => p.Unidad)
      .Include(l => l.Sucursal)
      .Include(l => l.TipoMovimiento)
      .Where(l => IDSucursal!=0? l.Sucursal.IDSucursal == IDSucursal: true)
      .OrderByDescending(l => l.Fecha)
      .ToListAsync();
  }

  public async Task<IReadOnlyList<LogInventario>> ObtenerLogPorUsuario(int IDUsuario)
  {
    return await context.LogInventario
      .Include(l => l.QuienRealiza)
      .Include(l => l.Producto)
        .ThenInclude(p => p.Unidad)
      .Include(l => l.Sucursal)
      .Include(l => l.TipoMovimiento)
      .Where(l => l.QuienRealiza.IDUsuario == IDUsuario)
      .OrderByDescending(l => l.Fecha)
      .ToListAsync();
  }

  public async Task<LogInventario?> ObtenerLog(int IDLogInventario)
  {
    return await context.LogInventario
      .Include(l => l.QuienRealiza)
      .Include(l => l.Producto)
        .ThenInclude(p => p.Unidad)
      .Include(l => l.Sucursal)
      .Include(l => l.TipoMovimiento)
      .FirstOrDefaultAsync(l => l.IDLogInventario == IDLogInventario);
  }

  public async Task<bool> CrearLogInventario(LogInventario logInventario)
  {
    await context.LogInventario.AddAsync(logInventario);
    return await context.SaveChangesAsync() > 0;
  }

  public async Task<TiposMovimientosInventario?> ObtenerTipoMovimiento(int IDTipoMovimiento)
  {
    return await context.Set<TiposMovimientosInventario>()
      .FirstOrDefaultAsync(tm => tm.IDTipoMovimientoInventario == IDTipoMovimiento);
  }
  
  public async Task<IQueryable<LogInventario>> ObtenerTotales()
  {
    return context.LogInventario
      .Include(li => li.Producto)
      .Where(li => li.IDTipoMovimiento == 1 || li.IDTipoMovimiento == 3 || li.IDTipoMovimiento == 4) //Venta, Merma, Compra
      .Where(li => li.Fecha.Month == DateTime.Now.Month); //Solo del mes corriente
  }

  public async Task<IReadOnlyList<LogInventario>> ObtenerMovimientosRecientes()
  {
    return await context.LogInventario
      .Include(l => l.QuienRealiza)
      .Include(l => l.Producto)
        .ThenInclude(p => p.Unidad)
      .Include(l => l.Sucursal)
      .Include(l => l.TipoMovimiento)
      .OrderByDescending(l => l.Fecha)
      .Take(5)
      .ToListAsync();
  }
  
  public async Task<IReadOnlyList<LogInventario>> ObtenerVentasVsCompras()
  {
    return await context.LogInventario
      .Include(li => li.Producto)
      .Where(li => li.IDTipoMovimiento == 1 || li.IDTipoMovimiento == 4) //Venta, Compra
      .Where(li => li.Fecha.Year == DateTime.Now.Year) //Solo del año corriente
      .ToListAsync(); 
  }
  public async Task<IReadOnlyList<TiposMovimientosInventario>> ObtenerTiposMovimiento()
  {
    return await context.TiposMovimientosInventario.ToListAsync();
  }
}
