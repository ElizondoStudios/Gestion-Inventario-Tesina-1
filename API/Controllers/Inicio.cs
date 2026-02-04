using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Data.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Inicio(IInicioService inicioService) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<DTOAlertasInventarioInicio>>> GetAlertasInventario()
        {
            var res = await inicioService.ObtenerAlertasInventario();
            return Ok(res);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<DTOTotalesInicio>> GetTotales()
        {
            var res = await inicioService.ObtenerTotales();
            return Ok(res);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<DTOMovimientosRecientesInicio>>> GetMovimientosRecientes()
        {
            var res = await inicioService.ObtenerMovimientosRecientes();
            return Ok(res);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<DTOVentasVsComprasInicio>> GetVentasVsCompras()
        {
            var res = await inicioService.ObtenerVentasVsCompras();
            return Ok(res);
        }
    }
}
