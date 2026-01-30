using System;
using API.Data.DTOs;

namespace API.Interfaces;

public interface IUnidadesService
{
  Task<IReadOnlyList<DTOUnidad>> ObtenerUnidades();
  Task<DTOUnidad> ObtenerUnidad(int IDUnidad);
  Task<DTOUnidad> CrearUnidad(DTOCrearUnidad dto);
  Task<DTOUnidad> ActualizarUnidad(DTOActualizarUnidad dto);
  Task InhabilitarUnidad(int IDUnidad);
  Task HabilitarUnidad(int IDUnidad);
}
