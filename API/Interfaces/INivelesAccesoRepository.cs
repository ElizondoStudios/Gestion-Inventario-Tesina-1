using API.Entities;

namespace API.Interfaces;

public interface INivelesAccesoRepository
{
  Task<IReadOnlyList<NivelesAcceso>> ObtenerNivelesAcceso();
  Task<NivelesAcceso?> ObtenerNivelAcceso(int NivelAcceso);
  Task<bool> CrearNivelAcceso(NivelesAcceso nivelAcceso);
  Task<bool> ActualizarNivelAcceso(NivelesAcceso nivelAcceso);
}
