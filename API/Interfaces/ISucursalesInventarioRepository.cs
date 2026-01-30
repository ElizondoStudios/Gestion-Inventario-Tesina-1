using API.Entities;

namespace API.Interfaces;

public interface ISucursalesInventarioRepository
{
  Task<IReadOnlyList<SucursalesInventario>> ObtenerSucursalesInventario();
  Task<IReadOnlyList<SucursalesInventario>> ObtenerInventarioPorSucursal(int IDSucursal);
  Task<SucursalesInventario?> ObtenerSucursalInventario(int IDSucursalInventario);
  Task<bool> CrearSucursalInventario(SucursalesInventario sucursalInventario);
  Task<bool> ActualizarSucursalInventario(SucursalesInventario sucursalInventario);
  Task<bool> EliminarSucursalInventario(int IDSucursalInventario);
}
