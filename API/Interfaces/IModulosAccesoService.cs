using System;
using API.Data.DTOs;

namespace API.Interfaces;

public interface IModulosAccesoService
{
  Task<IReadOnlyList<DTOModulosAcceso>> ObtenerModulosAcceso();
  Task<IReadOnlyList<DTOModulosAcceso>> ObtenerModulosAccesoUsuario(int IDUsuario);
  Task<IReadOnlyList<DTOModulosAcceso>> ObtenerModulosAccesoPerfilPuesto(int IDPerfilPuesto);
  Task<IReadOnlyList<DTOModulo>> ObtenerModulos();
  Task<IReadOnlyList<DTONivel>> ObtenerNiveles();
  Task<DTOModulosAcceso?> ValidarAccesoModulo(int IDUsuario, int IDModulo);
  Task<DTOModulosAcceso?> RegistrarAccesoModulo(DTORegistrarAccesoModulo dto);
  Task<bool> EliminarAccesoModulo(int IDModuloAcceso);
}
