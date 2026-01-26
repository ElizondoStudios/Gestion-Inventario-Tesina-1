using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Interfaces;
using API.Data.DTOs;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioSucursal(IUsuarioSucursalService usuarioSucursalService) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<DTOUsuarioSucursal>>> GetUsuariosSucursales()
        {
            var res = await usuarioSucursalService.ObtenerUsuariosSucursales();
            return Ok(res);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<DTOUsuarioSucursal>> GetUsuarioSucursal([Required] int IDSucursalUsuario)
        {
            var res = await usuarioSucursalService.ObtenerUsuarioSucursal(IDSucursalUsuario);
            if (res == null)
                return NotFound();
            return Ok(res);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<DTOUsuarioSucursal>>> GetSucursalesPorUsuario([Required] int IDUsuario)
        {
            var res = await usuarioSucursalService.ObtenerSucursalesPorUsuario(IDUsuario);
            return Ok(res);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<DTOUsuarioSucursal>>> GetUsuariosPorSucursal([Required] int IDSucursal)
        {
            var res = await usuarioSucursalService.ObtenerUsuariosPorSucursal(IDSucursal);
            return Ok(res);
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult<DTOUsuarioSucursal>> CrearUsuarioSucursal([FromBody] DTOCrearUsuarioSucursal dto)
        {
            var res = await usuarioSucursalService.CrearUsuarioSucursal(dto);
            return Ok(res);
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult<DTOUsuarioSucursal>> InhabilitarUsuarioSucursal([Required] int IDSucursalUsuario)
        {
            var res = await usuarioSucursalService.InhabilitarUsuarioSucursal(IDSucursalUsuario);
            return Ok(res);
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult<DTOUsuarioSucursal>> HabilitarUsuarioSucursal([Required] int IDSucursalUsuario)
        {
            var res = await usuarioSucursalService.HabilitarUsuarioSucursal(IDSucursalUsuario);
            return Ok(res);
        }
    }
}
