using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Interfaces;
using API.Data.DTOs;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesInventario(ISucursalesInventarioService sucursalesInventarioService) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<DTOSucursalInventario>>> GetSucursalesInventario()
        {
            var res = await sucursalesInventarioService.ObtenerSucursalesInventario();
            return Ok(res);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<DTOSucursalInventario>> GetSucursalInventario([Required] int IDSucursalInventario)
        {
            var res = await sucursalesInventarioService.ObtenerSucursalInventario(IDSucursalInventario);
            if (res == null)
                return NotFound();
            return Ok(res);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<DTOSucursalInventario>>> GetInventarioPorSucursal([Required] int IDSucursal)
        {
            var res = await sucursalesInventarioService.ObtenerInventarioPorSucursal(IDSucursal);
            return Ok(res);
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult<DTOSucursalInventario>> CrearSucursalInventario([FromBody] DTOCrearSucursalInventario dto)
        {
            var res = await sucursalesInventarioService.CrearSucursalInventario(dto);
            return Ok(res);
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult<DTOSucursalInventario>> ActualizarSucursalInventario([FromBody] DTOActualizarSucursalInventario dto)
        {
            var res = await sucursalesInventarioService.ActualizarSucursalInventario(dto);
            return Ok(res);
        }
        
        [HttpDelete("[action]")]
        public async Task<ActionResult> EliminarSucursalInventario([Required] int IDSucursalInventario)
        {
            await sucursalesInventarioService.EliminarSucursalInventario(IDSucursalInventario);
            return Ok();
        }
    }
}
