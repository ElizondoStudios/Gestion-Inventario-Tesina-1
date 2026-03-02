using System;
using API.Controllers;
using API.Data.DTOs;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class InicioService(ISucursalesInventarioRepository sucursalesInventarioRepository, ILogInventarioRepository logInventarioRepository): IInicioService
{
  public async Task<IReadOnlyList<DTOAlertasInventarioInicio>> ObtenerAlertasInventario()
  {
    var sucursalesInventarioBajos = await sucursalesInventarioRepository.ObtenerAlertasInventario();
    List<DTOAlertasInventarioInicio> alertas = [];
    foreach(var sucursalInventarioBajo in sucursalesInventarioBajos)
    {
      alertas.Add(new DTOAlertasInventarioInicio
      {
        Existencia = sucursalInventarioBajo.Existencia,
        UmbralExistencia = sucursalInventarioBajo.UmbralExistencia,
        NoParte = sucursalInventarioBajo.NoParte,
        NombreSucursal = sucursalInventarioBajo.Sucursal.Nombre
      });
    }
    return alertas;
  }

  public async Task<DTOTotalesInicio> ObtenerTotales()
  {
    var totalesQueryable= await logInventarioRepository.ObtenerTotales();
    decimal ventas= await totalesQueryable
      .Where(li => li.IDTipoMovimiento == 1)
      .SumAsync(li => (li.Cantidad)* (li.Producto.Precio));
    
    decimal compras= await totalesQueryable
      .Where(li => li.IDTipoMovimiento == 4)
      .SumAsync(li => (li.Cantidad)* (li.Producto.Costo));
    
    decimal merma= await totalesQueryable
      .Where(li => li.IDTipoMovimiento == 3)
      .SumAsync(li => (li.Cantidad)* (li.Producto.Costo));

    return new DTOTotalesInicio
    {
      Ventas = ventas,
      Compras = compras,
      Merma = merma
    };
  }

  public async Task<IReadOnlyList<DTOMovimientosRecientesInicio>> ObtenerMovimientosRecientes()
  {
    var logInventarioMovimientosRecientes= await logInventarioRepository.ObtenerMovimientosRecientes();
    List<DTOMovimientosRecientesInicio> movimientosRecientes = [];
    foreach(var log in logInventarioMovimientosRecientes)
    {
      movimientosRecientes.Add(new DTOMovimientosRecientesInicio
      {
        Cantidad = log.Cantidad,
        DescripcionTipoMoviento = log.TipoMovimiento.Descripcion,
        DescripcionUnidad = log.Producto.Unidad.Descripcion,
        Fecha = log.Fecha,
        NombreProducto = log.Producto.NombreProducto,
        EntradaSalida = log.TipoMovimiento.EntradaSalida
      });
    }
    
    return movimientosRecientes;
  }
  public async Task<DTOVentasVsComprasInicio> ObtenerVentasVsCompras()
  {
    var logInventario = await logInventarioRepository.ObtenerVentasVsCompras();
    var ventasVsCompras = new DTOVentasVsComprasInicio
    {
      Ventas = [],
      Compras = []
    };
    foreach(var log in logInventario)
    {
      if(log.IDTipoMovimiento == 1)
      {
        // Venta
        ventasVsCompras.Ventas.Add(new VentaInicio
        {
          Total = log.Cantidad * log.Producto.Precio,
          Fecha = log.Fecha
        });
      }
      else
      {
        // Compra
        ventasVsCompras.Compras.Add(new CompraInicio
        {
          Total = log.Cantidad * log.Producto.Costo,
          Fecha = log.Fecha
        });
      }
    }
    return ventasVsCompras;
  }
}
