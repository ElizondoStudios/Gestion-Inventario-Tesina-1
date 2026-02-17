using API.Entities;

namespace API.Interfaces;

public interface IModulosAccesoRepository
{
  Task<IReadOnlyList<ModulosAcceso>> ObtenerModulosAcceso();
  Task<IReadOnlyList<Modulo>> ObtenerModulos();
  Task<IReadOnlyList<NivelesAcceso>> ObtenerNiveles();
  Task<ModulosAcceso?> ObtenerModuloAcceso(int IDModuloAcceso);
  Task<ModulosAcceso?> ValidarAccesoModulo(int IDUsuario, int IDModulo);
  Task<ModulosAcceso?> RegistrarAccesoModulo(ModulosAcceso modulosAcceso);
  Task<ModulosAcceso?> EliminarAccesoModulo(int IDModuloAcceso);
}
