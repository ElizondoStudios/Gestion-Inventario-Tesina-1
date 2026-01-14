using API.Entities;

namespace API.Interfaces;

public interface IModulosCategoriaRepository
{
  Task<IReadOnlyList<ModulosCategoria>> ObtenerModulosCategorias();
  Task<ModulosCategoria?> ObtenerModuloCategoria(int IDModuloCategoria);
  Task<bool> CrearModuloCategoria(ModulosCategoria moduloCategoria);
  Task<bool> ActualizarModuloCategoria(ModulosCategoria moduloCategoria);
  Task<bool> InhabilitarModuloCategoria(int IDModuloCategoria);
  Task<bool> HabilitarModuloCategoria(int IDModuloCategoria);
}
