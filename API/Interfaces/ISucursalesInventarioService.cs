using System;
using API.Data.DTOs;

namespace API.Interfaces;

public interface ISucursalesInventarioService
{
  Task<IReadOnlyList<DTOSucursalInventario>> ObtenerSucursalesInventario();
  Task<IReadOnlyList<DTOSucursalInventario>> ObtenerInventarioPorSucursal(int IDSucursal);
  Task<DTOSucursalInventario?> ObtenerSucursalInventario(int IDSucursalInventario);
  Task<DTOSucursalInventario> CrearSucursalInventario(DTOCrearSucursalInventario dto);
  Task<DTOSucursalInventario> ActualizarSucursalInventario(DTOActualizarSucursalInventario dto);
  Task EliminarSucursalInventario(int IDSucursalInventario);
}
