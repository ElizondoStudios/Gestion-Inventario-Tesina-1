using System;
using API.Data.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Services;

public class ModulosAccesoService(IModulosAccesoRepository modulosAccesoRepository): IModulosAccesoService
{
  private static DTOModulosAcceso? ConvertirDTO(ModulosAcceso? registro)
  {
    return registro== null?
    null :
    new DTOModulosAcceso
    {
      IDModuloAcceso = registro.IDModuloAcceso,
      IDModulo = registro.IDModulo,
      NombreModulo = registro.Modulo.Nombre,
      IDPerfilPuesto = registro.IDPerfilPuesto,
      DescripcionPerfilPuesto = registro.PerfilPuesto.Descripcion,
      NivelAcceso = registro.NivelAcceso.NivelAcceso,
      DescripcionNivelAcceso = registro.NivelAcceso.Descripcion,
    };
  }

  public async Task<IReadOnlyList<DTOModulosAcceso>> ObtenerModulosAcceso()
  {
    var registros = await modulosAccesoRepository.ObtenerModulosAcceso();
    return [.. registros.Select(r => ConvertirDTO(r)!)]; 
  }
  public async Task<IReadOnlyList<DTOModulosAcceso>> ObtenerModulosAccesoUsuario(int IDUsuario)
  {
    var registros = await modulosAccesoRepository.ObtenerModulosAccesoUsuario(IDUsuario);
    return [.. registros.Select(r => ConvertirDTO(r)!)]; 
  }
  public async Task<IReadOnlyList<DTOModulosAcceso>> ObtenerModulosAccesoPerfilPuesto(int IDPerfilPuesto)
  {
    var registros = await modulosAccesoRepository.ObtenerModulosAcceso();
    return [.. registros.Where(pp => pp.IDPerfilPuesto == IDPerfilPuesto).Select(r => ConvertirDTO(r)!)]; 
  }
  public async Task<DTOModulosAcceso?> ValidarAccesoModulo(int IDUsuario, int IDModulo)
  {
    var registro = await modulosAccesoRepository.ValidarAccesoModulo(IDUsuario,IDModulo);
    return ConvertirDTO(registro);
  }
  public async Task<DTOModulosAcceso?> RegistrarAccesoModulo(DTORegistrarAccesoModulo dto)
  {
    var acceso= new ModulosAcceso{
      IDModulo = dto.IDModulo,
      IDNivelAcceso = dto.IDNivelAcceso,
      IDPerfilPuesto = dto.IDPerfilPuesto
    };

    var registro = await modulosAccesoRepository.RegistrarAccesoModulo(acceso);
    return ConvertirDTO(registro);
  }
  public async Task<bool> EliminarAccesoModulo(int IDModuloAcceso)
  {
    var exito = await modulosAccesoRepository.EliminarAccesoModulo(IDModuloAcceso);
    return exito;
  }
  public async Task<IReadOnlyList<DTOModulo>> ObtenerModulos()
  {
    var registros = await modulosAccesoRepository.ObtenerModulos();
    return [.. registros.Select(r => new DTOModulo{
      IDModulo= r.IDModulo,
      Icono = r.Icono,
      Nombre = r.Nombre
    })];
  }
  public async Task<IReadOnlyList<DTONivel>> ObtenerNiveles()
  {
    var registros = await modulosAccesoRepository.ObtenerNiveles();
    return [.. registros.Select(r => new DTONivel{
      Descripcion = r.Descripcion,
      NivelAcceso = r.NivelAcceso
    })];
  }
}
