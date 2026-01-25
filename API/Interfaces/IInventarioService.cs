using System;
using API.Data.DTOs;

namespace API.Interfaces;

public interface IInventarioService
{
  Task<IReadOnlyList<DTOInventario>> ObtenerInventario();
  Task<DTOInventario> ObtenerProducto(string NoParte);
  Task<DTOInventario> CrearProducto(DTOCrearInventario dto);
  Task<DTOInventario> ActualizarProducto(DTOActualizarInventario dto);
  Task InhabilitarProducto(string NoParte);
  Task HabilitarProducto(string NoParte);
}
