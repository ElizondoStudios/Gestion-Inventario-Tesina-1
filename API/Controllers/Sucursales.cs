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
    public class Sucursales(ISucursalesService sucursalesService) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<DTOSucursal>>> GetSucursales()
        {
            var res = await sucursalesService.ObtenerSucursales();
            return Ok(res);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<DTOSucursal>> GetSucursal([Required] int IDSucursal)
        {
            var res = await sucursalesService.ObtenerSucursal(IDSucursal);
            return Ok(res);
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult<DTOSucursal>> CrearSucursal([FromBody] DTOCrearSucursal dto)
        {
            var res= await sucursalesService.CrearSucursal(dto);
            return Ok(res);
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult<DTOSucursal>> ActualizarSucursal([FromBody] DTOActualizarSucursal dto)
        {
            var res= await sucursalesService.ActualizarSucursal(dto);
            return Ok(res);
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult<DTOSucursal>> InhabilitarSucursal([Required] int IDSucursal)
        {
            await sucursalesService.InhabilitarSucursal(IDSucursal);
            return Ok();
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult<DTOSucursal>> HabilitarSucursal([Required] int IDSucursal)
        {
            await sucursalesService.HabilitarSucursal(IDSucursal);
            return Ok();
        }
    }
}
