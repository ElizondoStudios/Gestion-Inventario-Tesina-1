using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Data.DTOs;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LogInventario(ILogInventarioService logInventarioService) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<DTOLogInventario>>> GetLogInventario()
        {
            var res = await logInventarioService.ObtenerLogInventario();
            return Ok(res);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<DTOLogInventario>> GetLog([Required] int IDLogInventario)
        {
            var res = await logInventarioService.ObtenerLog(IDLogInventario);
            if (res == null)
                return NotFound(new {mensaje = "No se encontró el registro"});
            return Ok(res);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<DTOLogInventario>>> GetLogPorProducto([Required] string NoParte)
        {
            var res = await logInventarioService.ObtenerLogPorProducto(NoParte);
            return Ok(res);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<DTOLogInventario>>> GetLogPorSucursal([Required] int IDSucursal)
        {
            var res = await logInventarioService.ObtenerLogPorSucursal(IDSucursal);
            return Ok(res);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<DTOLogInventario>>> GetLogPorUsuario([Required] int IDUsuario)
        {
            var res = await logInventarioService.ObtenerLogPorUsuario(IDUsuario);
            return Ok(res);
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult<DTOLogInventario>> CrearLogInventario([FromBody] DTOCrearLogInventario dto)
        {
            var res = await logInventarioService.CrearLogInventario(dto);
            return Ok(res);
        }
    }
}
