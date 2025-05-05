using System.Security.Cryptography;

namespace BBIS.Common
{
    public static class RandomStringGeneratorHelper
    {
        public static string Generate(string prefix, int length = 10)
        {
            // Generate a random byte array
            byte[] buffer = RandomNumberGenerator.GetBytes(10);

            // Convert the random byte array to a string of alpha numeric characters
            var randomText = prefix + Convert.ToBase64String(buffer).TrimEnd('=').Replace("+", "").Replace("/", "");

            return randomText.Substring(0, length).ToUpper();
        }
    }
}
