using ApiLogin.Configures;
using ApiLogin.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiLogin.Services
{
    public class TokenService
    {
        public static string TokenGenerate(EmployeeModel model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = GenerateClaims(model),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(3)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(EmployeeModel employee)
        {
            var claimsIdentity = new ClaimsIdentity();

            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, employee.Name));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, employee.Roles));

            return claimsIdentity;
        }
    }
}
