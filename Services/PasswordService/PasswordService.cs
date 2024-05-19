using Services.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace Services.PasswordService
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string pass)
        {
            byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(pass));
            return Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string pass, string userPass)
        {
            return string.Equals(HashPassword(pass), userPass);
        }
    }
}
