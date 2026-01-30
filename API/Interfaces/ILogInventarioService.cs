using System;
using API.Data.DTOs;

namespace API.Interfaces;

public interface ILogInventarioService
{
  Task<IReadOnlyList<DTOLogInventario>> ObtenerLogInventario();
  Task<IReadOnlyList<DTOLogInventario>> ObtenerLogPorProducto(string NoParte);
  Task<IReadOnlyList<DTOLogInventario>> ObtenerLogPorSucursal(int IDSucursal);
  Task<IReadOnlyList<DTOLogInventario>> ObtenerLogPorUsuario(int IDUsuario);
  Task<DTOLogInventario?> ObtenerLog(int IDLogInventario);
  Task<DTOLogInventario> CrearLogInventario(DTOCrearLogInventario dto);
}
