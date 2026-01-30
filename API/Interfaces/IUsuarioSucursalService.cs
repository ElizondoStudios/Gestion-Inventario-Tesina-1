using System;
using API.Data.DTOs;

namespace API.Interfaces;

public interface IUsuarioSucursalService
{
  Task<IReadOnlyList<DTOUsuarioSucursal>> ObtenerUsuariosSucursales();
  Task<IReadOnlyList<DTOUsuarioSucursal>> ObtenerSucursalesPorUsuario(int IDUsuario);
  Task<IReadOnlyList<DTOUsuarioSucursal>> ObtenerUsuariosPorSucursal(int IDSucursal);
  Task<DTOUsuarioSucursal?> ObtenerUsuarioSucursal(int IDSucursalUsuario);
  Task<DTOUsuarioSucursal?> CrearUsuarioSucursal(DTOCrearUsuarioSucursal dto);
  Task<DTOUsuarioSucursal?> InhabilitarUsuarioSucursal(int IDSucursalUsuario);
  Task<DTOUsuarioSucursal?> HabilitarUsuarioSucursal(int IDSucursalUsuario);
}
