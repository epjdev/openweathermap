using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace epjdev.OpenWeatherMap.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        readonly string TokenSecret = "b2797113-d41d-46c6-850a-cc928418921a";
        readonly string TokenName = "default";
        readonly string TokenRole = "default";
        readonly DateTime TokenExpiringDate = new DateTime(2200, 01, 01, 00, 00, 00);

        [HttpGet]
        public object Get()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var secret = Encoding.ASCII.GetBytes(TokenSecret);

            var tokenPayload = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, TokenName),
                    new Claim(ClaimTypes.Role, TokenRole)
                }),
                Expires = TokenExpiringDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenPayload);
            return tokenHandler.WriteToken(token);
        }
    }
}
