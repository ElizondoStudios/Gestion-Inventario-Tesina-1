using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration configuration): ITokenService
{
  public string CreateToken(Usuario usuario)
  {
    var tokenKey = configuration["TokenKey"] ?? throw new ArgumentNullException("No se puedo obtener la key del token");
    if (tokenKey.Length < 64)
    {
      throw new ArgumentException("la token key no tiene la longitud correcta");
    }
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
    var claims = new List<Claim>
        {
            new(ClaimTypes.Email, usuario.Correo),
            new(ClaimTypes.NameIdentifier, usuario.IDUsuario.ToString())
        };
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
    var tokenDescription = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.AddHours(5),
      SigningCredentials = creds
    };
    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescription);

    return tokenHandler.WriteToken(token);
  }
}
