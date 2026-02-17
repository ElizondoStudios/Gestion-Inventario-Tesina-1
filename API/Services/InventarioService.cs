using System;
using API.Data.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Services;

public class InventarioService(IInventarioRepository inventarioRepository, IUnidadRepository unidadRepository): IInventarioService
{
  private static DTOInventario? ConvertirDTO(Inventario? registro)
  {
    return registro == null ?
    null :
    new DTOInventario
    {
      NoParte = registro.NoParte,
      NombreProducto = registro.NombreProducto,
      DescripcionProducto = registro.DescripcionProducto,
      Precio = registro.Precio,
      Costo = registro.Costo,
      Activo = registro.Activo,
      IDUnidad = registro.IDUnidad,
      DescripcionUnidad = registro.Unidad?.Descripcion,
      AbreviacionUnidad = registro.Unidad?.Abreviacion
    };
  }

  public async Task<IReadOnlyList<DTOInventario>> ObtenerInventario()
  {
    var registros = await inventarioRepository.ObtenerInventario();
    return [.. registros.Select(r => ConvertirDTO(r)!)]; 
  }

  public async Task<DTOInventario> ObtenerProducto(string NoParte)
  {
    var registro = await inventarioRepository.ObtenerProducto(NoParte) ?? throw new Exception("No se encontró el producto");
    return ConvertirDTO(registro)!;
  }

  public async Task<DTOInventario> CrearProducto(DTOCrearInventario dto)
  {
    // Validar que la unidad existe y está activa
    var unidad = await unidadRepository.ObtenerUnidad(dto.IDUnidad) ?? throw new Exception("La unidad especificada no existe");
    
    if (!unidad.Activo)
      throw new Exception("La unidad especificada no está activa");

    // Validar que el NoParte no existe
    var productoExistente = await inventarioRepository.ObtenerProducto(dto.NoParte);
    if (productoExistente != null)
      throw new Exception("Ya existe un producto con ese número de parte");

    // Crear nuevo registro
    var registro = new Inventario
    {
      NoParte = dto.NoParte,
      NombreProducto = dto.NombreProducto,
      DescripcionProducto = dto.DescripcionProducto,
      Precio = dto.Precio,
      Costo = dto.Costo,
      IDUnidad = dto.IDUnidad,
      Activo = true
    };

    // Persistir
    if(!await inventarioRepository.CrearProducto(registro))
    {
      throw new Exception("Ocurrió un error al crear el producto");
    }

    // Obtener el producto completo con la unidad
    var productoCreado = await inventarioRepository.ObtenerProducto(registro.NoParte);
    return ConvertirDTO(productoCreado)!;
  }
  
  public async Task<DTOInventario> ActualizarProducto(DTOActualizarInventario dto)
  {
    var registro = await inventarioRepository.ObtenerProducto(dto.NoParte) ?? throw new Exception("Producto no encontrado");
    
    // Validar activo
    if (!registro.Activo)
      throw new Exception("No se puede modificar un producto inactivo");

    // Validar que la unidad existe y está activa
    var unidad = await unidadRepository.ObtenerUnidad(dto.IDUnidad) ?? throw new Exception("La unidad especificada no existe");
    
    if (!unidad.Activo)
      throw new Exception("La unidad especificada no está activa");

    // Aplicar cambios
    registro.NombreProducto = dto.NombreProducto;
    registro.DescripcionProducto = dto.DescripcionProducto;
    registro.Precio = dto.Precio;
    registro.Costo = dto.Costo;
    registro.IDUnidad = dto.IDUnidad;

    // Persistir
    if(!await inventarioRepository.ActualizarProducto(registro))
    {
      throw new Exception("Ocurrió un error al actualizar el producto");
    }

    // Obtener el producto actualizado con la unidad
    var productoActualizado = await inventarioRepository.ObtenerProducto(registro.NoParte);
    return ConvertirDTO(productoActualizado)!;
  }

  public async Task InhabilitarProducto(string NoParte)
  {
    var success = await inventarioRepository.InhabilitarProducto(NoParte);
    if (!success)
    {
      throw new Exception("Hubo un error al inhabilitar el producto");
    }
    return;
  }
 
  public async Task HabilitarProducto(string NoParte)
  {
    var success = await inventarioRepository.HabilitarProducto(NoParte);
    if (!success)
    {
      throw new Exception("Hubo un error al habilitar el producto");
    }
    return;
  }
}
