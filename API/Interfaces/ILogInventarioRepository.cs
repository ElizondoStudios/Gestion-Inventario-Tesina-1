using API.Entities;

namespace API.Interfaces;

public interface ILogInventarioRepository
{
  Task<IReadOnlyList<LogInventario>> ObtenerLogInventario();
  Task<IReadOnlyList<LogInventario>> ObtenerLogPorProducto(string NoParte);
  Task<IReadOnlyList<LogInventario>> ObtenerLogPorSucursal(int IDSucursal);
  Task<IReadOnlyList<LogInventario>> ObtenerLogPorUsuario(int IDUsuario);
  Task<LogInventario?> ObtenerLog(int IDLogInventario);
  Task<bool> CrearLogInventario(LogInventario logInventario);
}
