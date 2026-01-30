using System.ComponentModel.DataAnnotations;
using API.Data.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Inventario(IInventarioService inventarioService) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<DTOInventario>>> GetInventario()
        {
            var res = await inventarioService.ObtenerInventario();
            return Ok(res);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<DTOInventario>> GetProducto([Required] string NoParte)
        {
            var res = await inventarioService.ObtenerProducto(NoParte);
            return Ok(res);
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult<DTOInventario>> CrearProducto([FromBody] DTOCrearInventario dto)
        {
            var res = await inventarioService.CrearProducto(dto);
            return Ok(res);
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult<DTOInventario>> ActualizarProducto([FromBody] DTOActualizarInventario dto)
        {
            var res = await inventarioService.ActualizarProducto(dto);
            return Ok(res);
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult> InhabilitarProducto([Required] string NoParte)
        {
            await inventarioService.InhabilitarProducto(NoParte);
            return Ok();
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult> HabilitarProducto([Required] string NoParte)
        {
            await inventarioService.HabilitarProducto(NoParte);
            return Ok();
        }
    }
}
