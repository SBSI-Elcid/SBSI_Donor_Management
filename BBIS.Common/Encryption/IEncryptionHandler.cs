namespace BBIS.Common.Encryption
{
    public interface IEncryptionHandler
    {
        string CreatSalt();
        string HashPassword(string password, string salt);
    }
}
