using JWT.Algorithms;
using JWT.Builder;
using System.Security.Cryptography;
using System.Security.Policy;

namespace Services
{
    public class JwtService
    {
        private readonly RSA certificate;

        public string GenerateJwt(string user) {
       
            var token = JwtBuilder.Create()
                      .WithAlgorithm(new RS256Algorithm(certificate))
                      .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                      .AddClaim("user", user)
                      .Encode();
            
            return token;
        }

        public bool ValidateJwt()
        {

            return true;
        }
    }
}
