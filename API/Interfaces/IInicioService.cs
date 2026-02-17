using System;
using API.Data.DTOs;

namespace API.Interfaces;

public interface IInicioService
{
  Task<IReadOnlyList<DTOAlertasInventarioInicio>> ObtenerAlertasInventario();
  Task<DTOTotalesInicio> ObtenerTotales();
  Task<IReadOnlyList<DTOMovimientosRecientesInicio>> ObtenerMovimientosRecientes();
  Task<DTOVentasVsComprasInicio> ObtenerVentasVsCompras();
}
