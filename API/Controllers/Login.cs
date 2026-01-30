using API.Data.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login(ITokenService tokenService, ILoginService loginService) : ControllerBase
    {
        [HttpPost("[action]")]
        public async Task<ActionResult<DTOSesion>> IniciarSesion([FromBody] DTOIniciarSesion dto)
        {
            var usuario = await loginService.IniciarSesion(dto);
            if(usuario == null)
            {
                return Unauthorized(new { mensaje = "Usuario y/o contraseña incorrectos" });
            }
            string token= tokenService.CreateToken(usuario);
            var sesion= new DTOSesion
            {
                IDUsuario = usuario.IDUsuario,
                Token= token
            };
            return Ok(sesion);
        }
    }
}
