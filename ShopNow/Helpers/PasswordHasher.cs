using System.Security.Cryptography;
using System.Text;

namespace ShopNow.Helpers
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Compute hash from password string
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to hexadecimal string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string inputPassword, string storedHashedPassword)
        {
            // Hash the input password using the same algorithm and compare with the stored hashed password
            string hashedInputPassword = HashPassword(inputPassword);
            return string.Equals(hashedInputPassword, storedHashedPassword);
        }
    }
}
