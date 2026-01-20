using API.Data.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface IUsuariosService
{
  Task<bool> CrearUsuario(DTOCrearUsuario dto);
  Task<bool> ActualizarUsuario(DTOActualizarUsuario dto);
}
