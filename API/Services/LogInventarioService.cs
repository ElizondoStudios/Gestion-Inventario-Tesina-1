using System;
using API.Data.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Services;

public class LogInventarioService(ILogInventarioRepository registroInventarioRepository): ILogInventarioService
{
  private static DTOLogInventario? ConvertirDTO(LogInventario? registro)
  {
    return registro == null ?
    null :
    new DTOLogInventario
    {
      IDLogInventario = registro.IDLogInventario,
      Fecha = registro.Fecha,
      Cantidad = registro.Cantidad,
      IDUsuario = registro.IDUsuario,
      NoParte = registro.NoParte,
      IDSucursal = registro.IDSucursal,
      Sucursal = registro.Sucursal.Nombre,
      IDTipoMovimiento = registro.IDTipoMovimiento,
      DescripcionTipoMovimiento = registro.TipoMovimiento.Descripcion
    };
  }

  public async Task<IReadOnlyList<DTOLogInventario>> ObtenerLogInventario()
  {
    var registros = await registroInventarioRepository.ObtenerLogInventario();
    return registros.Select(r => ConvertirDTO(r)!).Where(dto => dto != null).ToList()!;
  }

  public async Task<IReadOnlyList<DTOLogInventario>> ObtenerLogPorProducto(string NoParte)
  {
    var registros = await registroInventarioRepository.ObtenerLogPorProducto(NoParte);
    return registros.Select(r => ConvertirDTO(r)!).Where(dto => dto != null).ToList()!;
  }

  public async Task<IReadOnlyList<DTOLogInventario>> ObtenerLogPorSucursal(int IDSucursal)
  {
    var registros = await registroInventarioRepository.ObtenerLogPorSucursal(IDSucursal);
    return registros.Select(r => ConvertirDTO(r)!).Where(dto => dto != null).ToList()!;
  }

  public async Task<IReadOnlyList<DTOLogInventario>> ObtenerLogPorUsuario(int IDUsuario)
  {
    var registros = await registroInventarioRepository.ObtenerLogPorUsuario(IDUsuario);
    return registros.Select(r => ConvertirDTO(r)!).Where(dto => dto != null).ToList()!;
  }

  public async Task<DTOLogInventario?> ObtenerLog(int IDLogInventario)
  {
    var registro = await registroInventarioRepository.ObtenerLog(IDLogInventario);
    return ConvertirDTO(registro);
  }

  public async Task<DTOLogInventario> CrearLogInventario(DTOCrearLogInventario registroInventario)
  {
    var nuevoLog = new LogInventario
    {
      Fecha = DateTime.Now,
      Cantidad = registroInventario.Cantidad,
      IDUsuario = registroInventario.IDUsuario,
      NoParte = registroInventario.NoParte,
      IDSucursal = registroInventario.IDSucursal,
      IDTipoMovimiento = registroInventario.IDTipoMovimiento
    };

    var resultado = await registroInventarioRepository.CrearLogInventario(nuevoLog);
    
    if (!resultado)
      throw new Exception("No se pudo crear el registro de inventario");

    var registroCreado = await registroInventarioRepository.ObtenerLog(nuevoLog.IDLogInventario);
    return ConvertirDTO(registroCreado)!;
  }
}
