using CT.Core.Domain;
using CT.Manager.Interfaces.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CT.Data.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GerarToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var chave = Encoding.ASCII.GetBytes(_configuration.GetSection("Jwt:Secret").Value);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuario.Login)
        };

        claims.AddRange(usuario.Funcoes.Select(p => new Claim(ClaimTypes.Role, p.Descricao)));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Audience = _configuration.GetSection("Jwt:Audience").Value,
            Issuer = _configuration.GetSection("Jwt:Issuer").Value,
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration.GetSection("Jwt:ExpiraEmMinutos").Value)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
