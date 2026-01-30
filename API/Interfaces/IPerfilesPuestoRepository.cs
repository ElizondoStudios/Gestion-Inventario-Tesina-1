using API.Entities;

namespace API.Interfaces;

public interface IPerfilesPuestoRepository
{
  Task<IReadOnlyList<PerfilPuesto>> ObtenerPerfilesPuesto();
  Task<PerfilPuesto?> ObtenerPerfilPuesto(int IDPerfilPuesto);
  Task<bool> CrearPerfilPuesto(PerfilPuesto PerfilPuesto);
  Task<bool> ActualizarPerfilPuesto(PerfilPuesto PerfilPuesto);
  Task<bool> InhabilitarPerfilPuesto(int IDPerfilPuesto);
  Task<bool> HabilitarPerfilPuesto(int IDPerfilPuesto);
}