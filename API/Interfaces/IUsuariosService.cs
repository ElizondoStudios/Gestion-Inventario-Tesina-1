using API.Data.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface IUsuariosService
{
  Task<IReadOnlyList<DTOUsuario>> ObtenerUsuarios();
  Task<DTOUsuario> ObtenerUsuario(int IDUsuario);
  Task<DTOUsuario> CrearUsuario(DTOCrearUsuario dto);
  Task<DTOUsuario> ActualizarUsuario(DTOActualizarUsuario dto);
  Task InhabilitarUsuario(int IDUsuario);
  Task HabilitarUsuario(int IDUsuario);
}
