using API.Entities;

namespace API.Interfaces;

public interface ISucursalRepository
{
  Task<IReadOnlyList<Sucursal>> ObtenerSucursales();
  Task<Sucursal?> ObtenerSucursal(int IDSucursal);
  Task<bool> CrearSucursal(Sucursal sucursal);
  Task<bool> ActualizarSucursal(Sucursal sucursal);
  Task<bool> InhabilitarSucursal(int IDSucursal);
  Task<bool> HabilitarSucursal(int IDSucursal);
}
