using API.Entities;

namespace API.Interfaces;

public interface IUsuariosService
{
  Task<bool> CrearUsuario(DTOCrearUsuario dto);
}
