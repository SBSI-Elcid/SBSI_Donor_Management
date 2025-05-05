namespace BBIS.Common.Encryption
{
    public class EncryptionHandler : IEncryptionHandler
    {
        private const string _SecurityKey = "8AA49D87ED6CE1A3B2436DFB5146CF5FA1C195561B93BDFA1D45041C9E52D928BEAE030241BC3594029D8E829D4642745B5F230CF8BDFC2E792BF84D882788A2";

        public string CreatSalt()
        {
            return HashManager.GenerateSaltString();
        }

        public string HashPassword(string password, string salt)
        {
            return HashManager.CreateHash(password, salt, _SecurityKey);
        }
    }
}
