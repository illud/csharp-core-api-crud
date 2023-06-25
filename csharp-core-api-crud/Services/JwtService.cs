using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService()
        {
        }

        public JwtService(IConfiguration configuration) {
            _configuration = configuration;
        }

        public string GenerateJwt(string user) {

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user)
            };

           // var tokenKey = _configuration.GetSection("Jwt:Key").Value;

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("VpEWuEAohDTOkD6y2z6xc69WQr39pAdAVpEWuEAohDTOkD6y2z6xc69WQr39pAdA"));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public bool ValidateJwt()
        {
            return true;
        }
    }
}
