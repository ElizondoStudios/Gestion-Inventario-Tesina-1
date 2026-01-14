using API.Entities;

namespace API.Interfaces;

public interface IUsuarioSucursalRepository
{
  Task<IReadOnlyList<UsuarioSucursal>> ObtenerUsuariosSucursales();
  Task<IReadOnlyList<UsuarioSucursal>> ObtenerSucursalesPorUsuario(int IDUsuario);
  Task<IReadOnlyList<UsuarioSucursal>> ObtenerUsuariosPorSucursal(int IDSucursal);
  Task<UsuarioSucursal?> ObtenerUsuarioSucursal(int IDSucursalUsuario);
  Task<bool> CrearUsuarioSucursal(UsuarioSucursal usuarioSucursal);
  Task<bool> InhabilitarUsuarioSucursal(int IDSucursalUsuario);
  Task<bool> HabilitarUsuarioSucursal(int IDSucursalUsuario);
}
