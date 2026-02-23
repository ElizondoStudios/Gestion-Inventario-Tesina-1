using System;
using API.Data.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Services;

public class LogInventarioService(
  ILogInventarioRepository registroInventarioRepository,
  ISucursalesInventarioRepository sucursalesInventarioRepository
) : ILogInventarioService
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

  private async Task ActualizarInventarioSucursal(string NoParte, int IDSucursal, decimal cantidad, int IDTipoMovimiento)
  {
    var tipoMovimiento = await registroInventarioRepository.ObtenerTipoMovimiento(IDTipoMovimiento);

    if (tipoMovimiento == null)
      throw new Exception($"No se encontró el tipo de movimiento con ID {IDTipoMovimiento}");

    bool esEntrada = tipoMovimiento.EntradaSalida;

    var inventarioSucursal = await sucursalesInventarioRepository.ObtenerInventarioPorProductoYSucursal(NoParte, IDSucursal);

    if (inventarioSucursal == null)
    {
      // No existe el inventario sucursal, hay que crearlo
      var sucursalInventario= new SucursalesInventario
      {
        Existencia = cantidad,
        UmbralExistencia = 100,
        NoParte = NoParte,
        IDSucursal = IDSucursal
      };
      await sucursalesInventarioRepository.CrearSucursalInventario(sucursalInventario);
      return;
    }

    // Si es entrada, suma; si es salida, resta
    if (esEntrada)
      inventarioSucursal.Existencia += cantidad;
    else
      inventarioSucursal.Existencia -= cantidad;

    // Validar que no quede en negativo
    if (inventarioSucursal.Existencia < 0)
      throw new Exception("No hay suficiente existencia para realizar esta operación");

    var resultado = await sucursalesInventarioRepository.ActualizarSucursalInventario(inventarioSucursal);

    if (!resultado)
      throw new Exception("No se pudo actualizar el inventario de la sucursal");
  }

  public async Task<DTOLogInventario> CrearLogInventario(DTOCrearLogInventario dto)
  {
    await ActualizarInventarioSucursal(dto.NoParte, dto.IDSucursal, dto.Cantidad, dto.IDTipoMovimiento);
    var nuevoLog = new LogInventario
    {
      Fecha = DateTime.Now,
      Cantidad = dto.Cantidad,
      IDUsuario = dto.IDUsuario,
      NoParte = dto.NoParte,
      IDSucursal = dto.IDSucursal,
      IDTipoMovimiento = dto.IDTipoMovimiento
    };

    var resultado = await registroInventarioRepository.CrearLogInventario(nuevoLog);

    if (!resultado)
      throw new Exception("No se pudo crear el registro de inventario");

    var registroCreado = await registroInventarioRepository.ObtenerLog(nuevoLog.IDLogInventario);
    return ConvertirDTO(registroCreado)!;
  }

  public async Task<IReadOnlyList<DTOTipoMovimiento>> ObtenerTiposMovimiento()
  {
    var registros = await registroInventarioRepository.ObtenerTiposMovimiento();
    return registros.Select(r => new DTOTipoMovimiento
    {
      IDTipoMovimientoInventario = r.IDTipoMovimientoInventario,
      Descripcion = r.Descripcion,
      EntradaSalida = r.EntradaSalida
    }).Where(dto => dto != null).ToList()!;
  }

}
