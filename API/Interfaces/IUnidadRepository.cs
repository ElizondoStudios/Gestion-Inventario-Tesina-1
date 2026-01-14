using API.Entities;

namespace API.Interfaces;

public interface IUnidadRepository
{
  Task<IReadOnlyList<Unidad>> ObtenerUnidades();
  Task<Unidad?> ObtenerUnidad(int IDUnidad);
  Task<bool> CrearUnidad(Unidad unidad);
  Task<bool> ActualizarUnidad(Unidad unidad);
  Task<bool> InhabilitarUnidad(int IDUnidad);
  Task<bool> HabilitarUnidad(int IDUnidad);
}
