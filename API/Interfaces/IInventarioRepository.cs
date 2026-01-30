using API.Entities;

namespace API.Interfaces;

public interface IInventarioRepository
{
  Task<IReadOnlyList<Inventario>> ObtenerInventario();
  Task<Inventario?> ObtenerProducto(string NoParte);
  Task<bool> CrearProducto(Inventario producto);
  Task<bool> ActualizarProducto(Inventario producto);
  Task<bool> InhabilitarProducto(string NoParte);
  Task<bool> HabilitarProducto(string NoParte);
}
