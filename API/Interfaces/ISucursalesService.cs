using System;
using API.Data.DTOs;

namespace API.Interfaces;

public interface ISucursalesService
{
  Task<IReadOnlyList<DTOSucursal>> ObtenerSucursales();
  Task<DTOSucursal> ObtenerSucursal(int IDSucursal);
  Task<DTOSucursal> CrearSucursal(DTOCrearSucursal dto);
  Task<DTOSucursal> ActualizarSucursal(DTOActualizarSucursal dto);
  Task InhabilitarSucursal(int IDSucursal);
  Task HabilitarSucursal(int IDSucursal);
}
