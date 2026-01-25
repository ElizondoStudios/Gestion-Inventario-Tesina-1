using System;
using API.Data.DTOs;

namespace API.Interfaces;

public interface IModulosAccesoService
{
  Task<IReadOnlyList<DTOModulosAcceso>> ObtenerModulosAcceso();
  Task<DTOModulosAcceso?> ValidarAccesoModulo(int IDUsuario, int IDModulo);
  Task<DTOModulosAcceso?> RegistrarAccesoModulo(DTORegistrarAccesoModulo dto);
  Task<DTOModulosAcceso?> EliminarAccesoModulo(int IDModuloAcceso);
}
