using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using wms_api.DTO;

namespace wms_api.Helpers
{
    public class JwtHelper
    {
        public static AuthenticationResponseDTO BuildToken(UserCredentialsDTO userCredentialsDTO, IList<Claim> claims, string jwtKey)
        {
            claims.Add(new Claim("email", userCredentialsDTO.Email));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(7);

            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: credentials);

            return new AuthenticationResponseDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }        
    }
}
