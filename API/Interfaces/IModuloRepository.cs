using API.Entities;

namespace API.Interfaces;

public interface IModuloRepository
{
  Task<IReadOnlyList<Modulo>> ObtenerModulos();
  Task<Modulo?> ObtenerModulo(int IDModulo);
  Task<bool> CrearModulo(Modulo modulo);
  Task<bool> ActualizarModulo(Modulo modulo);
  Task<bool> InhabilitarModulo(int IDModulo);
  Task<bool> HabilitarModulo(int IDModulo);
}
