using API.Entities;

namespace API.Interfaces;

public interface IModulosAccesoRepository
{
  Task<IReadOnlyList<ModulosAcceso>> ObtenerModulosAcceso();
  Task<IReadOnlyList<ModulosAcceso>> ObtenerModulosAccesoPorPerfilPuesto(int IDPerfilPuesto);
  Task<ModulosAcceso?> ObtenerModuloAcceso(int IDModuloAcceso);
  Task<bool> CrearModuloAcceso(ModulosAcceso moduloAcceso);
  Task<bool> EliminarModuloAcceso(int IDModuloAcceso);
}
