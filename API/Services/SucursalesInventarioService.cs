using System;
using API.Data.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Services;

public class SucursalesInventarioService(ISucursalesInventarioRepository sucursalesInventarioRepository): ISucursalesInventarioService
{
  private static DTOSucursalInventario? ConvertirDTO(SucursalesInventario? registro)
  {
    return registro == null ?
    null :
    new DTOSucursalInventario
    {
      IDSucursalInventario = registro.IDSucursalInventario,
      Existencia = registro.Existencia,
      UmbralExistencia = registro.UmbralExistencia,
      NoParte = registro.NoParte,
      NombreProducto = registro.Producto.NombreProducto,
      Unidad = registro.Producto.Unidad.Abreviacion,
      IDSucursal = registro.IDSucursal,
      NombreSucursal = registro.Sucursal.Nombre
    };
  }

  public async Task<IReadOnlyList<DTOSucursalInventario>> ObtenerSucursalesInventario()
  {
    var registros = await sucursalesInventarioRepository.ObtenerSucursalesInventario();
    return [.. registros.Select(r => ConvertirDTO(r)!)];
  }

  public async Task<IReadOnlyList<DTOSucursalInventario>> ObtenerInventarioPorSucursal(int IDSucursal)
  {
    var registros = await sucursalesInventarioRepository.ObtenerInventarioPorSucursal(IDSucursal);
    return [.. registros.Select(r => ConvertirDTO(r)!)];
  }

  public async Task<DTOSucursalInventario?> ObtenerSucursalInventario(int IDSucursalInventario)
  {
    var registro = await sucursalesInventarioRepository.ObtenerSucursalInventario(IDSucursalInventario);
    return ConvertirDTO(registro);
  }

  public async Task<DTOSucursalInventario> CrearSucursalInventario(DTOCrearSucursalInventario dto)
  {
    var nuevoRegistro = new SucursalesInventario
    {
      Existencia = dto.Existencia,
      UmbralExistencia = dto.UmbralExistencia,
      NoParte = dto.NoParte,
      IDSucursal = dto.IDSucursal
    };

    if (!await sucursalesInventarioRepository.CrearSucursalInventario(nuevoRegistro))
    {
      throw new Exception("Ocurrió un error al crear el registro");
    }

    var registroCreado = await sucursalesInventarioRepository.ObtenerSucursalInventario(nuevoRegistro.IDSucursalInventario);
    return ConvertirDTO(registroCreado)!;
  }

  public async Task<DTOSucursalInventario> ActualizarSucursalInventario(DTOActualizarSucursalInventario dto)
  {
    var registro = await sucursalesInventarioRepository.ObtenerSucursalInventario(dto.IDSucursalInventario) ?? throw new Exception("Registro no encontrado");

    // Aplicar cambios
    registro.Existencia = dto.Existencia;
    registro.UmbralExistencia = dto.UmbralExistencia;

    // Persistir
    if (!await sucursalesInventarioRepository.ActualizarSucursalInventario(registro))
    {
      throw new Exception("Ocurrió un error al actualizar el registro");
    }

    var registroActualizado = await sucursalesInventarioRepository.ObtenerSucursalInventario(dto.IDSucursalInventario);
    return ConvertirDTO(registroActualizado)!;
  }

  public async Task EliminarSucursalInventario(int IDSucursalInventario)
  {
    var success = await sucursalesInventarioRepository.EliminarSucursalInventario(IDSucursalInventario);
    if (!success)
    {
      throw new Exception("Hubo un error al eliminar el registro");
    }
  }
}
