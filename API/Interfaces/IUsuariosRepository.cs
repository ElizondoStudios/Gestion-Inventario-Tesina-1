using API.Entities;

namespace API.Interfaces;

public interface IUsuariosRepository
{
  Task<IReadOnlyList<Usuario>> ObtenerUsuarios();
  Task<Usuario?> ObtenerUsuario(int IDUsuario);
  Task<bool> CrearUsuario(Usuario usuario);
  Task<bool> ActualizarUsuario(Usuario usuario);
  Task<bool> InhabilitarUsuario(int IDUsuario);
  Task<bool> HabilitarUsuario(int IDUsuario);
  Task<bool> ExisteCorreo(string Correo, int IDUsuario);
}