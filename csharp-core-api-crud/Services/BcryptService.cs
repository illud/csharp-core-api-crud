using BC = BCrypt.Net.BCrypt;

namespace Services
{
    public class BcryptService
    {
        public string HashPassword(string password)
        {
            string passwordHash = BC.HashPassword(password);

            return passwordHash;
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            bool verify = BC.Verify(password, passwordHash);

            return verify;
        }
    }
}
