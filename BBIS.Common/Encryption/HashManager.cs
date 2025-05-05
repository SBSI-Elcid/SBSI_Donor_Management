using System.Security.Cryptography;
using System.Text;

namespace BBIS.Common.Encryption
{
    public class HashManager
    {
        public const int SaltBytes = 30;
        public const int HashBytes = 24;

        public static string CreateHash(string password, string salt, string appkey)
        {
            var finalString = appkey + salt + password;

            return GetFinalHash(finalString);
        }

        public static string GenerateSaltString()
        {
            var saltBytes = RandomNumberGenerator.GetBytes(SaltBytes);
            var salt = Convert.ToBase64String(saltBytes);

            return salt;
        }

        public static string GetHash(string password, string appkey, string salt)
        {
            var finalString = appkey + salt + password;
            return GetFinalHash(finalString);
        }

        public static string Hash(string password)
        {
            return GetFinalHash(password);
        }

        private static string GetFinalHash(string toHashValue)
        {
            var dataBytes = Encoding.UTF8.GetBytes(toHashValue);
            var sha256 = SHA256.Create();
            var resultBytes = sha256.ComputeHash(dataBytes);

            // return the hash string to the caller
            return Convert.ToBase64String(resultBytes);
        }
    }
}
